using System.Configuration;
using System.Data.Entity;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirCore.Models.EFContex
{
	public class ExtraAirContext : DbContext
	{
		public ExtraAirContext(): base(ConfigurationManager.ConnectionStrings["ExtraAirContextLocal"].ConnectionString) { }

		public virtual DbSet<Address> Addresses { get; set; }
		public virtual DbSet<Airport> Airports { get; set; }
		public virtual DbSet<Comfort> Comforts { get; set; }
		public virtual DbSet<CreditCard> CreditCards { get; set; }
		public virtual DbSet<Feedback> Feedbacks { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<Passenger> Passengers { get; set; }
		public virtual DbSet<Plane> Planes { get; set; }
		public virtual DbSet<Tour> Tours { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Client> Clients { get; set; }
		public virtual DbSet<Dispatcher> Dispatchers { get; set; }
		public virtual DbSet<Admin> Admins { get; set; }
		public virtual DbSet<UserClaim> UserClaims { get; set; }
		public virtual DbSet<TourToAirport> TourToAirports { get; set; }
		public virtual DbSet<TourSearchHistory> TourSearchHistories { get; set; }
		public virtual DbSet<TourDetails> TourDetailses { get; set; }
		public virtual DbSet<BookedPlace> BookedPlaces { get; set; }
		public virtual DbSet<TourStatus> TourStatuses { get; set; }
	}
}