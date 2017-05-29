namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lkjl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsActive", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsActive");
        }
    }
}
