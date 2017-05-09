namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lkgj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourStatus", "AirportFromId", c => c.Int());
            AddColumn("dbo.TourStatus", "AirportToId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourStatus", "AirportToId");
            DropColumn("dbo.TourStatus", "AirportFromId");
        }
    }
}
