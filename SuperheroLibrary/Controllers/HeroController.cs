using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperheroLibrary.Models;
using SuperheroLibrary.Services;
using SuperheroLibrary.Models.ViewModels;

namespace SuperheroLibrary.Controllers
{
    public class HeroController : Controller
    {
        /*private UserService    userService    = new UserService();
        
        private AbilityService abilityService = new AbilityService();*/
        private HeroService heroService = new HeroService();
        [HttpGet]
        public ActionResult Create()
        {
            var model = heroService.GetHeroCreateModel(User.Identity.Name);
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(HeroCreateModel model)
        {
            if (!ModelState.IsValid || model.UploadImage == null)
            {
                return View(model);
            }
            heroService.CreateHero(model);
            return View("Created", model);
        }

        

        [HttpGet]
        public ActionResult Edit(int id)
        {
            /*Superhero hero = null;
            using (var db = new AppContext())
            {
                hero = db.Heroes.First(h => h.Id == id);
                hero.Abilities = new List<Superability>(hero.Abilities);
            }
            if (hero != null)
            {
                return View(hero);
            }
            else
            {
                return RedirectToAction("Show");
            }*/
            var hero = heroService.GetHeroEditModel(id);
            return View(hero);
        }

        /*[HttpPost]
        public ActionResult Edit(Superhero hero, HttpPostedFileBase uploadImage, int[] selectedAbilities)
        {
            if (uploadImage != null)
            {
                byte[] imageData = null;
                using (var br = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = br.ReadBytes(uploadImage.ContentLength);
                }
                hero.Image = imageData;
            }
            using (var db = new AppContext())
            {
                var keptHero = db.Heroes.Find(hero.Id);
                keptHero.Name = hero.Name;
                keptHero.Description = hero.Description;

                keptHero.Abilities.Clear();
                if (selectedAbilities != null)
                {
                    var abilities = db.Abilities.Where(a => selectedAbilities.Contains(a.Id));
                    foreach (var a in abilities)
                    {
                        keptHero.Abilities.Add(a);
                    }
                }
                if (uploadImage != null)
                {
                    keptHero.Image = hero.Image;
                }
                else
                {
                    hero.Image = keptHero.Image;
                }
                db.SaveChanges();
                hero.User = db.Users.Find(keptHero.UserId);
            }
            return View("Edited", hero);
        }*/
        [HttpPost]
        public ActionResult Edit(HeroEditModel model)
        {
            heroService.EditHero(model);
            return View("Edited", model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var hero = heroService.GetById(id);
            return View(hero);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Deleting(int id)
        {
            heroService.DeleteHero(id);
            return RedirectToAction("Show");
        }

        [HttpGet]
        public ActionResult Show()
        {
            /*int userId = 0;
            string userName = User.Identity.Name;
            IEnumerable<Superhero> heroes = null;

            using (var db = new AppContext())
            {
                userId = db.Users.First(u => u.Login == userName).Id;
                heroes = new List<Superhero>(db.Heroes.Where(h => h.UserId == userId));
                foreach(var h in heroes)
                {
                    h.Abilities = new List<Superability>(h.Abilities);
                }
            }

            return View(heroes);*/
            var model = heroService.GetShowModel(User.Identity.Name);
            return View(model);
        }
    }
}