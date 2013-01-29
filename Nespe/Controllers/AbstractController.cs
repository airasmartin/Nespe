using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Models;
using Nespe.Context;
using System.Data.Entity;
using System.Data.Entity.Validation;

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
        public T SaveToDb<T>(DbSet<T> drc, T selected, NespeDbContext db) where T:AbstractRequestInfo
        {
            
            var dr = (from t in drc where t.Id == selected.Id select t).FirstOrDefault();
            if(dr==null)
                dr = (from t in drc where t.Request_Id == selected.Request_Id select t).FirstOrDefault();
            if (dr == null)
            {
                drc.Add(selected);
            }
            else {
                dr.Copy(selected);
            }
            return selected;
        }

        protected virtual Request SaveToDb(Request selected)
        {
            using (var db = new NespeDbContext())
            {
                selected = SaveToDb(selected, db);
                db.SaveChanges();
            }
            return selected;
        }
        protected virtual Request SaveToDb(Request selected, NespeDbContext db)
        {
            SaveToDb(selected.PersonDepartment, db);
            var dr = (from t in db.RequestSet where t.Id == selected.Id select t).FirstOrDefault();
            if (dr==null)
            {
                db.RequestSet.Add(selected);
            }
            else
            {
                
                dr.Copy(selected);
                selected = dr;
            }
            return selected;
        }
        protected virtual PersonDepartment SaveToDb(PersonDepartment selected, NespeDbContext db)
        {
            if(selected==null)
                return selected;
            if (selected.Person_Id > 0) {
                selected.Person = (from t in db.PersonSet where t.Id == selected.Department_Id select t).FirstOrDefault();
            }
            else if (selected.Person != null) {
                if (selected.Person.Id > 0 )
                {
                    selected.Person = (from t in db.PersonSet where t.Id == selected.Person.Id select t).FirstOrDefault();
                }else {
                    SaveToDb(selected.Person, db);
                }
            }
            if (selected.Person_Id > 0)
            {
                selected.Department = (from t in db.DepartmentSet where t.Id == selected.Department_Id select t).FirstOrDefault();
            }
            else if (selected.Person != null)
            {
                if (selected.Department.Id > 0)
                {
                    selected.Department = (from t in db.DepartmentSet where t.Id == selected.Department.Id select t).FirstOrDefault();
                }
                else
                {
                    SaveToDb(selected.Department, db);
                }
            }

            if (selected.Id > 0)
            {
                var dr = (from t in db.PersonDepartmentSet where t.Id == selected.Id select t).FirstOrDefault();
                dr.Copy(selected);
                selected = dr;
            }
            else
            {
                db.PersonDepartmentSet.Add(selected);
            }
            return selected;
        }
        protected virtual Person SaveToDb(Person selected, NespeDbContext db)
        {

            var dr = (from t in db.PersonSet where t.Id == selected.Id select t).FirstOrDefault();
            if (dr==null)
            {
                dr = (from t in db.PersonSet where t.FirstName == selected.FirstName && t.LastName == selected.LastName && t.Initials == selected.Initials select t).FirstOrDefault();
            }
            if (dr == null)
                db.PersonSet.Add(selected);
            else
            {
                dr.Copy(selected);
                selected = dr;
            }
            return selected;
        }
        protected virtual Department SaveToDb(Department selected, NespeDbContext db)
        {
            var dr = (from t in db.DepartmentSet where t.Id == selected.Id select t).FirstOrDefault();
            if (dr == null)
            {
                dr = (from t in db.DepartmentSet where t.Name == selected.Name select t).FirstOrDefault();
            }
            if (dr == null)
                db.DepartmentSet.Add(selected);
            else
            {
                dr.Copy(selected);
                selected = dr;
            }
            return selected;

        }
        public void AddModelError(DbEntityValidationException ex, string prefix="")
        {
            ModelState.AddModelError("EntityValidationException", ex);
            foreach (var o in ex.EntityValidationErrors)
            {
                AddModelError(o);
            }
        }
        public void AddModelError(DbEntityValidationResult ex, string prefix = "")
        {
            prefix = prefix + ex.Entry.Entity.GetType().FullName + ".ValidationError";
            ModelState.AddModelError(prefix, ex.Entry.Entity.GetType().FullName); ;
            foreach (var e in ex.ValidationErrors)
            {
                AddModelError(e, prefix );
            }
        }
        public void AddModelError(DbValidationError ex, string prefix = "")
        {
            ModelState.AddModelError(prefix + ex.PropertyName, ex.ErrorMessage);
        }
        public static void UpdateRequestList(List<Request> rl)
        {
            using (var db = new NespeDbContext())
            {
                var q = (
                    from t in db.RequestSet
                    from pd in db.PersonDepartmentSet
                    from p in db.PersonSet
                    from d in db.DepartmentSet 
                    where 
                        pd.Id == t.PersonDepartment_Id  &&
                        p.Id==pd.Person_Id &&
                        d.Id==pd.Department_Id
                    select new {Request=t, PersonDepartment=pd, Person=p, Department=d}
               );
                if (q.Count() > 0)
                {
                    rl.Clear();
                    foreach (var o in q)
                    {
                        var dr = o.Request;
                        dr.PersonDepartment = o.PersonDepartment;
                        dr.PersonDepartment.Person = o.Person;
                        dr.PersonDepartment.Department = o.Department;
                        rl.Add(dr);
                    }
                }
            }
        }
    }
}