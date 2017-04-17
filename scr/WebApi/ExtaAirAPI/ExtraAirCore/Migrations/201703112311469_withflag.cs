namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withflag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourToAirports", "isInterim", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourToAirports", "isInterim");
        }
    }
}
