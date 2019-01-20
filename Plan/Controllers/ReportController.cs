using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plan.Models;
using System.Data.Entity;

namespace Plan.Controllers
{
    public class ReportController : Controller
    {
		private UserContext db = new UserContext();
		
        // GET: Report
        public ActionResult Index()
        {
			// not logged in
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Home");

			// first date
			string curUserLogin = User.Identity.Name;
			var tasks = db.Tasks.Include(t => t.User).Where(item => item.User.Email == curUserLogin).OrderBy(v => new { v.StartDate, v.StartTime });
            return View(tasks.FirstOrDefault());
        }

		public ActionResult GetTasksInReportByDate(DateTime date)
		{
			string curUserLogin = User.Identity.Name;

			var tasks = from item in db.Tasks
			where item.User.Email == curUserLogin && item.StartDate.Value!=null && item.StartDate.Value.Day==date.Day 
			orderby new { item.StartDate, item.StartTime }
			select item;

			return View(tasks);
		}
	}
}