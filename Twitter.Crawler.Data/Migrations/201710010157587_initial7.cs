namespace Twitter.Crawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TwitterStatusEntities", "TrainingResult", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TwitterStatusEntities", "TrainingResult");
        }
    }
}
