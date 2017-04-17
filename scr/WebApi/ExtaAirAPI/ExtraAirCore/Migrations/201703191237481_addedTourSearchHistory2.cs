namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTourSearchHistory2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TourSearchHistories", "DateStart", c => c.String());
            AlterColumn("dbo.TourSearchHistories", "DateSearch", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TourSearchHistories", "DateSearch", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TourSearchHistories", "DateStart", c => c.DateTime(nullable: false));
        }
    }
}
