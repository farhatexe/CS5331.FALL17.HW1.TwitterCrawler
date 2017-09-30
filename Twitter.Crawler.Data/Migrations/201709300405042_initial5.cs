namespace Twitter.Crawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TwitterCrawlerClassEntities", "IsUnique", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TwitterCrawlerClassEntities", "IsUnique");
        }
    }
}
