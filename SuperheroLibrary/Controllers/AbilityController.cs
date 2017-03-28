using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperheroLibrary.Models;
using SuperheroLibrary.Models.ViewModels;
using SuperheroLibrary.Services;

namespace SuperheroLibrary.Controllers
{
    public class AbilityController : Controller
    {
        private UserService userService = new UserService();
        private AbilityService abilityService = new AbilityService();
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AbilityCreateModel model)
        {
            if (!ModelState.IsValid || model.UploadImage == null)
            {
                return View(model);
            }
            model.UserId = userService.GetUserIdByName(User.Identity.Name);
            abilityService.CreateAbility(model);
            return View("Created", model);
            
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(abilityService.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(AbilityEditModel model)
        {
            abilityService.EditAbility(model);
            return View("Edited", model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(abilityService.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Deleting(int id)
        {
            abilityService.DeleteAbility(id);
            return RedirectToAction("Show");
        }

        [HttpGet]
        public ActionResult Show()
        {
            /*int? userId = userService.GetUserIdByName(User.Identity.Name);
            var abilities = abilityService.GetAbilitiesBelongsUser(userId);*/
            var model = abilityService.GetShowModel(User.Identity.Name);
            return View(model);
        }
    }
}