namespace Twitter.Crawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TwitterCrawlerClassEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassText = c.String(),
                        InstanceCount = c.Int(nullable: false),
                        IsHashTag = c.Boolean(nullable: false),
                        IsAddress = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TwitterCrawlerClassEntities");
        }
    }
}
