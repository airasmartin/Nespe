using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Application.WebSite.Models;
using Nespe.Data.Context;
using Nespe.Data.Entities;

namespace Nespe.Application.WebSite.Controllers
{
    public class TransfertPersonTaskRequestController : AbstractPersonTaskRequestController<TransfertPersonTaskRequest, TransfertPersonTaskRequestListModel, TransfertPersonTaskRequestItemModel>
    {
        //
        // GET: /TransfertPersonTaskRequest/

        public ActionResult Index()
        {
            var model = CreateListModel(ControllerActionEnum.List);
            model.ItemList = FindBy();
            return View(model);
        }

        //
        // GET: /TransfertPersonTaskRequest/Details/5

        public ActionResult Details(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);

            var selected = FindById(id);
            var db = CurrentDataContext();
            model.ItemSelected = selected;
            return View(model);
        }

        //
        // GET: /TransfertPersonTaskRequest/Create

        public ActionResult Create()
        {
            var model = CreateItemModel(ControllerActionEnum.Create);
            return View(model);
        }

        //
        // POST: /TransfertPersonTaskRequest/Create

        [HttpPost]
        public ActionResult Create(TransfertPersonTaskRequestItemModel model, FormCollection collection)
        {
            try
            {
                if (base.ModelState.IsValid)
                {
                    var selected = model.ItemSelected;
                    using (var db = CurrentDataContext())
                    {
                        selected.Department = (from t in db.DepartmentSet where t.Id == selected.Department.Id select t).First();
                        db.TransfertPersonTaskRequestSet.Add(selected);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                base.ModelState.AddModelError("Edit", ex);
            }
            return View(model);
        }

        //
        // GET: /TransfertPersonTaskRequest/Edit/5

        public ActionResult Edit(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /TransfertPersonTaskRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, TransfertPersonTaskRequestItemModel model, FormCollection collection)
        {
            try
            {
                if (base.ModelState.IsValid)
                {
                    var selected = model.ItemSelected;
                    using (var db = CurrentDataContext())
                    {
                        selected = db.TransfertPersonTaskRequestSet.Attach(selected);
                        selected.Department = (from t in db.DepartmentSet where t.Id == selected.Department.Id select t).First();
                        db.Entry(selected).State = System.Data.EntityState.Modified;
                        //db.ChangeTracker.DetectChanges();
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                base.ModelState.AddModelError("Edit", ex);
            }
            return View(model);
        }

        //
        // GET: /TransfertPersonTaskRequest/Delete/5

        public ActionResult Delete(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /TransfertPersonTaskRequest/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, TransfertPersonTaskRequestItemModel model, FormCollection collection)
        {
            try
            {

                using (var db = CurrentDataContext())
                {
                    var selected = FindById(id);
                    selected = db.TransfertPersonTaskRequestSet.Remove(selected);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        protected override TransfertPersonTaskRequestListModel CreateListModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new TransfertPersonTaskRequestListModel { };
        }

        protected override TransfertPersonTaskRequestItemModel CreateItemModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            
            return new TransfertPersonTaskRequestItemModel
            {
                ItemSelected = new TransfertPersonTaskRequest
                {
                    Person = new Person { },
                    Department = new Department { },
                    Date= System.DateTime.Now,
                    StartDate = System.DateTime.Now.AddDays(30),
                }
                
            };
        }
        public override TransfertPersonTaskRequest FindById(int id)
        {
            return (from t in FindBy() where t.Id == id select t).FirstOrDefault();
        }
        public override IQueryable<TransfertPersonTaskRequest> FindBy()
        {
            return (from t in CurrentDataContext().TransfertPersonTaskRequestSet orderby t.Rank, t.Name select t);
        }
    }
}
