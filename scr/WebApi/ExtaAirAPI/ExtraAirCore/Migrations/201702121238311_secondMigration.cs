namespace ExtraAirCore.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class secondMigration : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Addresses",
				c => new
				{
					AddressId = c.Int(nullable: false, identity: true),
					Country = c.String(),
					City = c.String(),
					Street = c.String(),
					StreetNumber = c.Int(nullable: false),
					PostIndex = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.AddressId);

			CreateTable(
				"dbo.Airports",
				c => new
				{
					AirportId = c.Int(nullable: false, identity: true),
					Name = c.String(),
					AddressId = c.Int(),
				})
				.PrimaryKey(t => t.AirportId)
				.ForeignKey("dbo.Addresses", t => t.AddressId)
				.Index(t => t.AddressId);

			CreateTable(
				"dbo.Feedbacks",
				c => new
				{
					FeedbackId = c.Int(nullable: false, identity: true),
					Title = c.String(),
					Description = c.String(),
					TourId = c.Int(),
					AirportId = c.Int(),
					UserId = c.Int(),
				})
				.PrimaryKey(t => t.FeedbackId)
				.ForeignKey("dbo.Airports", t => t.AirportId)
				.ForeignKey("dbo.Tours", t => t.TourId)
				.ForeignKey("dbo.Users", t => t.UserId)
				.Index(t => t.TourId)
				.Index(t => t.AirportId)
				.Index(t => t.UserId);

			CreateTable(
				"dbo.Tours",
				c => new
				{
					TourId = c.Int(nullable: false, identity: true),
					DateStart = c.DateTime(nullable: false),
					DateFinish = c.DateTime(nullable: false),
					Price = c.Decimal(nullable: false, precision: 18, scale: 2),
					CurrentCountPassenger = c.Int(nullable: false),
					PlaneId = c.Int(nullable: false),
					OrderId = c.Int(nullable: false),
					Airport_AirportId = c.Int(),
				})
				.PrimaryKey(t => t.TourId)
				.ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
				.ForeignKey("dbo.Planes", t => t.PlaneId, cascadeDelete: true)
				.ForeignKey("dbo.Airports", t => t.Airport_AirportId)
				.Index(t => t.PlaneId)
				.Index(t => t.OrderId)
				.Index(t => t.Airport_AirportId);

			CreateTable(
				"dbo.Orders",
				c => new
				{
					OrderId = c.Int(nullable: false, identity: true),
					Date = c.DateTime(nullable: false),
					Price = c.Decimal(nullable: false, precision: 18, scale: 2),
					Paid = c.Boolean(nullable: false),
					UserId = c.Int(nullable: false),
					CreditCardId = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.OrderId)
				.ForeignKey("dbo.CreditCards", t => t.CreditCardId, cascadeDelete: true)
				.ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
				.Index(t => t.UserId)
				.Index(t => t.CreditCardId);

			CreateTable(
				"dbo.CreditCards",
				c => new
				{
					CreditCardId = c.Int(nullable: false, identity: true),
					CardNumber = c.Int(nullable: false),
					ExpirationDate = c.DateTime(nullable: false),
					SecurityCode = c.Int(nullable: false),
					UserId = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.CreditCardId)
				.ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
				.Index(t => t.UserId);

			CreateTable(
				"dbo.Users",
				c => new
				{
					UserId = c.Int(nullable: false, identity: true),
					FirstName = c.String(),
					LastName = c.String(),
					Email = c.String(nullable: false),
					Password = c.String(nullable: false),
					Phone = c.String(),
					Birthday = c.DateTime(nullable: false),
					Deleted = c.Boolean(nullable: false),
					AddressId = c.Int(),
					UserClaimId = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.UserId)
				.ForeignKey("dbo.Addresses", t => t.AddressId)
				.ForeignKey("dbo.UserClaims", t => t.UserClaimId, cascadeDelete: true)
				.Index(t => t.AddressId)
				.Index(t => t.UserClaimId);

			CreateTable(
				"dbo.UserClaims",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					ClaimType = c.String(),
					ClaimValue = c.String(),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.Passengers",
				c => new
				{
					PassengerId = c.Int(nullable: false, identity: true),
					PassengerType = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.PassengerId);

			CreateTable(
				"dbo.Planes",
				c => new
				{
					PlaneId = c.Int(nullable: false, identity: true),
					Name = c.String(),
					MaxCountPassenger = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.PlaneId);

			CreateTable(
				"dbo.Comforts",
				c => new
				{
					ComfortId = c.Int(nullable: false, identity: true),
					Name = c.String(),
					ComfortType = c.Int(nullable: false),
					Price = c.Decimal(nullable: false, precision: 18, scale: 2),
					PlaneId = c.Int(nullable: false),
				})
				.PrimaryKey(t => t.ComfortId)
				.ForeignKey("dbo.Planes", t => t.PlaneId, cascadeDelete: true)
				.Index(t => t.PlaneId);

			CreateTable(
				"dbo.PassengerOrders",
				c => new
				{
					Passenger_PassengerId = c.Int(nullable: false),
					Order_OrderId = c.Int(nullable: false),
				})
				.PrimaryKey(t => new { t.Passenger_PassengerId, t.Order_OrderId })
				.ForeignKey("dbo.Passengers", t => t.Passenger_PassengerId, cascadeDelete: true)
				.ForeignKey("dbo.Orders", t => t.Order_OrderId, cascadeDelete: true)
				.Index(t => t.Passenger_PassengerId)
				.Index(t => t.Order_OrderId);

			CreateTable(
				"dbo.PassengerTours",
				c => new
				{
					Passenger_PassengerId = c.Int(nullable: false),
					Tour_TourId = c.Int(nullable: false),
				})
				.PrimaryKey(t => new { t.Passenger_PassengerId, t.Tour_TourId })
				.ForeignKey("dbo.Passengers", t => t.Passenger_PassengerId, cascadeDelete: true)
				.ForeignKey("dbo.Tours", t => t.Tour_TourId, cascadeDelete: true)
				.Index(t => t.Passenger_PassengerId)
				.Index(t => t.Tour_TourId);

		}

		public override void Down()
		{
			DropForeignKey("dbo.Tours", "Airport_AirportId", "dbo.Airports");
			DropForeignKey("dbo.Feedbacks", "UserId", "dbo.Users");
			DropForeignKey("dbo.Feedbacks", "TourId", "dbo.Tours");
			DropForeignKey("dbo.Tours", "PlaneId", "dbo.Planes");
			DropForeignKey("dbo.Comforts", "PlaneId", "dbo.Planes");
			DropForeignKey("dbo.Tours", "OrderId", "dbo.Orders");
			DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
			DropForeignKey("dbo.PassengerTours", "Tour_TourId", "dbo.Tours");
			DropForeignKey("dbo.PassengerTours", "Passenger_PassengerId", "dbo.Passengers");
			DropForeignKey("dbo.PassengerOrders", "Order_OrderId", "dbo.Orders");
			DropForeignKey("dbo.PassengerOrders", "Passenger_PassengerId", "dbo.Passengers");
			DropForeignKey("dbo.Orders", "CreditCardId", "dbo.CreditCards");
			DropForeignKey("dbo.CreditCards", "UserId", "dbo.Users");
			DropForeignKey("dbo.Users", "UserClaimId", "dbo.UserClaims");
			DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
			DropForeignKey("dbo.Feedbacks", "AirportId", "dbo.Airports");
			DropForeignKey("dbo.Airports", "AddressId", "dbo.Addresses");
			DropIndex("dbo.PassengerTours", new[] { "Tour_TourId" });
			DropIndex("dbo.PassengerTours", new[] { "Passenger_PassengerId" });
			DropIndex("dbo.PassengerOrders", new[] { "Order_OrderId" });
			DropIndex("dbo.PassengerOrders", new[] { "Passenger_PassengerId" });
			DropIndex("dbo.Comforts", new[] { "PlaneId" });
			DropIndex("dbo.Users", new[] { "UserClaimId" });
			DropIndex("dbo.Users", new[] { "AddressId" });
			DropIndex("dbo.CreditCards", new[] { "UserId" });
			DropIndex("dbo.Orders", new[] { "CreditCardId" });
			DropIndex("dbo.Orders", new[] { "UserId" });
			DropIndex("dbo.Tours", new[] { "Airport_AirportId" });
			DropIndex("dbo.Tours", new[] { "OrderId" });
			DropIndex("dbo.Tours", new[] { "PlaneId" });
			DropIndex("dbo.Feedbacks", new[] { "UserId" });
			DropIndex("dbo.Feedbacks", new[] { "AirportId" });
			DropIndex("dbo.Feedbacks", new[] { "TourId" });
			DropIndex("dbo.Airports", new[] { "AddressId" });
			DropTable("dbo.PassengerTours");
			DropTable("dbo.PassengerOrders");
			DropTable("dbo.Comforts");
			DropTable("dbo.Planes");
			DropTable("dbo.Passengers");
			DropTable("dbo.UserClaims");
			DropTable("dbo.Users");
			DropTable("dbo.CreditCards");
			DropTable("dbo.Orders");
			DropTable("dbo.Tours");
			DropTable("dbo.Feedbacks");
			DropTable("dbo.Airports");
			DropTable("dbo.Addresses");
		}
	}
}
