using HomeInsurance.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HomeInsurance.Controllers
{
	public class QuotesController : HIControllerBase
	{
		public QuotesController() { }

		public QuotesController(IQuotesEntitySource dataSource): base(dataSource) { }

		public ActionResult GetStarted()
		{
			return View();
		}

		// Full summary of Quote/Property/Location
		public ActionResult QuoteSummary(int? quoteId)
		{
			if (!quoteId.HasValue)
			{
				try
				{
					quoteId = (int)Session["quoteId"];
				}

				catch
				{
					// TODO: redirect
					return RedirectToAction("GetStarted", "Quotes");
				}
			}

			else
			{
				Session["quoteId"] = quoteId;
			}

			// TODO: do we have a User?
			using(IQuotesEntity qe = QuoteSource.CreateQuotesEntity())
			{
				Quote q = qe.IncludeInQuotes("Property.Location.Homeowner.User").Where(qq => qq.Id == quoteId).FirstOrDefault();
                return View(q);
			}
		}

		public ActionResult QuoteDetails()
		{
            User user = Session["User"] as User;
            List<Quote> quoteList = new List<Quote>();
            //List<Policy> policyList = new List<Policy>();
            using (IQuotesEntity qe = QuoteSource.CreateQuotesEntity())
            {
                Homeowner ho = qe.Homeowners.FirstOrDefault(h => h.UserId == user.Id);
                quoteList.AddRange(qe.IncludeInQuotes("Property.Location.Homeowner.User")
                    .Where(q => q.Property.Location.Homeowner.UserId == user.Id));
                //policyList.AddRange(qe.IncludeInPolicies("Quote.Property.Location.Homeowner.User")
                //    .Where(p => p.Quote.Property.Location.Homeowner.UserId == ho.UserId));
                //var quotesWithPolicy = quoteList.Where(q => policyList.Any(p => p.QuoteId == q.Id));
                //var quotesWithoutPolicy = quoteList.Except(quotesWithPolicy);
                return View(quoteList);
                //return View(quotesWithoutPolicy);
            }
        }

		public ActionResult CoverageDetails()
		{
			Property property = Session["Property"] as Property;
			Quote quote = new Quote(property);

			Session["Quote"] = quote;
			return View(quote);
		}

		public ActionResult SaveQuote()
		{
			Quote quote = Session["Quote"] as Quote;
			Property property = Session["Property"] as Property;
			Location location = Session["Location"] as Location;
			Homeowner homeowner = Session["Homeowner"] as Homeowner;
			User user = Session["User"] as User;

			quote.Property = property;
			property.Location = location;
			location.Homeowner = homeowner;

			homeowner.UserId = user.Id;
			using(IQuotesEntity qe = QuoteSource.CreateQuotesEntity())
			{
				qe.AddQuote(quote);
				qe.SaveChanges();
			}

			Session.Clear();
			Session["User"] = user;
			return RedirectToAction("QuoteDetails");
		}
	}
}