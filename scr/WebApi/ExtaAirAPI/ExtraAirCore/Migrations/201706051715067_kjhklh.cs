namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjhklh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "Deleted", c => c.Boolean());
            AddColumn("dbo.TourDetails", "DatePushed", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourDetails", "DatePushed");
            DropColumn("dbo.Tours", "Deleted");
        }
    }
}
