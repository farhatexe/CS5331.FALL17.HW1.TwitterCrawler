namespace Twitter.Crawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TwitterStatusEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScreenName = c.String(),
                        StatusId = c.String(),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Text = c.String(),
                        Truncated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TwitterStatusEntities");
        }
    }
}
