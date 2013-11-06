using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Models;

namespace Nespe.Controllers
{
    public class HomeController : AbstractController
    {
        public ActionResult Index()
        {

            UpdateRequestList(RequestSet);
            var model = new CurrentStatusModel { RequestSet = RequestSet.AsQueryable() };
            return View(model);
        }

        public ActionResult MesOperations()
        {
            ViewBag.Message = "My requests";
            UpdateRequestList(RequestSet);
            var model = new CurrentStatusModel { RequestSet = RequestSet.AsQueryable() };
            return View(model);
        }

        public ActionResult Recherches()
        {
            ViewBag.Message = "Search";

            UpdateRequestList(RequestSet);
            var model = new CurrentStatusModel { RequestSet = RequestSet.AsQueryable() };
            return View(model);

        }

        [HttpGet]
        public ActionResult NewRequest()
        {
            return View();
        }

        [HttpPost]
        public ViewResult NewRequest(Request model, FormCollection formCollection)
        {
            return View(model);
        }


        public ActionResult Administration()
        {
            ViewBag.Message = "Administration";

            return View();
        }
        public ActionResult Formulaire()
        {
            ViewBag.Message = "Form";

            return View();
        }
    }
}
