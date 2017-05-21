namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class localDB2 : DbMigration
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
                .ForeignKey("dbo.Clients", t => t.UserId)
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
                        StringDays = c.String(),
                        PlaneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourId)
                .ForeignKey("dbo.Planes", t => t.PlaneId, cascadeDelete: true)
                .Index(t => t.PlaneId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Paid = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreditCardId = c.Int(),
                        DateStartTour = c.DateTime(),
                        DateFinishTour = c.DateTime(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Clients", t => t.UserId)
                .ForeignKey("dbo.CreditCards", t => t.CreditCardId)
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
                .ForeignKey("dbo.Clients", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
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
                        ImagePath = c.String(),
                        IdCard = c.String(),
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
                "dbo.TourSearchHistories",
                c => new
                    {
                        TourSearchHistoryId = c.Int(nullable: false, identity: true),
                        DateStart = c.String(),
                        DateSearch = c.String(),
                        PassengerCount = c.Int(),
                        MacAddress = c.String(),
                        AirportFromId = c.Int(nullable: false),
                        AirportToId = c.Int(nullable: false),
                        ComfortId = c.Int(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.TourSearchHistoryId)
                .ForeignKey("dbo.Comforts", t => t.ComfortId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Clients", t => t.UserId)
                .Index(t => t.ComfortId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comforts",
                c => new
                    {
                        ComfortId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ComfortType = c.Int(nullable: false),
                        PriceCorf = c.Double(nullable: false),
                        PlaneId = c.Int(),
                    })
                .PrimaryKey(t => t.ComfortId)
                .ForeignKey("dbo.Planes", t => t.PlaneId)
                .Index(t => t.PlaneId);
            
            CreateTable(
                "dbo.Planes",
                c => new
                    {
                        PlaneId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MaxCountPassengerEconomy = c.Int(nullable: false),
                        MaxCountPassengerBusiness = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlaneId);
            
            CreateTable(
                "dbo.Passengers",
                c => new
                    {
                        PassengerId = c.Int(nullable: false, identity: true),
                        PassengerType = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.Int(nullable: false),
                        CoordinateValue = c.String(),
                        IdCard = c.String(),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BaggageInternal = c.Boolean(nullable: false),
                        BaggageeExternal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PassengerId);
            
            CreateTable(
                "dbo.TourDetails",
                c => new
                    {
                        TourDetailsId = c.Int(nullable: false, identity: true),
                        DateStart = c.DateTime(nullable: false),
                        DateFinish = c.DateTime(nullable: false),
                        CurrentCountOfBusinessPassenger = c.Int(nullable: false),
                        CurrentCountOfEconomyPassenger = c.Int(nullable: false),
                        Temporary = c.Boolean(nullable: false),
                        TourId = c.Int(),
                    })
                .PrimaryKey(t => t.TourDetailsId)
                .ForeignKey("dbo.Tours", t => t.TourId)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.BookedPlace",
                c => new
                    {
                        BookedPlaceId = c.Int(nullable: false, identity: true),
                        PointX = c.Int(nullable: false),
                        PointY = c.Int(nullable: false),
                        ComfortType = c.Int(nullable: false),
                        TourDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookedPlaceId)
                .ForeignKey("dbo.TourDetails", t => t.TourDetailsId, cascadeDelete: true)
                .Index(t => t.TourDetailsId);
            
            CreateTable(
                "dbo.TourStatus",
                c => new
                    {
                        TourStatusId = c.Int(nullable: false, identity: true),
                        DateStart = c.DateTime(nullable: false),
                        DateFinish = c.DateTime(nullable: false),
                        AirportFromId = c.Int(),
                        AirportToId = c.Int(),
                        TourStatusType = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourStatusId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.TourToAirports",
                c => new
                    {
                        TourToAirportId = c.Int(nullable: false, identity: true),
                        DateStart = c.DateTime(),
                        DateFinish = c.DateTime(),
                        isInterim = c.Boolean(nullable: false),
                        TourId = c.Int(nullable: false),
                        AirportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourToAirportId)
                .ForeignKey("dbo.Airports", t => t.AirportId, cascadeDelete: true)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId)
                .Index(t => t.AirportId);
            
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
            
            CreateTable(
                "dbo.OrderTours",
                c => new
                    {
                        Order_OrderId = c.Int(nullable: false),
                        Tour_TourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderId, t.Tour_TourId })
                .ForeignKey("dbo.Orders", t => t.Order_OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Tours", t => t.Tour_TourId, cascadeDelete: true)
                .Index(t => t.Order_OrderId)
                .Index(t => t.Tour_TourId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Dispatchers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dispatchers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Clients", "UserId", "dbo.Users");
            DropForeignKey("dbo.Admins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Feedbacks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Feedbacks", "TourId", "dbo.Tours");
            DropForeignKey("dbo.TourToAirports", "TourId", "dbo.Tours");
            DropForeignKey("dbo.TourToAirports", "AirportId", "dbo.Airports");
            DropForeignKey("dbo.TourStatus", "TourId", "dbo.Tours");
            DropForeignKey("dbo.TourDetails", "TourId", "dbo.Tours");
            DropForeignKey("dbo.BookedPlace", "TourDetailsId", "dbo.TourDetails");
            DropForeignKey("dbo.Tours", "PlaneId", "dbo.Planes");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderTours", "Tour_TourId", "dbo.Tours");
            DropForeignKey("dbo.OrderTours", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.PassengerTours", "Tour_TourId", "dbo.Tours");
            DropForeignKey("dbo.PassengerTours", "Passenger_PassengerId", "dbo.Passengers");
            DropForeignKey("dbo.PassengerOrders", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.PassengerOrders", "Passenger_PassengerId", "dbo.Passengers");
            DropForeignKey("dbo.Orders", "CreditCardId", "dbo.CreditCards");
            DropForeignKey("dbo.CreditCards", "UserId", "dbo.Users");
            DropForeignKey("dbo.TourSearchHistories", "UserId", "dbo.Clients");
            DropForeignKey("dbo.TourSearchHistories", "UserId", "dbo.Users");
            DropForeignKey("dbo.TourSearchHistories", "ComfortId", "dbo.Comforts");
            DropForeignKey("dbo.Comforts", "PlaneId", "dbo.Planes");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Clients");
            DropForeignKey("dbo.Feedbacks", "UserId", "dbo.Clients");
            DropForeignKey("dbo.CreditCards", "UserId", "dbo.Clients");
            DropForeignKey("dbo.Users", "UserClaimId", "dbo.UserClaims");
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Feedbacks", "AirportId", "dbo.Airports");
            DropForeignKey("dbo.Airports", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Dispatchers", new[] { "UserId" });
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropIndex("dbo.Admins", new[] { "UserId" });
            DropIndex("dbo.OrderTours", new[] { "Tour_TourId" });
            DropIndex("dbo.OrderTours", new[] { "Order_OrderId" });
            DropIndex("dbo.PassengerTours", new[] { "Tour_TourId" });
            DropIndex("dbo.PassengerTours", new[] { "Passenger_PassengerId" });
            DropIndex("dbo.PassengerOrders", new[] { "Order_OrderId" });
            DropIndex("dbo.PassengerOrders", new[] { "Passenger_PassengerId" });
            DropIndex("dbo.TourToAirports", new[] { "AirportId" });
            DropIndex("dbo.TourToAirports", new[] { "TourId" });
            DropIndex("dbo.TourStatus", new[] { "TourId" });
            DropIndex("dbo.BookedPlace", new[] { "TourDetailsId" });
            DropIndex("dbo.TourDetails", new[] { "TourId" });
            DropIndex("dbo.Comforts", new[] { "PlaneId" });
            DropIndex("dbo.TourSearchHistories", new[] { "UserId" });
            DropIndex("dbo.TourSearchHistories", new[] { "ComfortId" });
            DropIndex("dbo.Users", new[] { "UserClaimId" });
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropIndex("dbo.CreditCards", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "CreditCardId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Tours", new[] { "PlaneId" });
            DropIndex("dbo.Feedbacks", new[] { "UserId" });
            DropIndex("dbo.Feedbacks", new[] { "AirportId" });
            DropIndex("dbo.Feedbacks", new[] { "TourId" });
            DropIndex("dbo.Airports", new[] { "AddressId" });
            DropTable("dbo.Dispatchers");
            DropTable("dbo.Clients");
            DropTable("dbo.Admins");
            DropTable("dbo.OrderTours");
            DropTable("dbo.PassengerTours");
            DropTable("dbo.PassengerOrders");
            DropTable("dbo.TourToAirports");
            DropTable("dbo.TourStatus");
            DropTable("dbo.BookedPlace");
            DropTable("dbo.TourDetails");
            DropTable("dbo.Passengers");
            DropTable("dbo.Planes");
            DropTable("dbo.Comforts");
            DropTable("dbo.TourSearchHistories");
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
