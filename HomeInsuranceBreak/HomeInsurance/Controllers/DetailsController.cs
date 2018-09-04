using HomeInsurance.Models;
using System.Web.Mvc;

namespace HomeInsurance.Controllers
{
	public class DetailsController : HIControllerBase
	{
		public DetailsController() { }

		public DetailsController(IQuotesEntitySource dataSource): base(dataSource) { }

		public ActionResult PolicyTerms()
		{
			return View();
		}

		public ActionResult QuoteTerms()
		{
			return View();
		}
	}
}