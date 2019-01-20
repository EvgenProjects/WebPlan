using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plan.Models;

namespace Plan.Controllers
{
    public class TaskManagementController : Controller
    {
        private UserContext db = new UserContext();

        // GET: Tasks
        public ActionResult Index()
        {
			// not logged in
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Home");

			string curUserLogin = User.Identity.Name;
			var tasks = db.Tasks.Include(t => t.User).Where(item => item.User.Email == curUserLogin);
            return View(tasks.ToList());
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
			// not logged in
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Home");

			string curUserLogin = User.Identity.Name;
            var user = db.Users.FirstOrDefault(item => item.Email== curUserLogin);
			Task task = new Task();
			task.StartDate = DateTime.Now;
			task.StartTime = DateTime.Now;
			task.EndTime = DateTime.Now;
			task.UserId = user.Id;
			return View(task);
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task task)
        {
			// not logged in
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Home");

			if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
			// not logged in
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", task.UserId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            Task task = db.Tasks.Find(id);
            if (task != null)
            {
				db.Tasks.Remove(task);
				db.SaveChanges();
            }
			return RedirectToAction("Index");
		}
    }
}
