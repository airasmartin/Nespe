using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Application.WebSite.Models;
using Nespe.Data.Entities;

namespace Nespe.Application.WebSite.Controllers
{
    public class PersonTaskRequestController : AbstractEntityController<PersonTaskRequest, PersonTaskRequestListModel, PersonTaskRequestItemModel>
    {
        //
        // GET: /PersonTaskRequest/

        public ActionResult Index()
        {
            var model = CreateListModel(ControllerActionEnum.List);
            model.ItemList = FindBy();
            return View(model);
        }

        //
        // GET: /PersonTaskRequest/Details/5

        public ActionResult Details(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);

            var selected = FindById(id);
            var db = CurrentDataContext();
            model.ItemSelected = selected;
            return View(model);
        }

        //
        // GET: /PersonTaskRequest/Create

        public ActionResult Create()
        {
            var model = CreateItemModel(ControllerActionEnum.Create);
            return View(model);
        }

        //
        // POST: /PersonTaskRequest/Create

        [HttpPost]
        public ActionResult Create(PersonTaskRequestItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    db.PersonTaskRequestSet.Add(selected);
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
        // GET: /PersonTaskRequest/Edit/5

        public ActionResult Edit(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /PersonTaskRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PersonTaskRequestItemModel model, FormCollection collection)
        {
            try
            {
                var selected = model.ItemSelected;
                using (var db = CurrentDataContext())
                {
                    selected = db.PersonTaskRequestSet.Attach(selected);
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
        // GET: /PersonTaskRequest/Delete/5

        public ActionResult Delete(int id)
        {
            var model = CreateItemModel(ControllerActionEnum.Details);
            model.ItemSelected = FindById(id);
            return View(model);
        }

        //
        // POST: /PersonTaskRequest/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, PersonTaskRequestItemModel model, FormCollection collection)
        {
            try
            {

                using (var db = CurrentDataContext())
                {
                    var selected = FindById(id);
                    selected = db.PersonTaskRequestSet.Remove(selected);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        protected override PersonTaskRequestListModel CreateListModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new PersonTaskRequestListModel { };
        }

        protected override PersonTaskRequestItemModel CreateItemModel(ControllerActionEnum action = ControllerActionEnum.Unknown)
        {
            return new PersonTaskRequestItemModel { };
        }
        public override PersonTaskRequest FindById(int id)
        {
            return (from t in FindBy() where t.Id == id select t).FirstOrDefault();
        }
        public override IQueryable<PersonTaskRequest> FindBy()
        {
            return (from t in CurrentDataContext().PersonTaskRequestSet orderby t.Rank, t.Name select t);
        }
    }
}
