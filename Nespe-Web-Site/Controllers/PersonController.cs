using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Application.WebSite.Models;
using Nespe.Data.Entities;

namespace Nespe.Application.WebSite.Controllers
{
    public class PersonController : AbstractEntityController<Person, PersonListModel, PersonItemModel>
    {
        //
        // GET: /Person/

        public ActionResult Index()
        {
            var model = CreateListModel(ControllerActionEnum.List);
            model.ItemList = FindBy();
            return View(model);
        }

        //
        // GET: /Person/Details/5

        public ActionResult Details(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);

            var selected = FindById(id);
            var db = CurrentDataContext();
            model.DepartmentList = new PersonDepartmentListModel { ItemList = (from t in db.PersonDepartmentSet where t.Person.Id == id select t).AsQueryable() };
            var departmentList = new List<Department>();

            var dl = db.DepartmentSet.ToList().Except((from t in db.PersonDepartmentSet where t.Person.Id == id select t.Department));
            foreach (var p in dl)
                departmentList.Add(p);
            model.AvailableDepartmentList = new DepartmentListModel { ItemList = departmentList.AsQueryable() };
            model.ItemSelected=selected;
            return View(model);
        }

        //
        // GET: /Person/Create

        public ActionResult Create()
        {
            var model = CreateItemModel(ControllerActionEnum.Create);
            return View(model);
        }

        //
        // POST: /Person/Create

        [HttpPost]
        public ActionResult Create(PersonItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    db.PersonSet.Add(selected);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Person/Edit/5

        public ActionResult Edit(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /Person/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PersonItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    selected = db.PersonSet.Attach(selected);
                    db.Entry(selected).State = System.Data.EntityState.Modified;
                    db.ChangeTracker.DetectChanges();
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Person/Delete/5

        public ActionResult Delete(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /Person/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, PersonItemModel model, FormCollection collection)
        {
            try
            {
                
                using (var db = CurrentDataContext())
                {
                    var selected = FindById(id);
                    selected = db.PersonSet.Remove(selected);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        protected override PersonListModel CreateListModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new PersonListModel { };
        }

        protected override PersonItemModel CreateItemModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new PersonItemModel{ };
        }
        public override Person FindById(int id)
        {
            return (from t in FindBy() where t.Id == id select t).FirstOrDefault();
        }
        public override IQueryable<Person> FindBy()
        {
            return (from t in CurrentDataContext().PersonSet orderby t.FirstName, t.LastName select t);
        }
    }
}
