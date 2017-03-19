using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperheroLibrary.Models;

namespace SuperheroLibrary.Controllers
{
    public class AbilityController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Superability ability, HttpPostedFileBase uploadImage)
        {
            if (!ModelState.IsValid || uploadImage == null)
            {
                return View(ability);
            }

            byte[] imageData = null;

            using (var br = new BinaryReader(uploadImage.InputStream))
            {
                imageData = br.ReadBytes(uploadImage.ContentLength);
            }

            ability.Image = imageData;
            User user = null;
            using (var db = new AppContext())
            {
                user = db.Users.FirstOrDefault(u => u.Login == User.Identity.Name);
                if (user != null)
                {
                    ability.User = user;
                }
                db.Abilities.Add(ability);
                db.SaveChanges();
            }
            return View("Created", ability);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Superability ability = null;
            using (var db = new AppContext())
            {
                ability = db.Abilities.Find(id);
            }
            return View(ability);
        }

        [HttpPost]
        public ActionResult Edit(Superability ability, HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null)
            {
                byte[] imageData = null;
                using (var br = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = br.ReadBytes(uploadImage.ContentLength);
                }
                ability.Image = imageData;
            }
            using (var db = new AppContext())
            {
                var keptAbility = db.Abilities.Find(ability.Id);
                keptAbility.Name = ability.Name;
                keptAbility.Description = ability.Description;
                if (uploadImage != null)
                {
                    keptAbility.Image = ability.Image;
                }
                else
                {
                    ability.Image = keptAbility.Image;
                }
                db.SaveChanges();
                ability.User = db.Users.Find(keptAbility.UserId);
            }

            return View("Edited", ability);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Superability ability = null;
            using (var db = new AppContext())
            {
                ability = db.Abilities.Find(id);
            }
            return View(ability);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Deleting(int id)
        {
            Superability ability = null;
            using (var db = new AppContext())
            {
                ability = db.Abilities.Find(id);
                db.Abilities.Remove(ability);
                db.SaveChanges();
            }
            return RedirectToAction("Show");
        }

        [HttpGet]
        public ActionResult Show()
        {
            List<Superability> abilities = new List<Superability>();

            string userName = User.Identity.Name;
            int userId = 0;
            using (var db = new AppContext())
            {
                userId = db.Users.FirstOrDefault(u => u.Login == userName).Id;
                abilities.AddRange(db.Abilities.Where(a => a.UserId == userId));
            }
            return View(abilities);
        }
    }
}