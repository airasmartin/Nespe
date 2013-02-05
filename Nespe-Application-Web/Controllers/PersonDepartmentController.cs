using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Models;
using Nespe.Context;

namespace Nespe.Controllers
{
    public class PersonDepartmentController : Controller
    {
        #region Index

        //
        // GET: /Person/

        public ActionResult Index()
        {
            using (var db = new NespeDbContext())
            {
                var drc = db.PersonDepartmentSet;
                var model = new PersonDepartmentModel { };

                model.Items = Query(db).ToList();
                return View(model);
            }

        }
        private PersonDepartment Get(long Id, NespeDbContext db)
        {
            return (from t in Query(db) where t.Id == Id select t).FirstOrDefault();
        }
        private IQueryable<PersonDepartment> Query(NespeDbContext db)
        {
                var drc = db.PersonDepartmentSet;
                //return (from t in drc from p in db.PersonSet from d in db.DepartmentSet where d.Id == t.Department_Id && p.Id == t.Person_Id select Normalize(t, p, d));
                //return (from t in drc from p in db.PersonSet from d in db.DepartmentSet where d.Id == t.Department_Id && p.Id == t.Person_Id select (tl, pl, dl)=> t);
                return (from t in drc select t);
        }
        private PersonDepartment Get(long Id, NespeObjectContext db)
        {
            return (from t in Query(db) where t.Id == Id select t).FirstOrDefault();
        }
        private IQueryable<PersonDepartment> Query(NespeObjectContext db)
        {
            var drc = db.PersonDepartmentSet;
            //return (from t in drc from p in db.PersonSet from d in db.DepartmentSet where d.Id == t.Department_Id && p.Id == t.Person_Id select Normalize(t, p, d));
            //return (from t in drc from p in db.PersonSet from d in db.DepartmentSet where d.Id == t.Department_Id && p.Id == t.Person_Id select (tl, pl, dl)=> t);
            return (from t in drc select t);
        }

        private void Bind(PersonDepartment selected, NespeDbContext db) {
            selected.Person = (from t in db.PersonSet where t.Id == selected.Person_Id select t).FirstOrDefault();
            selected.Department = (from t in db.DepartmentSet where t.Id == selected.Department_Id select t).FirstOrDefault();
        }
        #endregion//Index
        public static PersonDepartment Normalize(PersonDepartment t, Person p, Department d)
        {
            if (t == null)
                return t;
            if ((t.Person = p) != null)
                t.Person_Id = p.Id;
            if ((t.Department = d) != null)
                t.Department_Id = d.Id;
            return t;
        }
        #region Delete

        [HttpGet]
        public ActionResult Delete(long Id)
        {
            using (var db = new NespeDbContext())
            {
                var drc = db.PersonDepartmentSet;
                var dr = Get(Id, db);
                if (dr == null)
                {
                    base.ModelState.AddModelError("Action.Delete.Invalid.Id", "Invalid Id");
                    return RedirectToAction("Index");
                }
                var model = new PersonDepartmentModel { Selected = dr };
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult Delete(PersonDepartmentModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var drc = db.PersonDepartmentSet;
                    var dr = Get(model.Selected.Id, db);
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
                var drc = db.PersonDepartmentSet;
                var dr = Get(Id, db);
                if (dr == null)
                {
                    base.ModelState.AddModelError("Action.Details.Invalid.Id", "Invalid Id");
                    return RedirectToAction("Index");
                }
                var model = new PersonDepartmentModel { Selected = dr };
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult Details(PersonDepartmentModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var drc = db.PersonDepartmentSet;
                    var dr = Get(model.Selected.Id, db);
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
            //using (var db = new NespeObjectContext())
            {
                var drc = db.PersonDepartmentSet;
                var dr = Get(Id, db);
                if (dr == null)
                {
                    base.ModelState.AddModelError("Action.Edit.Invalid.Id", "Invalid Id");
                    return RedirectToAction("Index");
                }
                var model = new PersonDepartmentModel { Selected = dr };
                var selected = model.Selected;
                Bind(selected, db);
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Edit(PersonDepartmentModel model, FormCollection formCollection)
        {
            var selected = model.Selected;
            using (var db = new NespeDbContext())
            {
                Bind(selected, db);

                if (ModelState.IsValid)
                {

                    var drc = db.PersonDepartmentSet;
                    //drc.Attach(model.Selected);
                    var dr = Get(model.Selected.Id, db);
                    if (dr != null)
                    {
                        dr.Copy(selected);
                        selected = dr;
                    }
                    else
                        drc.Add(selected);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        #endregion//Edit

        #region Create

        [HttpGet]
        public ActionResult Create(long Person_Id, long Department_Id)
        {
            var model = new PersonDepartmentModel { Selected = new PersonDepartment { Person_Id = Person_Id, Department_Id = Department_Id } };
            using (var db = new NespeDbContext()) {
                Bind(model.Selected, db);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(PersonDepartmentModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var drc = db.PersonDepartmentSet;
                    var selected = model.Selected;
                    var dr = (from t in drc where t.Person_Id == selected.Person_Id && t.Department_Id == selected.Department_Id select t).FirstOrDefault();
                    if (dr != null && dr.Id > 0)
                    {
                        return RedirectToAction("Edit", new { Id = dr.Id });
                    }
                    else
                    {
                        Bind(selected, db);
                        drc.Add(selected);
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion//Create


        #region Details

        [HttpGet]
        public ActionResult AddDepartment(long Id, long Department_Id)
        {
            using (var db = new NespeDbContext())
            {
                var drc = db.PersonDepartmentSet;
                var dr = Get(Id, db);
                if (dr == null)
                {
                    base.ModelState.AddModelError("Action.Details.Invalid.Id", "Invalid Id");
                    return RedirectToAction("Index");
                }
                var model = new PersonDepartmentModel { Selected = dr };
                Bind(model.Selected, db);
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult AddDepartment(PersonDepartmentModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NespeDbContext())
                {
                    var drc = db.PersonDepartmentSet;
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
    }
}
