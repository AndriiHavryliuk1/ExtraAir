namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTourSearchHistory4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DateStartTour", c => c.DateTime());
            AddColumn("dbo.Orders", "DateFinishTour", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DateFinishTour");
            DropColumn("dbo.Orders", "DateStartTour");
        }
    }
}
