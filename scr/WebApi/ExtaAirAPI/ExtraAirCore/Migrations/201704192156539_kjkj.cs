namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjkj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "StringDays", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "StringDays");
        }
    }
}
