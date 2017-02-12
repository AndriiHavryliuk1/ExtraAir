namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withAbstractUser2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreditCards", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Dispatchers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            AddForeignKey("dbo.CreditCards", "UserId", "dbo.Clients", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.Clients", "UserId");
            DropColumn("dbo.Users", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Orders", "UserId", "dbo.Clients");
            DropForeignKey("dbo.CreditCards", "UserId", "dbo.Clients");
            DropForeignKey("dbo.Dispatchers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Clients", "UserId", "dbo.Users");
            DropForeignKey("dbo.Admins", "UserId", "dbo.Users");
            DropIndex("dbo.Dispatchers", new[] { "UserId" });
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropIndex("dbo.Admins", new[] { "UserId" });
            DropTable("dbo.Dispatchers");
            DropTable("dbo.Clients");
            DropTable("dbo.Admins");
            AddForeignKey("dbo.Orders", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.CreditCards", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
