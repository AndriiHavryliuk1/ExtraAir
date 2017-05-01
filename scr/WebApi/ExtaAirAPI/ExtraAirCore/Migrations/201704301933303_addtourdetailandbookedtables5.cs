namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtourdetailandbookedtables5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TourDetails", "TourId", "dbo.Tours");
            DropIndex("dbo.TourDetails", new[] { "TourId" });
            AlterColumn("dbo.TourDetails", "TourId", c => c.Int());
            CreateIndex("dbo.TourDetails", "TourId");
            AddForeignKey("dbo.TourDetails", "TourId", "dbo.Tours", "TourId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourDetails", "TourId", "dbo.Tours");
            DropIndex("dbo.TourDetails", new[] { "TourId" });
            AlterColumn("dbo.TourDetails", "TourId", c => c.Int(nullable: false));
            CreateIndex("dbo.TourDetails", "TourId");
            AddForeignKey("dbo.TourDetails", "TourId", "dbo.Tours", "TourId", cascadeDelete: true);
        }
    }
}
