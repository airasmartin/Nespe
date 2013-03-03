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
    public class ArrivalPersonTaskRequestController : AbstractEntityController<ArrivalPersonTaskRequest, ArrivalPersonTaskRequestListModel, ArrivalPersonTaskRequestItemModel>
    {
        //
        // GET: /ArrivalPersonTaskRequest/

        public ActionResult Index()
        {
            var model = CreateListModel(ControllerActionEnum.List);
            model.ItemList = FindBy();
            return View(model);
        }

        //
        // GET: /ArrivalPersonTaskRequest/Details/5

        public ActionResult Details(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);

            var selected = FindById(id);
            var db = CurrentDataContext();
            model.ItemSelected = selected;
            return View(model);
        }

        //
        // GET: /ArrivalPersonTaskRequest/Create

        public ActionResult Create()
        {
            var model = CreateItemModel(ControllerActionEnum.Create);
            return View(model);
        }

        //
        // POST: /ArrivalPersonTaskRequest/Create

        [HttpPost]
        public ActionResult Create(ArrivalPersonTaskRequestItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    db.ArrivalPersonTaskRequestSet.Add(selected);
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
        // GET: /ArrivalPersonTaskRequest/Edit/5

        public ActionResult Edit(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /ArrivalPersonTaskRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ArrivalPersonTaskRequestItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    selected = db.ArrivalPersonTaskRequestSet.Attach(selected);
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
        // GET: /ArrivalPersonTaskRequest/Delete/5

        public ActionResult Delete(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /ArrivalPersonTaskRequest/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, ArrivalPersonTaskRequestItemModel model, FormCollection collection)
        {
            try
            {

                using (var db = CurrentDataContext())
                {
                    var selected = FindById(id);
                    selected = db.ArrivalPersonTaskRequestSet.Remove(selected);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        protected override ArrivalPersonTaskRequestListModel CreateListModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new ArrivalPersonTaskRequestListModel { };
        }

        protected override ArrivalPersonTaskRequestItemModel CreateItemModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            
            return new ArrivalPersonTaskRequestItemModel
            {
                ItemSelected = new ArrivalPersonTaskRequest
                {
                    Person = new Person { },
                    Department = new Department { },
                    Date= System.DateTime.Now,
                    StartDate = System.DateTime.Now.AddDays(30),
                }
                
            };
        }
        public override ArrivalPersonTaskRequest FindById(int id)
        {
            return (from t in FindBy() where t.Id == id select t).FirstOrDefault();
        }
        public override IQueryable<ArrivalPersonTaskRequest> FindBy()
        {
            return (from t in CurrentDataContext().ArrivalPersonTaskRequestSet orderby t.Rank, t.Name select t);
        }
    }
}
