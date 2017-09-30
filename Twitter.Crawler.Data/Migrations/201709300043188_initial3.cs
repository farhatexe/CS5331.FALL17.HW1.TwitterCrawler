namespace Twitter.Crawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TwitterStatusEntities", "StatusId", c => c.Decimal(nullable: false, precision: 20, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TwitterStatusEntities", "StatusId", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
