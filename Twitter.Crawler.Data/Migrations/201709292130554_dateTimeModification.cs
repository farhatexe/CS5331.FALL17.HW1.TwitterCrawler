namespace Twitter.Crawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateTimeModification : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TwitterStatusEntities", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TwitterUserEntities", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TwitterUserEntities", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TwitterStatusEntities", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
