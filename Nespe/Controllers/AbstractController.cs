using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                        new Request { SurnameNC = "Employee 1", NameNC = "New Employee 1" }, 
                        new Request { SurnameNC = "Employee 2", NameNC = "New Employee 2" }, 
                        new Request { SurnameNC = "Employee 3", NameNC = "New Employee 3" } 
                   };

                }
                return _RequestSet;
            }
        }
        protected virtual void SaveToDb(Request request)
        {
            using (var db = new NespeEntityContainer())
            {
                var o = (from t in db.Requests where t.NameNC == request.NameNC && t.SurnameNC == request.SurnameNC select t).FirstOrDefault();
                if (o != null)
                {
                    request.Id = o.Id;

                }
                RequestEntity e = request;
                if (e.Id <= 0)
                {
                    db.Requests.AddObject(request);
                }
                else
                {
                    try
                    {
                        db.Attach(e);
                    }
                    catch (Exception) { }

                }

                db.SaveChanges();
            }
            
        }

        public static void UpdateRequestList(List<Request> rl)
        {
            using (var db = new NespeEntityContainer())
            {
                var q = (from t in db.Requests select t);
                if (q.Count() > 0)
                {
                    rl.Clear();
                    foreach (var dr in q)
                    {
                        rl.Add(dr);
                    }
                }
            }
        }
    }
}