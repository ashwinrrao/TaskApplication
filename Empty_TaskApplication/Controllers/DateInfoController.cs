using Empty_TaskApplication.Models;
using Empty_TaskApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
