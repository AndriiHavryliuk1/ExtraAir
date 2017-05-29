namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjklj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Birthday", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Birthday", c => c.DateTime(nullable: false));
        }
    }
}
