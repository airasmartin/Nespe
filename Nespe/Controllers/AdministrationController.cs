using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nespe.Controllers
{
    public class AdministrationController : Controller
    {
        //
        // GET: /Administration/

        public ActionResult Index()
        {
            return View();
        }

    }

    public class AdminDepartments
    {
        [Display(Name = "Department")]
        public string department { get; set; }
        [Display(Name = "Department's head")]
        public string headDepartment { get; set; }
        [Display(Name = "Assistant 1")]
        public string assistant1 { get; set; }
        [Display(Name = "Assistant 2")]
        public string assistant2 { get; set; }
        [Display(Name = "Assistant 3")]
        public string assistant3 { get; set; }
    }
}
