namespace Twitter.Crawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialTranlated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TwitterStatusEntities", "TweetId", c => c.String());
            AlterColumn("dbo.TwitterStatusEntities", "SinceID", c => c.String());
            AlterColumn("dbo.TwitterStatusEntities", "MaxID", c => c.String());
            AlterColumn("dbo.TwitterUserEntities", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TwitterUserEntities", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.TwitterStatusEntities", "MaxID", c => c.Int(nullable: false));
            AlterColumn("dbo.TwitterStatusEntities", "SinceID", c => c.Int(nullable: false));
            DropColumn("dbo.TwitterStatusEntities", "TweetId");
        }
    }
}
