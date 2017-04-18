using Empty_TaskApplication.Models;
using Empty_TaskApplication.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Empty_TaskApplication.Controllers
{
    public class DateInfoController : Controller
    {
        TaskDbContext _db = new TaskDbContext();
        // GET: DateInfo
        public ActionResult Index()
        {
            var model = _db.DateInfo.Select(x => x);
            return View(model);
        }

        // GET: DateInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DateInfo/Create
        [HttpPost]
        public ActionResult Create(DateInfo DateInfoObj)
        {
            if (ModelState.IsValid)
            {
                _db.DateInfo.Add(DateInfoObj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateInfo dateToEdit = _db.DateInfo.Find(id.Value);
            if (dateToEdit == null)
            {
                return HttpNotFound();
            }
            return View(dateToEdit);
        }

        [HttpPost]
        public ActionResult Edit(DateInfo DateInfoObj)
        {
            if (DateInfoObj.date == DateTime.MinValue)
                ModelState.AddModelError("date", "The Date field is required.");
            if (ModelState.IsValid)
            {
                _db.Entry(DateInfoObj).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(DateInfoObj);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateInfo date = _db.DateInfo.Find(id);
            if (date == null)
            {
                return HttpNotFound();
            }
            return View(date);
        }

        // POST: Restaurant/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            DateInfo date = _db.DateInfo.Find(id);
            _db.DateInfo.Remove(date);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
