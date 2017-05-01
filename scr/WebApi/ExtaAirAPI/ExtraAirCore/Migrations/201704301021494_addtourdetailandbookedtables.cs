namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtourdetailandbookedtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourDetails",
                c => new
                    {
                        TourDetailsId = c.Int(nullable: false, identity: true),
                        DateStart = c.DateTime(nullable: false),
                        DateFinish = c.DateTime(nullable: false),
                        CurrentCountPassenger = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourDetailsId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.BookedPlace",
                c => new
                    {
                        BookedPlaceId = c.Int(nullable: false, identity: true),
                        PointX = c.Int(nullable: false),
                        PointY = c.Int(nullable: false),
                        TourDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookedPlaceId)
                .ForeignKey("dbo.TourDetails", t => t.TourDetailsId, cascadeDelete: true)
                .Index(t => t.TourDetailsId);
            
            DropColumn("dbo.Tours", "CurrentCountPassenger");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tours", "CurrentCountPassenger", c => c.Int(nullable: false));
            DropForeignKey("dbo.TourDetails", "TourId", "dbo.Tours");
            DropForeignKey("dbo.BookedPlace", "TourDetailsId", "dbo.TourDetails");
            DropIndex("dbo.BookedPlace", new[] { "TourDetailsId" });
            DropIndex("dbo.TourDetails", new[] { "TourId" });
            DropTable("dbo.BookedPlace");
            DropTable("dbo.TourDetails");
        }
    }
}
