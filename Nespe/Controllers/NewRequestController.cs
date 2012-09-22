using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Models;
//using Outlook = Microsoft.Office.Interop.Outlook;

namespace Nespe.Controllers
{
    public class NewRequestController : AbstractController
    {

       
        //
        // GET: /NewRequest/
        [HttpGet]
        public ActionResult Index()
        {
            return Arrival();
        }

        [HttpGet]
        public ActionResult Arrival()
        {
            ViewBag.Message = "Nouvelle demande";
            var model = new ArrivalNewRequestModel { kind = RequestKindEnum.Arrival, StartDate = DateTime.Now.AddMonths(2) };
            return View( model);
        }
        [HttpGet]
        public ActionResult Departure()
        {
            ViewBag.Message = "Nouvelle demande";
            var model = new DepartureNewRequestModel { kind = RequestKindEnum.Departure, DepartureDate=DateTime.Now.AddMonths(1) };
            return View(model);
        }
        [HttpGet]
        public ActionResult Transfert()
        {
            ViewBag.Message = "Nouvelle demande";
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
            if (ModelState.IsValid)
            {
                //TO DO : Email guestResponse to ther part organizer
                Request request=model;
                
                SaveToDb(request);
                //RequestSet.Add(request);
                return View("Confirmation", request);
            }
            else
            {
                //there is a validation error - redisplay the form
                return View( model);
            }


        }
        [HttpPost]
        public ViewResult Departure(DepartureNewRequestModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                //TO DO : Email guestResponse to ther part organizer
                Request request = model;
                SaveToDb(request);
                return View("Confirmation", request);
            }
            else
            {
                //there is a validation error - redisplay the form
                return View( model);
            }
        }
        [HttpPost]
        public ViewResult Transfert(TransfertNewRequestModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                //TO DO : Email guestResponse to ther part organizer
                Request request = model;
                SaveToDb(request);
                return View("Confirmation", request);
            }
            else
            {
                //there is a validation error - redisplay the form
                return View(model);
            }
        }

        protected override void SaveToDb(Request request)
        {
            base.SaveToDb(request);
            UpdateRequestList(RequestSet);
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
