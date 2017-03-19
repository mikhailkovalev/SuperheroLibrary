using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperheroLibrary.Models;

namespace SuperheroLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult CreateHero()
        {
            return View();
        }

        /*[HttpPost]
        public ActionResult CreateHero(/*some_args*)
        {

        }*/

        [HttpGet]
        public ActionResult CreateAbility()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAbility(Superability ability, HttpPostedFileBase uploadImage)
        {
            if (/*!ModelState.IsValid ||*/ uploadImage == null)
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
                    //user.Abilities.Add(ability);
                }
                db.Abilities.Add(ability);
                db.SaveChanges();
            }
            return View("AbilityAdded", ability);
        }

        [HttpGet]
        public ActionResult AbilityList()
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
            
            /*string userName = User.Identity.Name;
            User user = null;
            using (var db = new AppContext())
            {
                user = db.Users.FirstOrDefault(u => u.Login == userName);
                //user.Abilities = db.Abilities.Where(a => a.UserId == user.Id);
                user.Abilities = new List<Superability>().AddRange(db.Abilities.Where(a => a.UserId == user.Id));
            }
            return View(user.Abilities);*/

        }

        [HttpGet]
        public ActionResult EditAbility(int id)
        {
            Superability ability = null;
            using (var db = new AppContext())
            {
                ability = db.Abilities.Find(id);
            }
            return View(ability);
        }

        [HttpPost]
        public ActionResult EditAbility(Superability ability, HttpPostedFileBase uploadImage)
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
            if (ability.User == null) throw new Exception("Crap");
            return View("AbilityEdited", ability);
        }

        [HttpGet]
        public ActionResult DeleteAbility(int id)
        {
            Superability ability = null;
            using (var db = new AppContext())
            {
                ability = db.Abilities.Find(id);
            }
            return View(ability);
        }

        [HttpPost, ActionName("DeleteAbility")]
        public ActionResult DeleteConfirmed(int id)
        {
            Superability ability = null;
            using (var db = new AppContext())
            {
                ability = db.Abilities.Find(id);
                db.Abilities.Remove(ability);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}