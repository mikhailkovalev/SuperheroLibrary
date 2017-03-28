using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperheroLibrary.Models;
using SuperheroLibrary.Models.ViewModels;

namespace SuperheroLibrary.Services
{
    public class HeroService
    {
        private UserService userService = new UserService();
        private AbilityService abilityService = new AbilityService();
        public Superhero GetById(int id)
        {
            Superhero hero = null;
            using (var db = new AppContext())
            {
                hero = db.Heroes.Find(id);
                hero.Abilities = hero.Abilities.ToList();
            }
            return hero;
        }

        public ICollection<Superhero> GetHeroesBelongsUser(int? userId)
        {
            if (userId == null)
            {
                return new List<Superhero>();
            }
            ICollection<Superhero> heroes = null;
            using (var db = new AppContext())
            {
                heroes = db.Heroes.Where(a => a.UserId == userId).ToList();
            }
            return heroes;
        }

        public HeroCreateModel GetHeroCreateModel(string userName)
        {
            HeroCreateModel model = new HeroCreateModel();

            int? userId = userService.GetUserIdByName(userName);
            model.UserId = userId;
            model.UserAblilities = abilityService.GetAbilitiesBelongsUser(userId);

            return model;
        }

        public HeroEditModel GetHeroEditModel(int id)
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<Superhero, HeroEditModel>());

            Superhero hero = null;
            HeroEditModel model = null;

            using (var db = new AppContext())
            {
                hero = db.Heroes.Find(id);
                model = AutoMapper.Mapper.Map<HeroEditModel>(hero);
                model.UserAbilities = db.Abilities.Where(a => a.UserId == hero.UserId).ToList();
                model.SelectedAbilities = new int[model.Abilities.Count];
                for (int i = 0; i < model.Abilities.Count; ++i)
                {
                    model.SelectedAbilities[i] = model.Abilities.ElementAt(i).Id;
                }
            }

            return model;
        }

        public void CreateHero(HeroCreateModel model)
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<HeroCreateModel, Superhero>());

            var hero = AutoMapper.Mapper.Map<Superhero>(model);
            model.Image = hero.Image = ImageService.GetImageData(model.UploadImage);

            using (var db = new AppContext())
            {
                model.UserAblilities = db.Abilities.Where(a => a.UserId == model.UserId).ToList();

                ICollection<Superability> chosenAbilities = null;

                if (model.SelectedAbilities == null)
                {
                    chosenAbilities = new List<Superability>();
                }
                else
                {
                    chosenAbilities = model.UserAblilities.Where(a => model.SelectedAbilities.Contains(a.Id)).ToList();
                }
                foreach (var a in chosenAbilities)
                {
                    hero.Abilities.Add(a);
                }

            
                db.Heroes.Add(hero);
                db.SaveChanges();
            }
        }

        public void EditHero(HeroEditModel model)
        {
            byte[] imageData = null;
            if (model.UploadImage != null)
            {
                imageData = ImageService.GetImageData(model.UploadImage);
            }
            using (var db = new AppContext())
            {
                var keptHero = db.Heroes.Find(model.Id);
                keptHero.Name = model.Name;
                keptHero.Description = model.Description;
                if (model.UploadImage != null)
                {
                    model.Image = keptHero.Image = imageData;
                }
                else
                {
                    model.Image = keptHero.Image;
                }

                var userAbilities = db.Abilities.Where(a => a.UserId == model.UserId);
                ICollection<Superability> chosenAbilities = null;
                if (model.SelectedAbilities == null)
                {
                    chosenAbilities = new List<Superability>();
                }
                else
                {
                    chosenAbilities = db.Abilities.Where(a => model.SelectedAbilities.Contains(a.Id)).ToList();
                }
                keptHero.Abilities.Clear();
                foreach (var a in chosenAbilities)
                {
                    keptHero.Abilities.Add(a);
                }

                db.SaveChanges();
            }
        }

        public void DeleteHero(int id)
        {
            using (var db = new AppContext())
            {
                var hero = db.Heroes.Find(id);
                db.Heroes.Remove(hero);
                db.SaveChanges();
            }
        }

        public HeroesShowModel GetShowModel(string userName)
        {
            HeroesShowModel model = new HeroesShowModel();

            using (var db = new AppContext())
            {
                int userId = db.Users.FirstOrDefault(u => u.Login == userName).Id;
                var heroes = db.Heroes.Where(h => h.UserId == userId).ToList();
                model.HeroesList = new List<HeroShowModel>(heroes.Count());
                foreach (var h in heroes)
                {
                    var showModel = new HeroShowModel
                    {
                        Id = h.Id,
                        Abilities = h.Abilities.ToList(),
                        Description = h.Description,
                        Image = h.Image,
                        Name = h.Name,
                        UserId = h.UserId
                    };
                    model.HeroesList.Add(showModel);
                }
            }

            return model;
        }
    }
}