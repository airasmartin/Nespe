using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Models;
using Nespe.Context;

namespace Nespe.Controllers
{
    public class RequestTypeController : Controller
    {
        #region Index

        //
        // GET: /RequestType/

        public ActionResult Index()
        {
            using (var db = new NespeDbContext())
            {
                var drc = db.RequestTypeSet;
                var model = new RequestTypeModel { };

                model.Items = (from t in drc select t).ToList();
                return View(model);
            }

        }
        #endregion//Index

        #region Delete

        [HttpGet]
        public ActionResult Delete(long Id)
        {
            using (var db = new NespeDbContext())
            {
                var drc = db.RequestTypeSet;
                var dr = (from t in drc where t.Id == Id select t).FirstOrDefault();
                if (dr == null)
                {
                    base.ModelState.AddModelError("Action.Delete.Invalid.Id", "Invalid Id");
                    return RedirectToAction("Index");
                }
                var model = new RequestTypeModel { Selected = dr };
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult Delete(RequestTypeModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var drc = db.RequestTypeSet;
                    var dr = (from t in drc where t.Id == model.Selected.Id select t).FirstOrDefault();
                    if (dr == null)
                    {
                        base.ModelState.AddModelError("Action.Delete.Invalid.Id", "Invalid Id");
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion//Delete

        #region Details

        [HttpGet]
        public ActionResult Details(long Id)
        {
            using (var db = new NespeDbContext())
            {
                var drc = db.RequestTypeSet;
                var dr = (from t in drc where t.Id == Id select t).FirstOrDefault();
                if (dr == null)
                {
                    base.ModelState.AddModelError("Action.Details.Invalid.Id", "Invalid Id");
                    return RedirectToAction("Index");
                }
                var model = new RequestTypeModel { Selected = dr };
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult Details(RequestTypeModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var drc = db.RequestTypeSet;
                    var dr = (from t in drc where t.Id == model.Selected.Id select t).FirstOrDefault();
                    if (dr == null)
                    {
                        base.ModelState.AddModelError("Action.Details.Invalid.Id", "Invalid Id");
                        return RedirectToAction("Index");
                    }
                    model.Selected = dr;
                }

            }
            return View(model);
        }
        #endregion//Details
        #region Edit

        [HttpGet]
        public ActionResult Edit(long Id)
        {
            using (var db = new NespeDbContext())
            {
                var drc = db.RequestTypeSet;
                var dr = (from t in drc where t.Id == Id select t).FirstOrDefault();
                if (dr == null)
                {
                    base.ModelState.AddModelError("Action.Edit.Invalid.Id", "Invalid Id");
                    return RedirectToAction("Index");
                }
                var model = new RequestTypeModel { Selected = dr };
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Edit(RequestTypeModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var selected = model.Selected;
                    var drc = db.RequestTypeSet;
                    //drc.Attach(model.Selected);
                    var dr = (from t in drc where t.Id == model.Selected.Id select t).FirstOrDefault();
                    if (dr != null)
                    {
                        dr.Copy(selected);
                        selected = dr;
                    }
                    else
                        drc.Add(selected);

                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion//Edit

        #region Create

        [HttpGet]
        public ActionResult Create()
        {
            var model = new RequestTypeModel { Selected = new RequestType { } };
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(RequestTypeModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var drc = db.RequestTypeSet;
                    var dr = (from t in drc where t.Name == model.Selected.Name select t).FirstOrDefault();
                    if (dr != null && dr.Id > 0)
                    {
                        return RedirectToAction("Edit", new { Id = dr.Id });
                    }
                    else
                    {
                        drc.Add(model.Selected);
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion//Create
    }
}
