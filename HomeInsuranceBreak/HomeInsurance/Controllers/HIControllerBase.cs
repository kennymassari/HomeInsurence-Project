using HomeInsurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeInsurance.Controllers
{

	public class HIControllerBase : Controller
	{
		protected HIControllerBase(): this(new DbQuotesEntitySource()) { }

		protected HIControllerBase(IQuotesEntitySource dataSource)
		{
			QuoteSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
		}

		protected IQuotesEntitySource QuoteSource { get; private set; }

	}
}