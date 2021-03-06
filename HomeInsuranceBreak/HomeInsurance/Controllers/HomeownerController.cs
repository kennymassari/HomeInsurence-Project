﻿using HomeInsurance.Models;
using System.Web.Mvc;

namespace HomeInsurance.Controllers
{

	public class HomeownerController : HIControllerBase
	{
		public HomeownerController() { }

		public HomeownerController(IQuotesEntitySource dataSource): base(dataSource) { }

		#region Homeowner Form
		public ActionResult HomeownerForm()
		{
			return View(new Homeowner());
		}

		[HttpPost]
		public ActionResult HomeownerForm(Homeowner homeOwner)
		{
			if (!ModelState.IsValid)
			{
				return View(homeOwner);
			}

			Session["Homeowner"] = homeOwner;
			return RedirectToAction("LocationForm", "Location");
		}
		#endregion
	}
}