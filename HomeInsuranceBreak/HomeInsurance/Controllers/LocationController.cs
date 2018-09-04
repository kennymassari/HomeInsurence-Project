using HomeInsurance.Models;
using System.Web.Mvc;

namespace HomeInsurance.Controllers
{

	public class LocationController : HIControllerBase
	{
		public LocationController() { }

		public LocationController(IQuotesEntitySource dataSource): base(dataSource) { }

		#region Location Form
		public ActionResult LocationForm()
		{
			return View(new Location());
		}

		[HttpPost]
		public ActionResult LocationForm(Location location)
		{
			if (!ModelState.IsValid)
			{
				return View(location);
			}

			Session["Location"] = location;
			return RedirectToAction("PropertyForm", "Property");
		}
		#endregion
	}
}