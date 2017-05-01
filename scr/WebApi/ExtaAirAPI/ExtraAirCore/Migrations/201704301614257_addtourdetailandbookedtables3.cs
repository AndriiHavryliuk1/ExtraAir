namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtourdetailandbookedtables3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourDetails", "CurrentCountOfBusinessPassenger", c => c.Int(nullable: false));
            AddColumn("dbo.TourDetails", "CurrentCountOfEconomyPassenger", c => c.Int(nullable: false));
            DropColumn("dbo.TourDetails", "CurrentCountPassenger");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TourDetails", "CurrentCountPassenger", c => c.Int(nullable: false));
            DropColumn("dbo.TourDetails", "CurrentCountOfEconomyPassenger");
            DropColumn("dbo.TourDetails", "CurrentCountOfBusinessPassenger");
        }
    }
}
