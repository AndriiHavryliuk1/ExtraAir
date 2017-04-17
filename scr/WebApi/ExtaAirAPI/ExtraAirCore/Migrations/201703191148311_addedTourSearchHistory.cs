namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTourSearchHistory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comforts", "PlaneId", "dbo.Planes");
            DropIndex("dbo.Comforts", new[] { "PlaneId" });
            CreateTable(
                "dbo.TourSearchHistories",
                c => new
                    {
                        TourSearchHistoryId = c.Int(nullable: false, identity: true),
                        DateStart = c.DateTime(nullable: false),
                        DateSearch = c.DateTime(nullable: false),
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
            
            AddColumn("dbo.Comforts", "PriceCorf", c => c.Double(nullable: false));
            AlterColumn("dbo.Comforts", "PlaneId", c => c.Int());
            CreateIndex("dbo.Comforts", "PlaneId");
            AddForeignKey("dbo.Comforts", "PlaneId", "dbo.Planes", "PlaneId");
            DropColumn("dbo.Comforts", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comforts", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.Comforts", "PlaneId", "dbo.Planes");
            DropForeignKey("dbo.TourSearchHistories", "UserId", "dbo.Clients");
            DropForeignKey("dbo.TourSearchHistories", "UserId", "dbo.Users");
            DropForeignKey("dbo.TourSearchHistories", "ComfortId", "dbo.Comforts");
            DropIndex("dbo.Comforts", new[] { "PlaneId" });
            DropIndex("dbo.TourSearchHistories", new[] { "UserId" });
            DropIndex("dbo.TourSearchHistories", new[] { "ComfortId" });
            AlterColumn("dbo.Comforts", "PlaneId", c => c.Int(nullable: false));
            DropColumn("dbo.Comforts", "PriceCorf");
            DropTable("dbo.TourSearchHistories");
            CreateIndex("dbo.Comforts", "PlaneId");
            AddForeignKey("dbo.Comforts", "PlaneId", "dbo.Planes", "PlaneId", cascadeDelete: true);
        }
    }
}
