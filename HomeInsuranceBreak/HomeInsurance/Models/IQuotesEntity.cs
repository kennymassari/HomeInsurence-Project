using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInsurance.Models
{
	/// <summary>
	/// Data-access interface used throughout the application.  
	/// This abstraction supports unit-testing of controller logic.
	/// </summary>
	public interface IQuotesEntity : IDisposable
	{
		IEnumerable<User> Users { get; }
		IEnumerable<Quote> Quotes { get; }
		IEnumerable<Policy> Policies { get; }
		IEnumerable<Property> Properties { get; }
		IEnumerable<Location> Locations { get; }
		IEnumerable<Homeowner> Homeowners { get; }
		User AddUser(User user);
		IEnumerable<Quote> IncludeInQuotes(string path);
		Quote AddQuote(Quote quote);
		IEnumerable<Policy> IncludeInPolicies(string path);
		Policy AddPolicy(Policy policy);
		int SaveChanges();
	}

	public interface IQuotesEntitySource
	{
		IQuotesEntity CreateQuotesEntity();
	}

	/// <summary>
	/// Implementation of IQuotesEntity using live database
	/// </summary>
	internal class QuotesEntityImpl : IQuotesEntity
	{
		private QuotesEntity _quotesDb = new QuotesEntity();

		public IEnumerable<User> Users => _quotesDb.Users;

		public IEnumerable<Quote> Quotes => _quotesDb.Quotes;

		public IEnumerable<Policy> Policies => _quotesDb.Policies;

		public IEnumerable<Property> Properties => _quotesDb.Properties;

		public IEnumerable<Location> Locations => _quotesDb.Locations;

		public IEnumerable<Homeowner> Homeowners => _quotesDb.HomeOwners;

		public void Dispose() => _quotesDb.Dispose();

		public int SaveChanges() => _quotesDb.SaveChanges();

		public User AddUser(User user) => _quotesDb.Users.Add(user);

		public IEnumerable<Quote> IncludeInQuotes(string path) => _quotesDb.Quotes.Include(path);

		public Quote AddQuote(Quote quote) => _quotesDb.Quotes.Add(quote);

		public IEnumerable<Policy> IncludeInPolicies(string path) => _quotesDb.Policies.Include(path);

		public Policy AddPolicy(Policy policy) => _quotesDb.Policies.Add(policy);
	}

	internal class DbQuotesEntitySource : IQuotesEntitySource
	{
		public IQuotesEntity CreateQuotesEntity()
		{
			return new QuotesEntityImpl();
		}
	}
}
