using HomeInsurance.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Web.Mvc;
using MvcContrib.TestHelper;

namespace HomeInsurance.UnitTests
{
	[TestClass]
	public class ControllerTestBase
	{
		[TestInitialize]
		public void TestInitialize()
		{
			QuotesEntitySource = new MockQuotesEntitySource();
		}

		protected MockQuotesEntitySource QuotesEntitySource { get; private set; }
		protected TestControllerBuilder Builder { get; private set; }

		private static readonly Type QesType = typeof(IQuotesEntitySource);
		private static readonly Type[] QesTypes = new Type[] { QesType };
		protected T CreateController<T>()
			where T : Controller
		{
			ConstructorInfo cinfo = typeof(T).GetConstructor(QesTypes);
			T t = (T)cinfo.Invoke(new object[] { QuotesEntitySource });
			TestControllerBuilder builder = new TestControllerBuilder();
			builder.InitializeController(t);
			Builder = builder;
			return t;
		}
	}
}
