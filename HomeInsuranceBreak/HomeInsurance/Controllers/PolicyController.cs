using HomeInsurance.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HomeInsurance.Controllers
{

	public class PolicyController : HIControllerBase
	{
		public PolicyController() { }

		public PolicyController(IQuotesEntitySource dataSource): base(dataSource) { }

		public ActionResult MyPolicies()
		{
			User user = Session["User"] as User;
			List<Policy> policies = new List<Policy>();

			using(IQuotesEntity qe = QuoteSource.CreateQuotesEntity())
			{
				Homeowner ho = qe.Homeowners.FirstOrDefault(h => h.UserId == user.Id);
				if (ho == null) return View(policies);
				policies.AddRange(qe.IncludeInPolicies("Quote.Property.Location.Homeowner.User")
					.Where(p => p.Quote.Property.Location.Homeowner.UserId == ho.UserId));
				return View(policies);
			}
		}

		public ActionResult Confirmation()
		{
			int policyId;

			try
			{
				policyId = (int)Session["policyId"];
			}

			catch
			{
				return RedirectToAction("GetStarted", "Quotes");
			}
			using (IQuotesEntity qe = QuoteSource.CreateQuotesEntity())
			{
				Policy p = qe.Policies.FirstOrDefault(pp => pp.Id == policyId);
				return View(p);
			}
		}

		public ActionResult BuyPolicy()
		{
			VerifyPolicy vp = new VerifyPolicy
			{
				PolicyStartDate = DateTime.Now.ToString("yyyy-MM-dd")
			};

			try
			{
				vp.QuoteId = (int)Session["quoteId"];
			}

			catch
			{
				return RedirectToAction("GetStarted", "Quotes");
			}

			return View(vp);
		}

		[HttpPost]
		public ActionResult BuyPolicy(VerifyPolicy verify)
		{
            if (!ModelState.IsValid)
            {
                if (!verify.Acknowledge)
                {
                    ModelState.AddModelError("", "Please e-sign to buy policy.");
                }
                return View(verify);
            }

            if (!verify.Acknowledge)
            {
                ModelState.AddModelError("", "Please e-sign to buy policy.");
                return View(verify);
            }

            if (!DateTime.TryParse(verify.PolicyStartDate, out DateTime startDate))
            {
                return View(verify);
            }

            Policy p = new Policy
            {
                PolicyEffDate = startDate.ToString("yyyy-MM-dd"),
                PolicyEndDate = startDate.AddYears(1).ToString("yyyy-MM-dd"),
                QuoteId = (int)Session["quoteId"],
                PolicyKey = Session["quoteId"].ToString() + "_1",
                PolicyStatus = startDate <= DateTime.Now ? "ACTIVE" : "PENDING",
                PolicyTerm = 1
            };

            using (QuotesEntity qe = new QuotesEntity())
            {
                qe.Policies.Add(p);
                qe.SaveChanges();
            }

            Session["policyId"] = p.Id;
            return RedirectToAction("Confirmation");
        }
    }
}