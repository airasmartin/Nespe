using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Application.WebSite.Models;
using Nespe.Data.Entities;

namespace Nespe.Application.WebSite.Controllers
{
    public class DepartmentController : AbstractEntityController<Department, DepartmentListModel, DepartmentItemModel>
    {
        //
        // GET: /Department/

        public ActionResult Index()
        {
            var model = CreateListModel(ControllerActionEnum.List);
            model.ItemList = FindBy();
            return View(model);
        }

        //
        // GET: /Department/Details/5

        public ActionResult Details(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);

            var selected = FindById(id);
            var db = CurrentDataContext();
            var personList = new List<Person>();
            var pl = db.PersonSet.ToList().Except((from t in selected.PersonList select t.Person));
            foreach (var p in pl)
                personList.Add(p);
            model.AvailablePersonList = new PersonListModel { ItemList = personList.AsQueryable() };
            model.ItemSelected=selected;
            return View(model);
        }

        //
        // GET: /Department/Create

        public ActionResult Create()
        {
            var model = CreateItemModel(ControllerActionEnum.Create);
            return View(model);
        }

        //
        // POST: /Department/Create

        [HttpPost]
        public ActionResult Create(DepartmentItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    db.DepartmentSet.Add(selected);
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
        // GET: /Department/Edit/5

        public ActionResult Edit(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, DepartmentItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    selected = db.DepartmentSet.Attach(selected);
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
        // GET: /Department/Delete/5

        public ActionResult Delete(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /Department/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, DepartmentItemModel model, FormCollection collection)
        {
            try
            {
                
                using (var db = CurrentDataContext())
                {
                    var selected = FindById(id);
                    selected = db.DepartmentSet.Remove(selected);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        protected override DepartmentListModel CreateListModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new DepartmentListModel { };
        }

        protected override DepartmentItemModel CreateItemModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new DepartmentItemModel{ };
        }
        public override Department FindById(int id)
        {
            return (from t in FindBy() where t.Id == id select t).FirstOrDefault();
        }
        public override IQueryable<Department> FindBy()
        {
            return (from t in CurrentDataContext().DepartmentSet orderby t.Name select t);
        }
    }
}
