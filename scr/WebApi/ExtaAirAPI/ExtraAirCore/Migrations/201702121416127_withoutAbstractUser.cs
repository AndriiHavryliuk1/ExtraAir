namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withoutAbstractUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Admins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Clients", "UserId", "dbo.Users");
            DropIndex("dbo.Admins", new[] { "UserId" });
            DropIndex("dbo.Clients", new[] { "UserId" });
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.Admins");
            DropTable("dbo.Clients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            DropColumn("dbo.Users", "Discriminator");
            CreateIndex("dbo.Clients", "UserId");
            CreateIndex("dbo.Admins", "UserId");
            AddForeignKey("dbo.Clients", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Admins", "UserId", "dbo.Users", "UserId");
        }
    }
}
