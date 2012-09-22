using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Models;

namespace Nespe.Controllers
{
    public class AbstractController : Controller
    {
        private List<Request> _RequestSet = null;
        public List<Request> RequestSet
        {
            get
            {
                if (_RequestSet != null)
                    return _RequestSet;
                _RequestSet = Session["_RequestSet"] as List<Request>;
                if (_RequestSet == null)
                {

                    Session["_RequestSet"] = _RequestSet = new List<Request> { 
                        new Request { Department=new Department{Name="Department_Id 1"}, Person=new Person {FirstName = "Employee 1", LastName = "New Employee 1" }},
                        new Request { Department=new Department{Name="Department_Id 1"}, Person=new Person {FirstName = "Employee 2", LastName = "New Employee 3" }},
                        new Request { Department=new Department{Name="Department_Id 2"}, Person=new Person {FirstName = "Employee 3", LastName = "New Employee 4" }},
                        new Request { Department=new Department{Name="Department_Id 3"}, Person=new Person {FirstName = "Employee 4", LastName = "New Employee 5" }},
                        new Request { Department=new Department{Name="Department_Id 2"}, Person=new Person {FirstName = "Employee 5", LastName = "New Employee 6" }},
                   };

                }
                return _RequestSet;
            }
        }
        protected virtual void SaveToDb(Request request)
        {
            //using (var db = new NespeEntityContainer())
            //{
            //    //var o = (from t in db.Requests where t.LastName == request.LastName && t.FirstName == request.FirstName select t).FirstOrDefault();
            //    //if (o != null)
            //    //{
            //    //    request.Id = o.Id;

            //    //}
            //    //RequestEntity e = request;
            //    //if (e.Id <= 0)
            //    //{
            //    //    db.Requests.AddObject(request);
            //    //}
            //    //else
            //    //{
            //    //    try
            //    //    {
            //    //        db.Attach(e);
            //    //    }
            //    //    catch (Exception) { }

            //    //}

            //    //db.SaveChanges();
            //}
            
        }

        public static void UpdateRequestList(List<Request> rl)
        {
            //using (var db = new NespeEntityContainer())
            //{
            //    //var q = (from t in db.Requests select t);
            //    //if (q.Count() > 0)
            //    //{
            //    //    rl.Clear();
            //    //    foreach (var dr in q)
            //    //    {
            //    //        rl.Add(dr);
            //    //    }
            //    //}
            //}
        }
    }
}