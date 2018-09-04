using System.Data.Entity;

namespace HomeInsurance.Models
{

	public class QuotesEntity : DbContext
	{

		static QuotesEntity()
		{
			Database.SetInitializer<QuotesEntity>(null);
		}

		public QuotesEntity() : base("LocalConnection") { }

		public DbSet<User> Users { get; set; }
		public DbSet<Quote> Quotes { get; set; }
		public DbSet<Policy> Policies { get; set; }
		public DbSet<Property> Properties { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<Homeowner> HomeOwners { get; set; }
	}
}