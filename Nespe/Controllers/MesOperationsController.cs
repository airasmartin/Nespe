using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Models;

namespace Nespe.Controllers
{
    public class MesOperationsController : AbstractController
    {
        //
        // GET: /MesOperations/
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "Mes Opérations";
            UpdateRequestList(RequestSet);
            var model = new CurrentStatusModel { RequestSet = RequestSet.AsQueryable() };
            return View("Index", model);

        }
        //
        // GET: /Completer/
        [HttpGet]
        public ActionResult Completer(long id)
        {
            ViewBag.Message = "Mes Opérations";
            UpdateRequestList(RequestSet);
            //var dst = new CurrentStatusModel { RequestSet = RequestSet.AsQueryable() };
            var requestData=( from t in RequestSet where t.Id==id select t).FirstOrDefault();
            var model = new ArrivalCompleterFormulaireModel
            {
                Id = id,
                RequestModel = requestData,
                dateCompleted = DateTime.Now.AddDays(-1),
                ITInfo = new ITRequestInfo { },
                TelephoneInfo = new TelephoneRequestInfo { },
                RoleSAPInfo = new RoleSAPRequestInfo { },
                PMOInfo = new PMORequestInfo { },
                MailCaseInfo = new MailCaseRequestInfo { },
                ClothesInfo = new ClothesRequestInfo { },
                LockerInfo = new LockerRequestInfo { },
                IntroInfo = new IntroRequestInfo { },

                //Id = new ArrivalNewRequestModel(),
                //Name = new ArrivalNewRequestModel(),
                //Executor = new ArrivalNewRequestModel(),
                //Status = new ArrivalNewRequestModel(),
                //Date = new ArrivalNewRequestModel(),
                //Comment = new ArrivalNewRequestModel(),

                //necessaryIT = new ArrivalNewRequestModel(),
                //laptop = new ArrivalNewRequestModel(),
                //docking = new ArrivalNewRequestModel(),
                //keyboard = new ArrivalNewRequestModel(),
                //desktop = new ArrivalNewRequestModel(),
                //screen = new ArrivalNewRequestModel(),
                //mouse = new ArrivalNewRequestModel(),
                //commentIT = new ArrivalNewRequestModel(),

                //necessaryPhone
                //fixPhone

            };
            return View(model);

        }
        [HttpPost]
        public ActionResult Completer(ArrivalCompleterFormulaireModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                //TO DO : Email guestResponse to ther part organizer
                //Request request = dst;

                //SaveToDb(dst);
                //RequestSet.Add(request);
                //return View("Confirmation", dst);
                return this.RedirectToAction("Index");
                //return Index();
            }
            else
            {
                //there is a validation error - redisplay the form
                return View(model);
            }


        }

        //
        // GET: /Edit/
        [HttpGet]
        public ActionResult Edit(long id)
        {
            ViewBag.Message = "Mes Opérations";
            UpdateRequestList(RequestSet);
            //var dst = new CurrentStatusModel { RequestSet = RequestSet.AsQueryable() };
            var requestData = (from t in RequestSet where t.Id == id select t).FirstOrDefault();

            var model = new ArrivalEditFormulaireModel
            {
                Id = id,
                RequestModel = requestData,
                dateCompleted = DateTime.Now.AddDays(-1),
                ITInfo = new ITRequestInfo { },
                TelephoneInfo = new TelephoneRequestInfo { },
                RoleSAPInfo = new RoleSAPRequestInfo { },
                PMOInfo = new PMORequestInfo { },
                MailCaseInfo = new MailCaseRequestInfo { },
                ClothesInfo = new ClothesRequestInfo { },
                LockerInfo = new LockerRequestInfo { },
                IntroInfo = new IntroRequestInfo { },
            };
            return View(model);

        }

        //
        // GET: /Edit/
        [HttpGet]
        public ActionResult Edit(ArrivalEditFormulaireModel model, FormCollection formCollection)
        {
            return View(model);

        }

        protected virtual void SaveToDb(ArrivalEditFormulaireModel model)
        {
            base.SaveToDb(model.RequestModel);


            UpdateRequestList(RequestSet);
        }

    }

    public class EditInfoNC
    {
        public bool recievePers { get; set; }
        public DateTime recievePersDate { get; set; }
        public bool recieveNC { get; set; }
        public DateTime recieveNCDate { get; set; }
        public bool shePers { get; set; }
        public DateTime shePersDate { get; set; }
        public bool sheNC { get; set; }
        public DateTime sheNCDate { get; set; }
        public bool presentationPers { get; set; }
        public DateTime presentationPersDate { get; set; }
        public bool presentationNC { get; set; }
        public DateTime presentationNCDate { get; set; }
        public bool precFunctPers { get; set; }
        public DateTime precFunctPersDate { get; set; }
        public bool precFunctNC { get; set; }
        public DateTime precFunctNCDate { get; set; }
        public bool badgePers { get; set; }
        public DateTime badgePersDate { get; set; }
        public bool badgeNC { get; set; }
        public DateTime badgeNCDate { get; set; }
        public bool equipmentPers { get; set; }
        public DateTime equipmentPersDate { get; set; }
        public bool equipmentNC { get; set; }
        public DateTime equipmentNCDate { get; set; }
        public bool programPers { get; set; }
        public DateTime programPersDate { get; set; }
        public bool ProgramNC { get; set; }
        public DateTime ProgramNCDate { get; set; }
        public bool organisationPers { get; set; }
        public DateTime organisationPersDate { get; set; }
        public bool organisationNC { get; set; }
        public DateTime organisationNCDate { get; set; }
        public bool administrativPers { get; set; }
        public DateTime administrativPersDate { get; set; }
        public bool administrativNC { get; set; }
        public DateTime administrativNCDate { get; set; }
        public bool lifePers { get; set; }
        public DateTime lifePersDate { get; set; }
        public bool lifeNC { get; set; }
        public DateTime lifeNCDate { get; set; }
        public bool basePers { get; set; }
        public DateTime basePersDate { get; set; }
        public bool baseNC { get; set; }
        public DateTime baseNCDate { get; set; }
        public bool servicesPers { get; set; }
        public DateTime servicesPersDate { get; set; }
        public bool servicesNC { get; set; }
        public DateTime servicesNCDate { get; set; }
        public bool instructionsPers { get; set; }
        public DateTime instructionsPersDate { get; set; }
        public bool instructionsNC { get; set; }
        public DateTime instructionsNCDate { get; set; }
        public bool tipPers { get; set; }
        public DateTime tipPersDate { get; set; }
        public bool tipNC { get; set; }
        public DateTime tipNCDate { get; set; }
    }
}
