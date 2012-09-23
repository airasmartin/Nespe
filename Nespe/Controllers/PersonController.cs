using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Context;
using Nespe.Models;

namespace Nespe.Controllers
{
    public class PersonController : Controller
    {
        #region Index

        //
        // GET: /Person/

        public ActionResult Index()
        {
            using (var db = new NespeDbContext())
            {
                var drc = db.PersonSet;
                var model = new PersonModel { };

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
                var drc = db.PersonSet;
                var dr = (from t in drc where t.Id == Id select t).FirstOrDefault();
                if (dr == null)
                {
                    base.ModelState.AddModelError("Action.Delete.Invalid.Id", "Invalid Id");
                    return RedirectToAction("Index");
                }
                var model = new PersonModel { Selected = dr };
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult Delete(PersonModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var drc = db.PersonSet;
                    var dr = (from t in drc where t.Id == model.Selected.Id select t).FirstOrDefault();
                    if (dr == null)
                    {
                        base.ModelState.AddModelError("Action.Delete.Invalid.Id", "Invalid Id");
                        return RedirectToAction("Index");
                    }
                    drc.Remove(dr);
                    db.SaveChanges();
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
                var drc = db.PersonSet;
                var dr = (from t in drc where t.Id == Id select t).FirstOrDefault();
                if (dr == null)
                {
                    base.ModelState.AddModelError("Action.Details.Invalid.Id", "Invalid Id");
                    return RedirectToAction("Index");
                }
                var model = new PersonModel { Selected = dr };
                model.DepartmentModel = new PersonDepartmentModel
                {
                    Items = (from t in db.PersonDepartmentSet 
                                                  where t.Person_Id == Id select t).ToList()
                };
                var q = (from o in (from t in db.DepartmentSet select t).Except((from t in model.DepartmentModel.Items select t.Department)) select o);
                foreach(var o in q)
                    model.DepartmentModel.Items.Add(new PersonDepartment { Person=dr, Department=o});
                
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult Details(PersonModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var drc = db.PersonSet;
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
            //using (var db = new NespeDbContext())
            using (var db = new NespeObjectContext())
            {
                var drc = db.PersonSet;
                var dr = (from t in drc where t.Id == Id select t).FirstOrDefault();
                if (dr == null)
                {
                    base.ModelState.AddModelError("Action.Edit.Invalid.Id", "Invalid Id");
                    return RedirectToAction("Index");
                }
                var model = new PersonModel { Selected = dr };
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Edit(PersonModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var selected = model.Selected;
                    var drc = db.PersonSet;
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
            var model = new PersonModel { Selected = new Person { } };
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(PersonModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var drc = db.PersonSet;
                    var selected = model.Selected;
                    var dr = (from t in drc where t.FirstName == selected.FirstName && t.FirstName == selected.FirstName select t).FirstOrDefault();
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
