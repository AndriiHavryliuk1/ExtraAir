namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtourdetailandbookedtables2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Planes", "MaxCountPassengerEconomy", c => c.Int(nullable: false));
            AddColumn("dbo.Planes", "MaxCountPassengerBusiness", c => c.Int(nullable: false));
            DropColumn("dbo.Planes", "MaxCountPassenger");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Planes", "MaxCountPassenger", c => c.Int(nullable: false));
            DropColumn("dbo.Planes", "MaxCountPassengerBusiness");
            DropColumn("dbo.Planes", "MaxCountPassengerEconomy");
        }
    }
}
