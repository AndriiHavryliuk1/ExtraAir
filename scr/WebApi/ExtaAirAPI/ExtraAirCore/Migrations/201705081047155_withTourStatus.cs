namespace ExtraAirCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withTourStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourStatus",
                c => new
                    {
                        TourStatusId = c.Int(nullable: false, identity: true),
                        DateStart = c.DateTime(nullable: false),
                        DateFinish = c.DateTime(nullable: false),
                        TourStatusType = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourStatusId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourStatus", "TourId", "dbo.Tours");
            DropIndex("dbo.TourStatus", new[] { "TourId" });
            DropTable("dbo.TourStatus");
        }
    }
}
