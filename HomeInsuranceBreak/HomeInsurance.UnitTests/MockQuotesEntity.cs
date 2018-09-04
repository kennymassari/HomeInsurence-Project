using HomeInsurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInsurance.UnitTests
{
	public class MockQuotesEntity : IQuotesEntity
	{
		private List<User> _users;
		private List<Quote> _quotes;
		private List<Policy> _policies;
		private List<Property> _properties;
		private List<Location> _locations;
		private List<Homeowner> _homeowners;
		private bool _isDisposed, _changesSaved;

		public MockQuotesEntity()
		{
			using(QuotesEntity qe = new QuotesEntity())
			{
				_users = new List<User>(qe.Users);
				_quotes = new List<Quote>(qe.Quotes.Include("Property.Location.Homeowner.User"));
				_policies = new List<Policy>(qe.Policies.Include("Quote.Property.Location.Homeowner.User"));
				_properties = new List<Property>(qe.Properties);
				_locations = new List<Location>(qe.Locations);
				_homeowners = new List<Homeowner>(qe.HomeOwners);
			}
		}

		private void CheckNotDisposed()
		{
			if (_isDisposed) throw new ObjectDisposedException(nameof(MockQuotesEntity));
		}

		public IEnumerable<User> Users
		{
			get
			{
				CheckNotDisposed();
				return _users;
			}
		}

		public IEnumerable<Quote> Quotes
		{
			get
			{
				CheckNotDisposed();
				return _quotes;
			}
		}
		public IEnumerable<Policy> Policies
		{
			get
			{
				CheckNotDisposed();
				return _policies;
			}
		}

		public IEnumerable<Property> Properties
		{
			get
			{
				CheckNotDisposed();
				return _properties;
			}
		}

		public IEnumerable<Location> Locations
		{
			get
			{
				CheckNotDisposed();
				return _locations;
			}
		}

		public IEnumerable<Homeowner> Homeowners
		{
			get 
			{
				CheckNotDisposed();
				return _homeowners;
			}
		}

		public void Dispose()
		{
			_isDisposed = true;
		}

		public int SaveChanges()
		{
			CheckNotDisposed();
			_changesSaved = true;
			return 1;
		}

		public bool IsDisposed => _isDisposed;

		public bool ChangesSaved => _changesSaved;

		public User AddUser(User user)
		{
			_users.Add(user);
			return user;
		}

		public Quote AddQuote(Quote quote)
		{
			_quotes.Add(quote);
			return quote;
		}

		internal void Reset()
		{
			_isDisposed = false;
		}

		public IEnumerable<Quote> IncludeInQuotes(string path)
		{
			return Quotes;
		}

		public IEnumerable<Policy> IncludeInPolicies(string path)
		{
			return Policies;
		}

		public Policy AddPolicy(Policy policy)
		{
			_policies.Add(policy);
			return policy;
		}
	}

	public class MockQuotesEntitySource : IQuotesEntitySource
	{
		private MockQuotesEntity _mqe;
		public IQuotesEntity CreateQuotesEntity()
		{
			if (_mqe == null) _mqe = new MockQuotesEntity();
			return _mqe;
		}

		public MockQuotesEntity MockQuotesEntity => _mqe;

		internal void Reset()
		{
			if (_mqe != null) _mqe.Reset();
		}
	}
}
