namespace Twitter.Crawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TwitterCrawlerClassEntities", "IsStopWord", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TwitterCrawlerClassEntities", "IsStopWord");
        }
    }
}
