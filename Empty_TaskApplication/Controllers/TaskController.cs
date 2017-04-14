using Empty_TaskApplication.Models;
using Empty_TaskApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Empty_TaskApplication.Controllers
{
    public class TaskController : Controller
    {
        TaskDbContext _db = new TaskDbContext();
        // GET: Task
        public ActionResult Index()
        {
            var model = _db.Task.Select(t => t);
            return View(model);
        }
        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(Task TaskObj)
        {
            if (ModelState.IsValid)
            {
                TaskObj.duration = calculateDuration(TaskObj.fromDate, TaskObj.toDate);
                _db.Task.Add(TaskObj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        private int calculateDuration(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == DateTime.MinValue || toDate == DateTime.MinValue)
            {
                return 0;
            }
            var AllDatesForTask = new List<DateTime>();
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                AllDatesForTask.Add(date);
            }
            var Holidays = _db.DateInfo.Where(x => x.isHoliday && x.date >= fromDate && x.date <= toDate).Select(x => x.date);
            var WorkingDays = _db.DateInfo.Where(x => (!x.isHoliday) && x.date >= fromDate && x.date <= toDate).Select(x => x.date);
            foreach (DateTime Date in AllDatesForTask.ToList())
            {
                if ((Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday) && (!WorkingDays.Contains(Date)))
                    AllDatesForTask.Remove(Date);
            }
            foreach (DateTime Holiday in Holidays)
            {
                if (AllDatesForTask.Contains(Holiday))
                    AllDatesForTask.Remove(Holiday);
            }
            return AllDatesForTask.Count();
        }
    }
}
