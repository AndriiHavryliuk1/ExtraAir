namespace ExtraAirCore.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class withAbstractUser : DbMigration
	{
		public override void Up()
		{
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

		}

		public override void Down()
		{
			DropForeignKey("dbo.Clients", "UserId", "dbo.Users");
			DropForeignKey("dbo.Admins", "UserId", "dbo.Users");
			DropIndex("dbo.Clients", new[] { "UserId" });
			DropIndex("dbo.Admins", new[] { "UserId" });
			DropTable("dbo.Clients");
			DropTable("dbo.Admins");
		}
	}
}
