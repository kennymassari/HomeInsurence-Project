using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeInsurance.Controllers;
using System.Web.Mvc;
using HomeInsurance.Models;
using System.Linq;

namespace HomeInsurance.UnitTests
{
	[TestClass]
	public class WelcomeControllerTest : ControllerTestBase
	{
		[TestMethod]
		public void LoginUser()
		{
			WelcomeController controller = CreateController<WelcomeController>();
			var view = (ViewResult)controller.LoginUser();
			Assert.IsNull(view.Model, "No model for default login view");
		}

		//[TestMethod]
		//public void LoginUserUnknownUser()
		//{
		//	WelcomeController controller = CreateController<WelcomeController>();
		//	var view = (ViewResult)controller.LoginUser(new User { Username = "Test", Password = "secret" });
		//	Assert.AreEqual(1, controller.ModelState.Count);
		//	Assert.AreEqual("LoginUser", view.ViewName);
		//}

		[TestMethod]
		public void LoginUserKnownUserAdmin()
		{
			WelcomeController controller = CreateController<WelcomeController>();
			var qe = QuotesEntitySource.CreateQuotesEntity();
			User user = qe.Users.First(u => u.IsAdmin);
			RedirectToRouteResult rr = controller.LoginUser(user) as RedirectToRouteResult;
			Assert.IsNotNull(rr);
			Assert.AreEqual(user, controller.Session["User"]);
			Assert.AreEqual(2, rr.RouteValues.Count);
			Assert.AreEqual(rr.RouteValues["action"], "SearchUser");
			Assert.AreEqual(rr.RouteValues["controller"], "Admin");
		}

		[TestMethod]
		public void LoginUserKnownUser()
		{
			WelcomeController controller = CreateController<WelcomeController>();
			var qe = QuotesEntitySource.CreateQuotesEntity();
			User user = qe.Users.First(u => !u.IsAdmin);
			RedirectToRouteResult rr = controller.LoginUser(user) as RedirectToRouteResult;
			Assert.IsNotNull(rr);
			Assert.AreEqual(user, controller.Session["User"]);
			Assert.AreEqual(2, rr.RouteValues.Count);
			Assert.AreEqual(rr.RouteValues["action"], "GetStarted");
			Assert.AreEqual(rr.RouteValues["controller"], "Quotes");
		}

		[TestMethod]
		public void NewUser()
		{
			WelcomeController controller = CreateController<WelcomeController>();
			var view = (ViewResult)controller.NewUser();
			Assert.AreEqual("NewUser", view.ViewName);
		}

		[TestMethod]
		public void NewUserPostBack()
		{
			NewUser newUser = new NewUser { Username = "UnitTest", Password = "abcdefg", ReEnterPassword = "abcdefg" };
			WelcomeController controller = CreateController<WelcomeController>();
			var view = controller.NewUser(newUser);
			Assert.IsNotNull(view);
			Assert.IsTrue(QuotesEntitySource.MockQuotesEntity.ChangesSaved);
			Assert.IsTrue(QuotesEntitySource.MockQuotesEntity.IsDisposed);
			QuotesEntitySource.Reset();
			var user = QuotesEntitySource.MockQuotesEntity.Users.FirstOrDefault(u => u.Username == newUser.Username && u.Password == newUser.Password);
			Assert.IsNotNull(user);
		}
	}
}
