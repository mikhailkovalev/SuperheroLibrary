using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperheroLibrary.Models;
using SuperheroLibrary.Models.ViewModels;

namespace SuperheroLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeIndexModel model = new HomeIndexModel { UserName = User.Identity.Name, Title = "Главная" };
            return View(model);
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
    }
}