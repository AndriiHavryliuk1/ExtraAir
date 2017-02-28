namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tours", "OrderId", "dbo.Orders");
            DropIndex("dbo.Tours", new[] { "OrderId" });
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
            
            DropColumn("dbo.Tours", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tours", "OrderId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderTours", "Tour_TourId", "dbo.Tours");
            DropForeignKey("dbo.OrderTours", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.OrderTours", new[] { "Tour_TourId" });
            DropIndex("dbo.OrderTours", new[] { "Order_OrderId" });
            DropTable("dbo.OrderTours");
            CreateIndex("dbo.Tours", "OrderId");
            AddForeignKey("dbo.Tours", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
        }
    }
}
