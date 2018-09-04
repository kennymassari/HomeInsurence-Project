using HomeInsurance.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HomeInsurance.Controllers
{

	public class WelcomeController : HIControllerBase
	{
		public WelcomeController() { }

		public WelcomeController(IQuotesEntitySource dataSource): base(dataSource) { }

		public ActionResult LogoutUser()
		{
			Session.Clear();
			return View();
		}

		public ActionResult LoginUser()
		{
			return View();
		}

		[HttpPost]
		public ActionResult LoginUser(User user)
		{
			if (!ModelState.IsValid)
			{
				return View("LoginUser");
			}

			using (IQuotesEntity qe = QuoteSource.CreateQuotesEntity())
			{
				User existing = qe.Users.FirstOrDefault(u => u.Password == user.Password
										&& u.Username == user.Username);

				if (existing == null)
				{
					ModelState.AddModelError("", "The user name or password provided is incorrect.");
					return View("LoginUser", user);
				}

				return RedirectUser(existing);
			}
		}

		public ActionResult NewUser()
		{
			return View("NewUser", new NewUser());
		}

		[HttpPost]
		public ActionResult NewUser(NewUser newUser)
		{
			if (!ModelState.IsValid)
			{
				return View("NewUser", newUser);
			}

			using (IQuotesEntity qe = QuoteSource.CreateQuotesEntity())
			{
				User u = qe.Users.FirstOrDefault(model => model.Username == newUser.Username);

				if (u != null)
				{
					ModelState.AddModelError("", "This username already exists.");
					return View("NewUser", newUser);
				}

				User user = new User(newUser);
				qe.AddUser(user);
				qe.SaveChanges();
				return RedirectUser(user);
			};
		}

		private ActionResult RedirectUser(User user)
		{
			Session["User"] = user;
			Session["Admin"] = user.IsAdmin ? "" : null;

			if (user.IsAdmin)
				return RedirectToAction("SearchUser", "Admin");
			else
				return RedirectToAction("GetStarted", "Quotes");
		}
	}
}