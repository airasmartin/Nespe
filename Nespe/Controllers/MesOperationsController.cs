using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Models;
using Nespe.Context;
using System.Data.Entity;
using Nespe.Helpers;

namespace Nespe.Controllers
{
    public class MesOperationsController : AbstractController
    {
        //
        // GET: /MesOperations/
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "My requests";
            UpdateRequestList(RequestSet);
            var model = new CurrentStatusModel { RequestSet = RequestSet.AsQueryable() };
            return View("Index", model);

        }

        public T GetByRequestId<T>(long Request_id, DbSet<T> drc, T dr) where T : AbstractRequestInfo
        {
            var r = (from t in drc where t.Request_Id == Request_id select t).FirstOrDefault();
            if (r == null)
                r = dr;
            return r;
        }
        public List<T> FillByRequestId<T>(long Request_id, DbSet<T> drc, List<T> r) where T : AbstractRequestInfo
        {
            var l = (from t in drc where t.Request_Id == Request_id select t);
            if (l == null || l.Count()<1)
                return r;
            if (r == null)
                r = new List<T>();
            else
                r.Clear();
            foreach (var o in l) {
                r.Add(o);
            }
            return r;
        }
		public void SaveToDb<T>(DbSet<T> drc, List<T> r) where T : AbstractRequestInfo
		{
			foreach (var o in r)
			{
				if (o == null)
					continue;
				var dr = (from t in drc where t.Id>0 && t.Id==o.Id select t).FirstOrDefault();
				if(dr!=null){
					AbstractRequestInfo.Copy(dr, o, true);
				}else{
					drc.Add(o);
				}
				
			}
		
		}
		
        //
        // GET: /Completer/
        [HttpGet]
        public ActionResult Completer(long id)
        {
            ViewBag.Message = "My requests";
            UpdateRequestList(RequestSet);
            using (var db = new NespeDbContext())
            {
                var requestData = (from t in RequestSet where t.Id == id select t).FirstOrDefault();
                var dateCompleted = DateTime.Now.AddDays(-1);
                var model = new ArrivalCompleterFormulaireModel
                {
                    Id = id,
                    RequestModel = requestData,
                    dateCompleted = dateCompleted,
                    ITInfo = GetByRequestId(id, db.ITRequestSet, new ITRequestInfo { Request_Id = id, Date = dateCompleted }),
                    TelephoneInfo = GetByRequestId(id, db.TelephoneRequestSet, new TelephoneRequestInfo { Request_Id = id, Date = dateCompleted }),
                    RoleSAPInfo = GetByRequestId(id, db.RoleSAPRequestSet, new RoleSAPRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMOInfo = GetByRequestId(id, db.PMORequestSet, new PMORequestInfo {Request_Id =id, Date =dateCompleted}),
                    PMOCatsInfo = GetByRequestId(id, db.PMOCatsSet, new PMOCatsRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMODMSInfo = GetByRequestId(id, db.PMODMSSet, new PMODMSRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMONPDIInfo = GetByRequestId(id, db.PMONPDISet, new PMONPDIRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMONESTMSInfo = GetByRequestId(id, db.PMONESTMSSet, new PMONESTMSRequestInfo { Request_Id = id, Date = dateCompleted }),
                    MailCaseInfo = GetByRequestId(id, db.MailCaseRequestSet, new MailCaseRequestInfo { Request_Id = id, Date = dateCompleted }),
                    ClothesInfo = GetByRequestId(id, db.ClothesRequestSet, new ClothesRequestInfo { Request_Id = id, Date = dateCompleted }),
                    LockerInfo = GetByRequestId(id, db.LockerRequestSet, new LockerRequestInfo { Request_Id = id, Date = dateCompleted }),
                    IntroInfo = GetByRequestId(id, db.IntroRequestSet, new IntroRequestInfo { Request_Id = id, Date = dateCompleted }),
                    
                };
                if (string.IsNullOrWhiteSpace(model.ITInfo.Name))
                    model.ITInfo.Name = "My ticket";
                if (!string.IsNullOrWhiteSpace(model.ITInfo.Name))
                    model.ITInfo.StaticDescription = model.ITInfo.Name;
                return View(model);
            }

        }
        
        [HttpPost]
        public ActionResult ArrivalNewRequestSaveChanges(ArrivalCompleterFormulaireModel model, FormCollection formCollection)
        {
            Request request = model.RequestModel;
            using (var db = new NespeDbContext())
            {
                var dp = (from t in db.PersonDepartmentSet from r in db.RequestSet where r.PersonDepartment_Id != null && r.Id == request.Id && t.Id == r.PersonDepartment_Id select t).FirstOrDefault();
                /*
                request.PersonDepartment = (from t in db.PersonDepartmentSet from r in db.RequestSet where r.PersonDepartment_Id != null && r.Id == request.Id && t.Id == r.PersonDepartment_Id select t).FirstOrDefault();
                var personDepartment = request.PersonDepartment;
                if (request.PersonDepartment != null)
                {
                    request.PersonDepartment.Person = (from t in db.PersonSet where request.PersonDepartment.Person_Id != null && t.Id == request.PersonDepartment.Person_Id select t).FirstOrDefault();
                    request.PersonDepartment.Department = (from t in db.DepartmentSet where request.PersonDepartment.Person_Id != null && t.Id == request.PersonDepartment.Department_Id select t).FirstOrDefault();
                    request.PersonDepartment_Id = request.PersonDepartment.Id;

                }
                var person = request.Person;

                if (person != null)
                {
                    request.PersonDepartment.Person_Id = person.Id;
                    model.RequestModel.Initials = person.Initials;
                    model.RequestModel.FirstName = person.FirstName;
                    model.RequestModel.LastName = person.LastName;
                }
                var department = request.Department;
                if (department != null)
                {
                    request.PersonDepartment.Department_Id = department.Id;
                }*/
                var p=( from t in db.PersonSet where t.Id== dp.Person_Id select t).FirstOrDefault();
                
                p.Initials = model.RequestModel.Initials;
                p.FirstName = model.RequestModel.FirstName;
                p.LastName = model.RequestModel.LastName;
                p.Phone = model.RequestModel.Phone;

                var q = (from t in db.RequestSet where t.Id == dp.Person_Id select t).FirstOrDefault();
                q.ActiveDirectoryId = model.RequestModel.ActiveDirectoryId;
                q.BusinessStream = model.RequestModel.BusinessStream;
                q.EmployeeNumber = model.RequestModel.EmployeeNumber;
                q.Function = model.RequestModel.Function;
                q.Local = model.RequestModel.Local;
                q.nonSAP = model.RequestModel.nonSAP;
                q.Parrain = model.RequestModel.Parrain;
                q.Superior = model.RequestModel.Superior;
                               
               
                db.SaveChanges();
            }

            return RedirectToAction("Completer", new { Id=model.RequestModel.Id });
            //var v = View("Completer", model);
            //return View("Completer", model);
        }
        [HttpPost]
        public ActionResult Completer(ArrivalCompleterFormulaireModel model, FormCollection formCollection)
        {


            if (model != null)
            {
                model.dateCompleted = DateTime.Now;
                var a = new AbstractRequestInfo[] { model.ITInfo, model.TelephoneInfo, model.RoleSAPInfo, model.PMOInfo, model.PMOCatsInfo, model.PMODMSInfo, model.PMONPDIInfo, model.PMONESTMSInfo, model.MailCaseInfo, model.ClothesInfo, model.LockerInfo, model.IntroInfo };
                foreach (var o in a)
                {
                    o.Request_Id = model.RequestModel.Id;
                    o.Date = model.dateCompleted;
                }
            }

            Request request = model.RequestModel;
            using (var db = new NespeDbContext())
            {

                request.PersonDepartment = (from t in db.PersonDepartmentSet from r in db.RequestSet where r.PersonDepartment_Id != null && r.Id == request.Id && t.Id == r.PersonDepartment_Id select t).FirstOrDefault();
                var personDepartment = request.PersonDepartment;
                if (request.PersonDepartment != null)
                {
                    request.PersonDepartment.Person = (from t in db.PersonSet where request.PersonDepartment.Person_Id != null && t.Id == request.PersonDepartment.Person_Id select t).FirstOrDefault();
                    request.PersonDepartment.Department = (from t in db.DepartmentSet where request.PersonDepartment.Person_Id != null && t.Id == request.PersonDepartment.Department_Id select t).FirstOrDefault();
                    request.PersonDepartment_Id = request.PersonDepartment.Id;

                }
                var person = request.Person;

                if (person != null)
                {
                    request.PersonDepartment.Person_Id = person.Id;
                    model.RequestModel.Initials = person.Initials;
                    model.RequestModel.FirstName = person.FirstName;
                    model.RequestModel.LastName = person.LastName;
                }
                var department = request.Department;
                if (department != null)
                {
                    request.PersonDepartment.Department_Id = department.Id;
                }

                //ValidateModel(model);
                /// TODO Just temporary operation
                if (true || ModelState.IsValid)
                {

                    var currentRequest = (from t in db.RequestSet where t.Id == model.RequestModel.Id select t).FirstOrDefault();
                    if (currentRequest != null)
                    {
                        if (!string.IsNullOrEmpty(model.RequestModel.EmployeeNumber))
                            currentRequest.EmployeeNumber = model.RequestModel.EmployeeNumber;

                        currentRequest.Completed = true;
                        ////Last line opf this block
                        //SaveToDb(currentRequest, db);
                        //model.RequestModel = currentRequest;
                    }

                    

                    model.PMOCatsInfo.IsRequired = model.PMOInfo.cats;
                    model.PMODMSInfo.IsRequired = model.PMOInfo.dms;
                    model.PMONPDIInfo.IsRequired = model.PMOInfo.npdi;
                    model.PMONESTMSInfo.IsRequired = model.PMOInfo.nestms;
                    

                    


                    SaveToDb(db.ITRequestSet, model.ITInfo, db);
                    request.SendInfo(model.ITInfo);
                    SaveToDb(db.TelephoneRequestSet, model.TelephoneInfo, db);
                    request.SendInfo(model.TelephoneInfo);
                    SaveToDb(db.PMORequestSet, model.PMOInfo, db);
                    request.SendInfo(model.PMOInfo);
                    SaveToDb(db.PMOCatsSet, model.PMOCatsInfo, db);
                    SaveToDb(db.PMODMSSet, model.PMODMSInfo, db);
                    SaveToDb(db.PMONPDISet, model.PMONPDIInfo, db);
                    SaveToDb(db.PMONESTMSSet, model.PMONESTMSInfo, db);
                    SaveToDb(db.MailCaseRequestSet, model.MailCaseInfo, db);
                    request.SendInfo(model.MailCaseInfo);
                    SaveToDb(db.ClothesRequestSet, model.ClothesInfo, db);
                    request.SendInfo(model.ClothesInfo);
                    SaveToDb(db.LockerRequestSet, model.LockerInfo, db);
                    request.SendInfo(model.LockerInfo);
                    SaveToDb(db.IntroRequestSet, model.IntroInfo, db);
                    request.SendInfo(model.IntroInfo);
                    SaveToDb(db.RoleSAPRequestSet, model.RoleSAPInfo, db);
                    request.SendInfo(model.RoleSAPInfo);
                    request.SendSummarize(model.ITInfo, model.TelephoneInfo, model.RoleSAPInfo, model.PMOInfo, model.MailCaseInfo, model.ClothesInfo, model.LockerInfo, model.IntroInfo);

                    db.SaveChanges();

                    //WebMailHelper.SendEMail(model.RequestModel);
                    //SaveToDb(dst);
                    //RequestSet.Add(selected);
                    //return View("Confirmation", dst);
                    return this.RedirectToAction("Index");
                    //return Index();

                }
                else
                {
                    return View(model);

                }


            }


        }

        [HttpGet]
        public ActionResult CompleterDepart(long id)
        {
            ViewBag.Message = "My requests";
            UpdateRequestList(RequestSet);
            using (var db = new NespeDbContext())
            {
                var requestData = (from t in RequestSet where t.Id == id select t).FirstOrDefault();
                var dateCompleted = DateTime.Now.AddDays(-1);
                var model = new DepartureCompleterFormulaireModel
                {
                    Id = id,
                    RequestModel = requestData,
                    dateCompleted = dateCompleted,
                    ITInfo = GetByRequestId(id, db.ITRequestSet, new ITRequestInfo { Request_Id = id, Date = dateCompleted }),
                    TelephoneInfo = GetByRequestId(id, db.TelephoneRequestSet, new TelephoneRequestInfo { Request_Id = id, Date = dateCompleted }),
                    RoleSAPInfo = GetByRequestId(id, db.RoleSAPRequestSet, new RoleSAPRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMOInfo = GetByRequestId(id, db.PMORequestSet, new PMORequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMOCatsInfo = GetByRequestId(id, db.PMOCatsSet, new PMOCatsRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMODMSInfo = GetByRequestId(id, db.PMODMSSet, new PMODMSRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMONPDIInfo = GetByRequestId(id, db.PMONPDISet, new PMONPDIRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMONESTMSInfo = GetByRequestId(id, db.PMONESTMSSet, new PMONESTMSRequestInfo { Request_Id = id, Date = dateCompleted }),
                    MailCaseInfo = GetByRequestId(id, db.MailCaseRequestSet, new MailCaseRequestInfo { Request_Id = id, Date = dateCompleted }),
                    ClothesInfo = GetByRequestId(id, db.ClothesRequestSet, new ClothesRequestInfo { Request_Id = id, Date = dateCompleted }),
                    LockerInfo = GetByRequestId(id, db.LockerRequestSet, new LockerRequestInfo { Request_Id = id, Date = dateCompleted }),
                    IntroInfo = GetByRequestId(id, db.IntroRequestSet, new IntroRequestInfo { Request_Id = id, Date = dateCompleted }),
                };
                if (string.IsNullOrWhiteSpace(model.ITInfo.Name))
                    model.ITInfo.Name = "My ticket";
                if (!string.IsNullOrWhiteSpace(model.ITInfo.Name))
                    model.ITInfo.StaticDescription = model.ITInfo.Name;

                    return View(model);
                
            }

        }
        [HttpPost]
        public ActionResult CompleterDepart(DepartureCompleterFormulaireModel model, FormCollection formCollection)
        {


            if (model != null)
            {
                model.dateCompleted = DateTime.Now;
                var a = new AbstractRequestInfo[] { model.ITInfo, model.TelephoneInfo, model.RoleSAPInfo, model.PMOInfo, model.PMOCatsInfo, model.PMODMSInfo, model.PMONPDIInfo, model.PMONESTMSInfo, model.MailCaseInfo, model.ClothesInfo, model.LockerInfo, model.IntroInfo };
                foreach (var o in a)
                {
                    if (o == null)
                        continue;
                    o.Request_Id = model.RequestModel.Id;
                    o.Date = model.dateCompleted;
                }
            }

            Request request = model.RequestModel;
            using (var db = new NespeDbContext())
            {

                //request.PersonDepartment = (from t in db.PersonDepartmentSet from r in db.RequestSet where r.PersonDepartment_Id != null && r.Id == request.Id && t.Id == r.PersonDepartment_Id select t).FirstOrDefault();
                //var personDepartment = request.PersonDepartment;
                //if (request.PersonDepartment != null)
                //{
                //    request.PersonDepartment.Person = (from t in db.PersonSet where request.PersonDepartment.Person_Id != null && t.Id == request.PersonDepartment.Person_Id select t).FirstOrDefault();
                //    request.PersonDepartment.Department = (from t in db.DepartmentSet where request.PersonDepartment.Person_Id != null && t.Id == request.PersonDepartment.Department_Id select t).FirstOrDefault();
                //    request.PersonDepartment_Id = request.PersonDepartment.Id;

                //}
                //var person = request.Person;

                //if (person != null)
                //{
                //    request.PersonDepartment.Person_Id = person.Id;
                //    model.RequestModel.Initials = person.Initials;
                //    model.RequestModel.FirstName = person.FirstName;
                //    model.RequestModel.LastName = person.LastName;
                //}
                //var department = request.Department;
                //if (department != null)
                //{
                //    request.PersonDepartment.Department_Id = department.Id;
                //}

                //ValidateModel(model);
                /// TODO Just temporary operation
                if (true || ModelState.IsValid)
                {

                    var currentRequest = (from t in db.RequestSet where t.Id == model.RequestModel.Id select t).FirstOrDefault();
                    if (currentRequest != null)
                    {
                        currentRequest.Completed = true;
                        //Last line opf this block
                        //SaveToDb(currentRequest, db);
                        //model.RequestModel = currentRequest;
                    }
                    if (model.ITInfo != null)
                    {
                        SaveToDb(db.ITRequestSet, model.ITInfo, db);
                        request.SendInfo(model.ITInfo);
                    }
                    if (model.TelephoneInfo != null)
                    {
                        SaveToDb(db.TelephoneRequestSet, model.TelephoneInfo, db);
                        request.SendInfo(model.TelephoneInfo);
                    }
                    if (model.PMOInfo != null)
                    {
                        SaveToDb(db.PMORequestSet, model.PMOInfo, db);
                        request.SendInfo(model.PMOInfo);
                    }
                    if (model.PMOCatsInfo != null)
                    {
                        SaveToDb(db.PMOCatsSet, model.PMOCatsInfo, db);
                    }
                    if (model.PMODMSInfo != null)
                    {
                        SaveToDb(db.PMODMSSet, model.PMODMSInfo, db);
                    }
                    if (model.PMONPDIInfo != null)
                    {
                        SaveToDb(db.PMONPDISet, model.PMONPDIInfo, db);
                    }
                    if (model.PMONESTMSInfo != null)
                    {
                        SaveToDb(db.PMONESTMSSet, model.PMONESTMSInfo, db);
                    }
                    if (model.MailCaseInfo != null)
                    {
                        SaveToDb(db.MailCaseRequestSet, model.MailCaseInfo, db);
                        request.SendInfo(model.MailCaseInfo);
                    }
                    if (model.ClothesInfo != null)
                    {
                        SaveToDb(db.ClothesRequestSet, model.ClothesInfo, db);
                        request.SendInfo(model.ClothesInfo);
                    }
                    if (model.LockerInfo != null)
                    {
                        SaveToDb(db.LockerRequestSet, model.LockerInfo, db);
                        request.SendInfo(model.LockerInfo);
                    }
                    if (model.IntroInfo != null)
                    {
                        SaveToDb(db.IntroRequestSet, model.IntroInfo, db);
                        request.SendInfo(model.IntroInfo);
                    }
                    if (model.RoleSAPInfo != null)
                    {
                        SaveToDb(db.RoleSAPRequestSet, model.RoleSAPInfo, db);
                        request.SendInfo(model.RoleSAPInfo);
                    }
                    db.SaveChanges();

                    //WebMailHelper.SendEMail(model.RequestModel);
                    //SaveToDb(dst);
                    //RequestSet.Add(selected);
                    //return View("Confirmation", dst);
                    return this.RedirectToAction("Index");
                    //return Index();

                }
                else
                {

                    return View(model);



                }


            }


        }
        //
        // GET: /FinishIt/
        [HttpGet]
        public ActionResult FinishIt(long id)
        {
            
            
            using (var db = new NespeDbContext())
            {
                var dr = (from t in db.RequestSet where t.Id == id select t).FirstOrDefault();
                if (dr != null) {
                    dr.IsFinished = true;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }
        //
        // GET: /Edit/
        [HttpGet]
        public ActionResult Edit(long id)
        {
            ViewBag.Message = "My requests";
            UpdateRequestList(RequestSet);
            using (var db = new NespeDbContext())
            {
                var requestData = (from t in RequestSet where t.Id == id select t).FirstOrDefault();
                var dateCompleted = DateTime.Now.AddDays(-1);
                var model = new ArrivalEditFormulaireModel
                {
                    Id = id,
                    RequestModel = requestData,
                    dateCompleted = dateCompleted,
                    ITInfo = GetByRequestId(id, db.ITRequestSet, new ITRequestInfo { Request_Id = id, Date = dateCompleted }),
                    TelephoneInfo = GetByRequestId(id, db.TelephoneRequestSet, new TelephoneRequestInfo { Request_Id = id, Date = dateCompleted }),
                    RoleSAPInfo = GetByRequestId(id, db.RoleSAPRequestSet, new RoleSAPRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMOInfo = GetByRequestId(id, db.PMORequestSet, new PMORequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMOCatsInfo = GetByRequestId(id, db.PMOCatsSet, new PMOCatsRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMODMSInfo = GetByRequestId(id, db.PMODMSSet, new PMODMSRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMONPDIInfo = GetByRequestId(id, db.PMONPDISet, new PMONPDIRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMONESTMSInfo = GetByRequestId(id, db.PMONESTMSSet, new PMONESTMSRequestInfo { Request_Id = id, Date = dateCompleted }),
                    MailCaseInfo = GetByRequestId(id, db.MailCaseRequestSet, new MailCaseRequestInfo { Request_Id = id, Date = dateCompleted }),
                    ClothesInfo = GetByRequestId(id, db.ClothesRequestSet, new ClothesRequestInfo { Request_Id = id, Date = dateCompleted }),
                    LockerInfo = GetByRequestId(id, db.LockerRequestSet, new LockerRequestInfo { Request_Id = id, Date = dateCompleted }),
                    IntroInfo = GetByRequestId(id, db.IntroRequestSet, new IntroRequestInfo { Request_Id = id, Date = dateCompleted }),
                };

                if (string.IsNullOrWhiteSpace(model.ITInfo.Name))
                    model.ITInfo.Name = "My ticket";
                if (!string.IsNullOrWhiteSpace(model.ITInfo.Name))
                    model.ITInfo.StaticDescription = model.ITInfo.Name;
                model.ExternalyManagedInfoList = FillByRequestId(id, db.ExternalyManagedRequestSet, model.ExternalyManagedInfoList);

                if (model.ExternalyManagedInfoList == null || model.ExternalyManagedInfoList.Count < 1)
                {
                    model.BeforeInCommingInitializeExternalyManagedInfoList();
                    model.AfterInCommingInitializeExternalyManagedInfoList();
                    foreach (var o in model.ExternalyManagedInfoList)
                        if (o != null)
                        {
                            o.Date = dateCompleted;
                            o.Request_Id = id;
                        }
                    SaveToDb(db.ExternalyManagedRequestSet, model.ExternalyManagedInfoList);
                    db.SaveChanges();
                }



                return View(model);
            }

        }


        //
        // GET: /Edit/
        [HttpPost]
        public ActionResult Edit(ArrivalEditFormulaireModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var id = model.RequestModel.Id;
                    var ITInfo = GetByRequestId(id, db.ITRequestSet, model.ITInfo);
                    var TelephoneInfo = GetByRequestId(id, db.TelephoneRequestSet, model.TelephoneInfo);
                    var RoleSAPInfo = GetByRequestId(id, db.RoleSAPRequestSet, model.RoleSAPInfo);
                    var PMOInfo = GetByRequestId(id, db.PMORequestSet, model.PMOInfo);
                    var PMOCats = GetByRequestId(id, db.PMOCatsSet, model.PMOCatsInfo);
                    var PMODMS = GetByRequestId(id, db.PMODMSSet, model.PMODMSInfo);
                    var PMONPDI = GetByRequestId(id, db.PMONPDISet, model.PMONPDIInfo);
                    var PMONESTMS = GetByRequestId(id, db.PMONESTMSSet, model.PMONESTMSInfo);
                    var MailCaseInfo = GetByRequestId(id, db.MailCaseRequestSet, model.MailCaseInfo);
                    var ClothesInfo = GetByRequestId(id, db.ClothesRequestSet, model.ClothesInfo);
                    var LockerInfo = GetByRequestId(id, db.LockerRequestSet, model.LockerInfo);
                    var IntroInfo = GetByRequestId(id, db.IntroRequestSet, model.IntroInfo);
                    var a = new AbstractRequestInfo[] { model.ITInfo, model.TelephoneInfo, model.RoleSAPInfo, model.PMOInfo, model.PMOCatsInfo, model.PMODMSInfo, model.PMONPDIInfo, model.PMONESTMSInfo, model.MailCaseInfo, model.ClothesInfo, model.LockerInfo, model.IntroInfo };

                    WebMailHelper.ValidateIt(ITInfo, Server.MapPath("TransferIt"+ @"\SM Ticket Detail List.xml"));
                    foreach (var o in a)
                    {
                        o.Date = DateTime.Now;
                        if (!o.IsValidated)
                            continue;
                        o.Executor = base.User.Identity.Name;

                        

                    }
					if (model.ExternalyManagedInfoList != null)
					{
						foreach (var o in model.ExternalyManagedInfoList)
						{
							if (o == null)
								continue;
							o.Request_Id = model.RequestModel.Id;
							o.Executor = base.User.Identity.Name;
							o.Date = model.dateCompleted;
						}
					}
					
                    WebMailHelper.ValidateIt(model.ITInfo, Nespe.Properties.Settings.Default.IT_VALIDATION_URL, string.Format("[NESPE] {0} Newcomer Arrival", model.ITInfo.Request_Id));
                    AbstractRequestInfo.Copy(ITInfo, model.ITInfo, true);
                    AbstractRequestInfo.Copy(TelephoneInfo, model.TelephoneInfo, true);
                    AbstractRequestInfo.Copy(RoleSAPInfo, model.RoleSAPInfo, true);
                    AbstractRequestInfo.Copy(PMOInfo, model.PMOInfo, true);
                    AbstractRequestInfo.Copy(PMOCats, model.PMOCatsInfo, true);
                    AbstractRequestInfo.Copy(PMODMS, model.PMODMSInfo, true);
                    AbstractRequestInfo.Copy(PMONPDI, model.PMONPDIInfo, true);
                    AbstractRequestInfo.Copy(PMONESTMS, model.PMONESTMSInfo, true);
                    AbstractRequestInfo.Copy(MailCaseInfo, model.MailCaseInfo, true);
                    AbstractRequestInfo.Copy(ClothesInfo, model.ClothesInfo, true);
                    AbstractRequestInfo.Copy(LockerInfo, model.LockerInfo, true);
                    AbstractRequestInfo.Copy(IntroInfo, model.IntroInfo, true);

                    if (model.ExternalyManagedInfoList == null)
                    {
                        //model.ExternalyManagedInfoList = new List<ExternalyManagedRequestInfo>();
                        bool bIsEnd = false;
                        for (int i = 0; bIsEnd == false; i++)
                        {
                            if (model.ExternalyManagedInfoList == null)
                                model.ExternalyManagedInfoList = new List<ExternalyManagedRequestInfo>();
                            var o = new ExternalyManagedRequestInfo();
                            base.UpdateModel(o, "ExternalyManagedInfoList.[" + i + "]", formCollection.ToValueProvider());
                            if (o.Id > 0)
                                model.ExternalyManagedInfoList.Add(o);
                            else
                                bIsEnd = true;
                        }
                    }
                    if (model.ExternalyManagedInfoList != null)
                    {
                        foreach (var o in model.ExternalyManagedInfoList)
                        {
                            o.Date = DateTime.Now;
                            if (!o.IsValidated)
                                continue;
                            o.Executor = base.User.Identity.Name;


                        }
                        SaveToDb(db.ExternalyManagedRequestSet, model.ExternalyManagedInfoList);
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);

        }
		


        //
        // GET: /Edit/
        [HttpGet]
        public ActionResult EditDepart(long id)
        {
            ViewBag.Message = "My requests";
            UpdateRequestList(RequestSet);
            using (var db = new NespeDbContext())
            {
                var requestData = (from t in RequestSet where t.Id == id select t).FirstOrDefault();
                var dateCompleted = DateTime.Now.AddDays(-1);
                var model = new DepartureEditFormulaireModel
                {
                    Id = id,
                    RequestModel = requestData,
                    dateCompleted = dateCompleted,
                    ITInfo = GetByRequestId(id, db.ITRequestSet, new ITRequestInfo { Request_Id = id, Date = dateCompleted }),
                    TelephoneInfo = GetByRequestId(id, db.TelephoneRequestSet, new TelephoneRequestInfo { Request_Id = id, Date = dateCompleted }),
                    RoleSAPInfo = GetByRequestId(id, db.RoleSAPRequestSet, new RoleSAPRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMOInfo = GetByRequestId(id, db.PMORequestSet, new PMORequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMOCatsInfo = GetByRequestId(id, db.PMOCatsSet, new PMOCatsRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMODMSInfo = GetByRequestId(id, db.PMODMSSet, new PMODMSRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMONPDIInfo = GetByRequestId(id, db.PMONPDISet, new PMONPDIRequestInfo { Request_Id = id, Date = dateCompleted }),
                    PMONESTMSInfo = GetByRequestId(id, db.PMONESTMSSet, new PMONESTMSRequestInfo { Request_Id = id, Date = dateCompleted }),
                    MailCaseInfo = GetByRequestId(id, db.MailCaseRequestSet, new MailCaseRequestInfo { Request_Id = id, Date = dateCompleted }),
                    ClothesInfo = GetByRequestId(id, db.ClothesRequestSet, new ClothesRequestInfo { Request_Id = id, Date = dateCompleted }),
                    LockerInfo = GetByRequestId(id, db.LockerRequestSet, new LockerRequestInfo { Request_Id = id, Date = dateCompleted }),
                    IntroInfo = GetByRequestId(id, db.IntroRequestSet, new IntroRequestInfo { Request_Id = id, Date = dateCompleted }),
                };
                if (string.IsNullOrWhiteSpace(model.ITInfo.Name))
                    model.ITInfo.Name = "My ticket";
                if (!string.IsNullOrWhiteSpace(model.ITInfo.Name))
                    model.ITInfo.StaticDescription = model.ITInfo.Name;
                model.ExternalyManagedInfoList = FillByRequestId(id, db.ExternalyManagedRequestSet, model.ExternalyManagedInfoList);
                if (model.ExternalyManagedInfoList == null || model.ExternalyManagedInfoList.Count < 1)
                {
                    model.DepartureInitializeExternalyManagedInfoList();
                    foreach (var o in model.ExternalyManagedInfoList)
                        if (o != null)
                        {
                            o.Date = dateCompleted;
                            o.Request_Id = id;
                        }
                    SaveToDb(db.ExternalyManagedRequestSet, model.ExternalyManagedInfoList);
                    db.SaveChanges();
                }

                return View(model);
            }

        }
        //
        // GET: /Edit/
        [HttpPost]
        public ActionResult EditDepart(DepartureEditFormulaireModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var id = model.RequestModel.Id;
                    var ITInfo = GetByRequestId(id, db.ITRequestSet, model.ITInfo);
                    var TelephoneInfo = GetByRequestId(id, db.TelephoneRequestSet, model.TelephoneInfo);
                    var RoleSAPInfo = GetByRequestId(id, db.RoleSAPRequestSet, model.RoleSAPInfo);
                    var PMOInfo = GetByRequestId(id, db.PMORequestSet, model.PMOInfo);
                    var PMOCatsInfo = GetByRequestId(id, db.PMOCatsSet, model.PMOCatsInfo);
                    var PMODMSInfo = GetByRequestId(id, db.PMODMSSet, model.PMODMSInfo);
                    var PMONPDIInfo = GetByRequestId(id, db.PMONPDISet, model.PMONPDIInfo);
                    var PMONESTMSInfo = GetByRequestId(id, db.PMONESTMSSet, model.PMONESTMSInfo);
                    var MailCaseInfo = GetByRequestId(id, db.MailCaseRequestSet, model.MailCaseInfo);
                    var ClothesInfo = GetByRequestId(id, db.ClothesRequestSet, model.ClothesInfo);
                    var LockerInfo = GetByRequestId(id, db.LockerRequestSet, model.LockerInfo);
                    var IntroInfo = GetByRequestId(id, db.IntroRequestSet, model.IntroInfo);
                    var a = new AbstractRequestInfo[] { model.ITInfo, model.TelephoneInfo, model.RoleSAPInfo, model.PMOInfo, model.PMOCatsInfo, model.PMODMSInfo, model.PMONPDIInfo, model.PMONESTMSInfo, model.MailCaseInfo, model.ClothesInfo, model.LockerInfo, model.IntroInfo };
                    WebMailHelper.ValidateIt(ITInfo, Server.MapPath("TransferIt" + @"\SM Ticket Detail List.xml"));
                    foreach (var o in a)
                    {
                        o.Date = DateTime.Now;
                        if (!o.IsValidated)
                            continue;
                        o.Executor = base.User.Identity.Name;
                        

                    }
                    WebMailHelper.ValidateIt(model.ITInfo, Nespe.Properties.Settings.Default.IT_VALIDATION_URL, string.Format("[NESPE] {0} Newcomer", model.ITInfo.Request_Id));
                    AbstractRequestInfo.Copy(ITInfo, model.ITInfo, true);
                    AbstractRequestInfo.Copy(TelephoneInfo, model.TelephoneInfo, true);
                    AbstractRequestInfo.Copy(RoleSAPInfo, model.RoleSAPInfo, true);
                    AbstractRequestInfo.Copy(PMOInfo, model.PMOInfo, true); 
                    AbstractRequestInfo.Copy(PMOCatsInfo, model.PMOCatsInfo, true);
                    AbstractRequestInfo.Copy(PMODMSInfo, model.PMODMSInfo, true);
                    AbstractRequestInfo.Copy(PMONPDIInfo, model.PMONPDIInfo, true);
                    AbstractRequestInfo.Copy(PMONESTMSInfo, model.PMONESTMSInfo, true);
                    AbstractRequestInfo.Copy(MailCaseInfo, model.MailCaseInfo, true);
                    AbstractRequestInfo.Copy(ClothesInfo, model.ClothesInfo, true);
                    AbstractRequestInfo.Copy(LockerInfo, model.LockerInfo, true);
                    AbstractRequestInfo.Copy(IntroInfo, model.IntroInfo, true);
                    if (model.ExternalyManagedInfoList == null) {
                        //model.ExternalyManagedInfoList = new List<ExternalyManagedRequestInfo>();
                        bool bIsEnd = false;
                        for (int i = 0; bIsEnd == false; i++) {
                            if (model.ExternalyManagedInfoList == null)
                                model.ExternalyManagedInfoList = new List<ExternalyManagedRequestInfo>();
                            var o=new ExternalyManagedRequestInfo();
                            base.UpdateModel(o, "ExternalyManagedInfoList.["+i+"]", formCollection.ToValueProvider());
                            if (o.Id > 0)
                                model.ExternalyManagedInfoList.Add(o);
                            else
                                bIsEnd = true;
                        }
                    }
                    if (model.ExternalyManagedInfoList != null)
                    {
                        foreach (var o in model.ExternalyManagedInfoList)
                        {
                            o.Date = DateTime.Now;
                            if (!o.IsValidated)
                                continue;
                            o.Executor = base.User.Identity.Name;
                            

                        }
                        SaveToDb(db.ExternalyManagedRequestSet, model.ExternalyManagedInfoList);
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);

        }
        [HttpGet]
        public ActionResult CloseInfo(long id, long Request_Id, string InfoType) { 
            using(var db=new NespeDbContext()){
                var requestModel=(from t in db.RequestSet where t.Id==Request_Id select t).FirstOrDefault();
                var fullInfoTypeName=typeof(ClothesRequestInfo).FullName.Replace(typeof(ClothesRequestInfo).Name, InfoType);
                var type=typeof(ClothesRequestInfo).Assembly.GetType(fullInfoTypeName, false, true);
                if (type != null) {
                    AbstractRequestInfo o = null;// type.GetConstructor(new Type[] { }).Invoke(new object[] { });
                    if (type == typeof(ClothesRequestInfo))
                    {
                        o = (from t in db.ClothesRequestSet where t.Id == id select t).FirstOrDefault();
                    }
                    if (type == typeof(ExternalyManagedRequestInfo))
                    {
                        o = (from t in db.ExternalyManagedRequestSet where t.Id == id select t).FirstOrDefault();
                    }
                    else if (type == typeof(ITRequestInfo))
                    {
                        o = (from t in db.ITRequestSet where t.Id == id select t).FirstOrDefault();
                    }
                    else if (type == typeof(TelephoneRequestInfo)) { o = (from t in db.TelephoneRequestSet where t.Id == id select t).FirstOrDefault(); }
                    else if (type == typeof(RoleSAPRequestInfo)) { o = (from t in db.RoleSAPRequestSet where t.Id == id select t).FirstOrDefault(); }
                    else if (type == typeof(PMORequestInfo)) { o = (from t in db.PMORequestSet where t.Id == id select t).FirstOrDefault(); }
                    else if (type == typeof(PMOCatsRequestInfo)) { o = (from t in db.PMOCatsSet where t.Id == id select t).FirstOrDefault(); }
                    else if (type == typeof(PMODMSRequestInfo)) { o = (from t in db.PMODMSSet where t.Id == id select t).FirstOrDefault(); }
                    else if (type == typeof(PMONPDIRequestInfo)) { o = (from t in db.PMONPDISet where t.Id == id select t).FirstOrDefault(); }
                    else if (type == typeof(PMONESTMSRequestInfo)) { o = (from t in db.PMONESTMSSet where t.Id == id select t).FirstOrDefault(); }
                    else if (type == typeof(MailCaseRequestInfo)) { o = (from t in db.MailCaseRequestSet where t.Id == id select t).FirstOrDefault(); }
                    else if (type == typeof(LockerRequestInfo)) { o = (from t in db.LockerRequestSet where t.Id == id select t).FirstOrDefault(); }
                    else if (type == typeof(IntroRequestInfo)) { o = (from t in db.IntroRequestSet where t.Id == id select t).FirstOrDefault(); }



                    if (o != null)
                    {
                        o.IsValidated = true;
                        o.Date = DateTime.Now;
                        o.Executor = base.User.Identity.Name;
                    }
                    db.SaveChanges();
                }
                if(requestModel!=null && requestModel.Kind==RequestKindEnum.Departure)
                    return RedirectToAction("EditDepart", new { id = Request_Id });
                else
                    return RedirectToAction("Edit", new { id = Request_Id });
            }
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
