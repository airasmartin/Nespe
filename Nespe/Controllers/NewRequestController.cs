using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Models;
using Nespe.Context;
using Nespe.Helpers;
using System.Data.Entity.Validation;
//using Outlook = Microsoft.Office.Interop.Outlook;

namespace Nespe.Controllers
{
    public class NewRequestController : AbstractController
    {

       
   
        // GET: /NewRequest/
        [HttpGet]
        public ActionResult Index()
        {
            return Arrival();
        }
        private IQueryable<Department> listDepartment() { 
            using(var db=new NespeDbContext()){
                var r=(from t in db.DepartmentSet select t);
                return r;
            }
        }
        [HttpGet]
        public ActionResult Arrival()
        {
            
            ViewBag.Message = "New Request";
            var model = new ArrivalNewRequestModel { kind = RequestKindEnum.Arrival, StartDate = DateTime.Now.AddMonths(2) };
            return View( model);
        }
        [HttpGet]
        public ActionResult Departure()
        {
            ViewBag.Message = "New Request";
            var model = new DepartureNewRequestModel { kind = RequestKindEnum.Departure, DepartureDate=DateTime.Now.AddMonths(1) };
            return View(model);
        }
        [HttpGet]
        public ActionResult Transfert()
        {
            ViewBag.DepartmentList = listDepartment();
            ViewBag.Message = "New Request";
            var model = new TransfertNewRequestModel { kind = RequestKindEnum.Transfert, StartDate = DateTime.Now.AddDays(15) };

            return View(model);
        }

        [HttpPost]
        public ViewResult Index(ArrivalNewRequestModel model, FormCollection formCollection)
        {
            return Arrival(model, formCollection);
        }

        [HttpPost]
        public ViewResult Arrival(ArrivalNewRequestModel model, FormCollection formCollection)
        {
            try
            {
                //TO DO : Email guestResponse to ther part organizer
                Request request = model;
                if (ModelState.IsValid)
                {
                    WebMailHelper.SendEmailCreation(request);
                    SaveToDb(request);
                    return View("Confirmation", request);
                }
                else
                {
                    //there is a validation error - redisplay the form
                    return View(model);
                }
            }
            catch (DbEntityValidationException ex)
            {
                AddModelError(ex);
                return View(model);
            }

        }
        [HttpPost]
        public ViewResult Departure(DepartureNewRequestModel model, FormCollection formCollection)
        {

            try
            {

                Request request = model;
                if (ModelState.IsValid)
                {
                    //TO DO : Email guestResponse to ther part organizer

                    if (request != null)
                    {
                        if(string.IsNullOrWhiteSpace(request.EmployeeNumber))
                            request.EmployeeNumber = "NA";
                        
                    }

                    SaveToDb(request);
                    return View("Confirmation", request);
                }
                else
                {
                    //there is a validation error - redisplay the form
                    return View(model);
                }
            }
            catch (DbEntityValidationException ex)
            {
                AddModelError(ex);
                return View(model);
            }
        }
        [HttpPost]
        public ViewResult Transfert(TransfertNewRequestModel model, FormCollection formCollection)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    Request request = model;
                    //TO DO : Email guestResponse to ther part organizer
                    var selected = model;
                    WebMailHelper.SendEmailCreation(request);
                    SaveToDb(selected);
                    
                    return View("Confirmation", request);
                }
                else
                {
                    //there is a validation error - redisplay the form
                    return View(model);
                }
            }
            catch (DbEntityValidationException ex)
            {
                AddModelError(ex);
                return View(model);
            }
        }

        protected override Request SaveToDb(Request request)
        {
            var r=base.SaveToDb(request);
            UpdateRequestList(RequestSet);
            return r;
        }
        //void DemoSetInitialAddressList()
        //{
        //    Outlook.AddressList contactsAddrList = null;
        //    Outlook.SelectNamesDialog snd =
        //        Application.Session.GetSelectNamesDialog();

        //     First obtain the default Contacts folder.
        //    string contactsEntryID =
        //        Application.Session.GetDefaultFolder(
        //        Outlook.OlDefaultFolders.olFolderContacts).EntryID;

        //     Enumerate AddressLists.
        //    Outlook.AddressLists addrLists =
        //        Application.Session.AddressLists;
        //    foreach (Outlook.AddressList addrList in addrLists)
        //    {
        //        if (addrList.GetContactsFolder() != null)
        //        {

        //             GetContactsFolder returns Folder object; compare EntryIDs.
        //            if (Application.Session.CompareEntryIDs(
        //                addrList.GetContactsFolder().EntryID, contactsEntryID))
        //            {
        //                contactsAddrList = addrList;
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Could not find Contacts Address Book.",
        //                "Lookup Error",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Error);
        //            return;
        //        }
        //    }

        //     Set additional properties on SelectNamesDialog.
        //    snd.Caption = "Special Contest";

        //     Set InitialAddressList to Contacts folder AddressList.
        //    snd.InitialAddressList = contactsAddrList;
        //    snd.NumberOfRecipientSelectors =
        //        Outlook.OlRecipientSelectors.olShowTo;
        //    snd.ToLabel = "Award Winner(s)";

        //     Display.
        //    snd.Display();

        //     Enumerate names of selected award winners.
        //    Outlook.Recipients recips = snd.Recipients;
        //    if (recips.Count > 0)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        foreach (Outlook.Recipient recip in recips)
        //        {
        //            sb.AppendLine(recip.Name);
        //        }
        //        MessageBox.Show(sb.ToString(),
        //            "Contest Winners",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Information);
        //    }
        //}


    }
}
