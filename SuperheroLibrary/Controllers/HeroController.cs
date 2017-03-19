using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperheroLibrary.Models;

namespace SuperheroLibrary.Controllers
{
    public class HeroController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            int userId = 0;
            string userName = User.Identity.Name;
            IEnumerable<Superability> abilities = null;
            using (var db = new AppContext())
            {
                userId = db.Users.First(u => u.Login == userName).Id;
                abilities = new List<Superability>(db.Abilities.Where(a => a.UserId == userId));
            }
            return View(abilities);
        }

        [HttpPost]
        public ActionResult Create(Superhero hero, HttpPostedFileBase uploadImage, int[] selectedAbilities)
        {
            string userName = User.Identity.Name;
            IEnumerable<Superability> abilities = null;
            if (!ModelState.IsValid || uploadImage == null)
            {
                int userId = 0;
                using (var db = new AppContext())
                {
                    userId = db.Users.First(u => u.Login == userName).Id;
                    abilities = new List<Superability>(db.Abilities.Where(a => a.UserId == userId));
                }
                return View(abilities);
            }

            byte[] imageData = null;
            using (var br = new BinaryReader(uploadImage.InputStream))
            {
                imageData = br.ReadBytes(uploadImage.ContentLength);
            }

            hero.Image = imageData;

            User user = null;
            using (var db = new AppContext())
            {
                user = db.Users.FirstOrDefault(u => u.Login == userName);
                if (user != null)
                {
                    hero.User = user;
                    abilities = db.Abilities.Where(a => a.UserId == user.Id && selectedAbilities.Contains(a.Id));
                    foreach (var a in abilities)
                    {
                        hero.Abilities.Add(a);
                    }
                }

                db.Heroes.Add(hero);
                db.SaveChanges();
            }
            return View("Created", hero);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Superhero hero = null;
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
            }
        }

        [HttpPost]
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
                var abilities = db.Abilities.Where(a => selectedAbilities.Contains(a.Id));
                foreach (var a in abilities)
                {
                    keptHero.Abilities.Add(a);
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
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Superhero hero = null;
            using (var db = new AppContext())
            {
                hero = db.Heroes.Find(id);
            }
            return View(hero);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Deleting(int id)
        {
            Superhero hero = null;
            using (var db = new AppContext())
            {
                hero = db.Heroes.Find(id);
                db.Heroes.Remove(hero);
                db.SaveChanges();
            }
            return RedirectToAction("Show");
        }

        [HttpGet]
        public ActionResult Show()
        {
            int userId = 0;
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

            return View(heroes);
        }
    }
}