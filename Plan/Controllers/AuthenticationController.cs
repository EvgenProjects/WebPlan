using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Plan.Models; 

namespace Plan.Controllers
{
	public class AuthenticationController : Controller
	{
		public ActionResult Login()
		{
			return View();
		}

		public ActionResult NotLoggedIn()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(UserLogin model)
		{
			if (ModelState.IsValid)
			{
				// поиск пользователя в базе данных 
				User user = null;
				using (UserContext db = new UserContext())
				{
					user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
				}

				// нашли пользователя 
				if (user != null)
				{
					// устанавливаем cookie 
					FormsAuthentication.SetAuthCookie(model.Name, true);

					// переходим 
					return RedirectToAction("Index", "Home");
				}
				else
				{
					// выводим ошибку 
					ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
				}
			}

			return View(model);
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(UserRegister model)
		{
			if (ModelState.IsValid)
			{
				// ищем пользователя в базе данных 
				User user = null;
				using (UserContext db = new UserContext())
				{
					user = db.Users.FirstOrDefault(u => u.Email == model.Name);
				}

				// пользователя нет базе данных 
				if (user == null)
				{
					// создаем нового пользователя 
					using (UserContext db = new UserContext())
					{
						db.Users.Add(new User { Email = model.Name, Password = model.Password });
						db.SaveChanges();

						user = db.Users.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
					}

					// пользователь добавлен в базу данных 
					if (user != null)
					{
						// устанавливаем cookie 
						FormsAuthentication.SetAuthCookie(model.Name, true);

						// переходим 
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					// выводим ошибку 
					ModelState.AddModelError("", "Пользователь с таким логином уже существует");
				}
			}

			return View(model);
		}

		public ActionResult Logoff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}
	}
}