namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kldnlkn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passengers", "IdCard", c => c.String());
            AddColumn("dbo.Passengers", "TicketPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Passengers", "BaggageInternal", c => c.Boolean(nullable: false));
            AddColumn("dbo.Passengers", "BaggageeExternal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Passengers", "BaggageeExternal");
            DropColumn("dbo.Passengers", "BaggageInternal");
            DropColumn("dbo.Passengers", "TicketPrice");
            DropColumn("dbo.Passengers", "IdCard");
        }
    }
}
