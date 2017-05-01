namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtourdetailandbookedtables4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookedPlace", "ComfortType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookedPlace", "ComfortType");
        }
    }
}
