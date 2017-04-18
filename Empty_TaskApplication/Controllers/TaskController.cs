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
    public class TaskController : Controller
    {
        TaskDbContext _db = new TaskDbContext();
        // GET: Task
        public ActionResult Index()
        {
            var tasks = _db.Task.Select(t => t);
            var model = new List<TaskView>();
            foreach (Task task in tasks)
            {
                var taskView = new TaskView(task);
                taskView.duration = calculateDuration(taskView.fromDate, taskView.toDate);
                model.Add(taskView);
            }
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task taskToEdit = _db.Task.Find(id.Value);
            TaskView taskView = new TaskView(taskToEdit);
            taskView.duration = calculateDuration(taskView.fromDate, taskView.toDate);
            if (taskToEdit == null)
            {
                return HttpNotFound();
            }
            return View(taskView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskView taskView)
        {
            Task task = new Task(taskView);
            if (task.fromDate == DateTime.MinValue)
                ModelState.AddModelError("fromDate", "The From Date field is required.");
            if (task.toDate == DateTime.MinValue)
                ModelState.AddModelError("toDate", "The To Date field is required.");
            if (ModelState.IsValid)
            {
                _db.Entry(task).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskView);
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
                _db.Task.Add(TaskObj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = _db.Task.Find(id);
            TaskView taskView = new TaskView(task);
            taskView.duration = calculateDuration(taskView.fromDate, taskView.toDate);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(taskView);
        }

        // POST: Restaurant/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Task task = _db.Task.Find(id);
            _db.Task.Remove(task);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
            //Remove all non-working weekends
            foreach (DateTime Date in AllDatesForTask.ToList())
            {
                if ((Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday) && (!WorkingDays.Contains(Date)))
                    AllDatesForTask.Remove(Date);
            }
            //Remove all non-working weekdays
            foreach (DateTime Holiday in Holidays)
            {
                if (AllDatesForTask.Contains(Holiday))
                    AllDatesForTask.Remove(Holiday);
            }
            return AllDatesForTask.Count();
        }
    }
}
