using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Application.WebSite.Models;
using Nespe.Data.Entities;

namespace Nespe.Application.WebSite.Controllers
{
    public class TaskRequestController : AbstractEntityController<TaskRequest, TaskRequestListModel, TaskRequestItemModel>
    {
        //
        // GET: /Task/

        public ActionResult Index()
        {
            var model = CreateListModel(ControllerActionEnum.List);
            model.ItemList = FindBy();
            return View(model);
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);

            var selected = FindById(id);
            var db = CurrentDataContext();
            model.ItemSelected = selected;
            return View(model);
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            var model = CreateItemModel(ControllerActionEnum.Create);
            return View(model);
        }

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(TaskRequestItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    db.TaskRequestSet.Add(selected);
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
        // GET: /Task/Edit/5

        public ActionResult Edit(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, TaskRequestItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    selected = db.TaskRequestSet.Attach(selected);
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
        // GET: /Task/Delete/5

        public ActionResult Delete(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /Task/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, TaskRequestItemModel model, FormCollection collection)
        {
            try
            {

                using (var db = CurrentDataContext())
                {
                    var selected = FindById(id);
                    selected = db.TaskRequestSet.Remove(selected);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        protected override TaskRequestListModel CreateListModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new TaskRequestListModel { };
        }

        protected override TaskRequestItemModel CreateItemModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new TaskRequestItemModel { };
        }
        public override TaskRequest FindById(int id)
        {
            return (from t in FindBy() where t.Id == id select t).FirstOrDefault();
        }
        public override IQueryable<TaskRequest> FindBy()
        {
            return (from t in CurrentDataContext().TaskRequestSet orderby t.Rank, t.Name select t);
        }
    }
}
