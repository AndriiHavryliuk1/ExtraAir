namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtou : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ImagePath", c => c.String());
            AddColumn("dbo.Users", "IdCard", c => c.String());
            AddColumn("dbo.Passengers", "FirstName", c => c.String());
            AddColumn("dbo.Passengers", "LastName", c => c.String());
            AddColumn("dbo.Passengers", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Passengers", "CoordinateValue", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Passengers", "CoordinateValue");
            DropColumn("dbo.Passengers", "Gender");
            DropColumn("dbo.Passengers", "LastName");
            DropColumn("dbo.Passengers", "FirstName");
            DropColumn("dbo.Users", "IdCard");
            DropColumn("dbo.Users", "ImagePath");
        }
    }
}
