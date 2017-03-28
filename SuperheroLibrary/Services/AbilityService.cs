using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperheroLibrary.Models;
using SuperheroLibrary.Models.ViewModels;

namespace SuperheroLibrary.Services
{
    public class AbilityService
    {
        private UserService userService = new UserService();
        public Superability GetById(int id)
        {
            Superability ability = null;
            using (var db = new AppContext())
            {
                ability = db.Abilities.Find(id);
            }
            return ability;
        }

        public ICollection<Superability> GetAbilitiesBelongsUser(int? userId)
        {
            if (userId == null)
            {
                return new List<Superability>();
            }
            ICollection<Superability> abilities = null;
            using (var db = new AppContext())
            {
                abilities = db.Abilities.Where(a => a.UserId == userId).ToList();
            }
            return abilities;
        }

        public void CreateAbility(AbilityCreateModel model)
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<AbilityCreateModel, Superability>());

            var ability = AutoMapper.Mapper.Map<Superability>(model);
            model.Image = ability.Image = ImageService.GetImageData(model.UploadImage);
            
            using (var db = new AppContext())
            {
                db.Abilities.Add(ability);
                db.SaveChanges();
            }
        }

        public void EditAbility(AbilityEditModel model)
        {
            
            byte[] imageData = null;
            if (model.UploadImage != null)
            {
                imageData = ImageService.GetImageData(model.UploadImage);
            }
            using (var db = new AppContext())
            {
                var keptAbility = db.Abilities.Find(model.Id);
                keptAbility.Name = model.Name;
                keptAbility.Description = model.Description;
                if (model.UploadImage != null)
                {
                    model.Image = keptAbility.Image = imageData;
                }
                else
                {
                    model.Image = keptAbility.Image;
                }
                db.SaveChanges();
            }
        }

        public void DeleteAbility(int id)
        {
            Superability ability = null;
            using (var db = new AppContext())
            {
                ability = db.Abilities.Find(id);
                db.Abilities.Remove(ability);
                db.SaveChanges();
            }
        }

        public AbilitiesShowModel GetShowModel(string userName)
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<Superability, AbilityShowModel>());

            AbilitiesShowModel model = new AbilitiesShowModel();

            using (var db = new AppContext())
            {
                var userId = db.Users.FirstOrDefault(u => u.Login == userName).Id;
                var abilities = db.Abilities.Where(a => a.UserId == userId).ToList();
                model.AbilitiesList = AutoMapper.Mapper.Map<IEnumerable<Superability>, List<AbilityShowModel>>(abilities);
            }

            return model;
        }
    }
}