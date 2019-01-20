using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plan.Models;
using System.Data.Entity;

namespace Plan.Controllers
{
    public class HomeController : Controller
    {
		private UserContext db = new UserContext();

        // GET: Home
        public ActionResult Index()
        {
			// not logged in
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("NotLoggedIn", "Authentication");

			// show in view
			string curUserLogin = User.Identity.Name;
			var tasks = db.Tasks.Include(t => t.User).Where(item => item.User.Email == curUserLogin).OrderBy(v => new { v.StartDate, v.StartTime });
			return View(tasks.ToList());
		}
	}
}