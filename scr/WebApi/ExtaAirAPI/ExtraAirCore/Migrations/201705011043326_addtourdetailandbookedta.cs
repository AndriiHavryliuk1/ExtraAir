namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtourdetailandbookedta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourDetails", "Temporary", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourDetails", "Temporary");
        }
    }
}
