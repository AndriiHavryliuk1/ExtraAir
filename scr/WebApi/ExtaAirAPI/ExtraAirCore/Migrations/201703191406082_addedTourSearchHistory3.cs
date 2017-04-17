namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTourSearchHistory3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CreditCardId", "dbo.CreditCards");
            DropIndex("dbo.Orders", new[] { "CreditCardId" });
            AlterColumn("dbo.Orders", "CreditCardId", c => c.Int());
            CreateIndex("dbo.Orders", "CreditCardId");
            AddForeignKey("dbo.Orders", "CreditCardId", "dbo.CreditCards", "CreditCardId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CreditCardId", "dbo.CreditCards");
            DropIndex("dbo.Orders", new[] { "CreditCardId" });
            AlterColumn("dbo.Orders", "CreditCardId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CreditCardId");
            AddForeignKey("dbo.Orders", "CreditCardId", "dbo.CreditCards", "CreditCardId", cascadeDelete: true);
        }
    }
}
