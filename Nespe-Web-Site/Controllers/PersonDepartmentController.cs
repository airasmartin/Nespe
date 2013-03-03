using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Application.WebSite.Models;
using Nespe.Data.Entities;

namespace Nespe.Application.WebSite.Controllers
{
    public class PersonDepartmentController : AbstractEntityController<PersonDepartment, PersonDepartmentListModel, PersonDepartmentItemModel>
    {
        //
        // GET: /PersonDepartment/

        public ActionResult Index()
        {
            var model = CreateListModel(ControllerActionEnum.List);
            model.ItemList = FindBy();
            return View(model);
        }

        //
        // GET: /PersonDepartment/Details/5

        public ActionResult Details(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);

            var selected = FindById(id);
            var db = CurrentDataContext();
            model.ItemSelected = selected;
            return View(model);
        }

        //
        // GET: /PersonDepartment/Create

        public ActionResult Create()
        {
            var model = CreateItemModel(ControllerActionEnum.Create);
            return View(model);
        }

        //
        // POST: /PersonDepartment/Create

        [HttpPost]
        public ActionResult Create(PersonDepartmentItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    selected.Department=(from t in db.DepartmentSet where t.Id==selected.Department.Id select t).First();
                    selected.Person = (from t in db.PersonSet where t.Id == selected.Person.Id select t).First();
                    db.PersonDepartmentSet.Add(selected);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                this.ModelState.AddModelError("Creation", ex);
                return View(model);
            }
        }

        //
        // GET: /PersonDepartment/Edit/5

        public ActionResult Edit(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /PersonDepartment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PersonDepartmentItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    selected = db.PersonDepartmentSet.Attach(selected);
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
        // GET: /PersonDepartment/Delete/5

        public ActionResult Delete(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /PersonDepartment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, PersonDepartmentItemModel model, FormCollection collection)
        {
            try
            {

                using (var db = CurrentDataContext())
                {
                    var selected = FindById(id);
                    selected = db.PersonDepartmentSet.Remove(selected);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        protected override PersonDepartmentListModel CreateListModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new PersonDepartmentListModel { };
        }

        protected override PersonDepartmentItemModel CreateItemModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new PersonDepartmentItemModel { };
        }
        public override PersonDepartment FindById(int id)
        {
            return (from t in FindBy() where t.Id == id select t).FirstOrDefault();
        }
        public override IQueryable<PersonDepartment> FindBy()
        {
            return (from t in CurrentDataContext().PersonDepartmentSet orderby t.Department.Name, t.Role, t.Person.FirstName select t);
        }
    }
}
