namespace Twitter.Crawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TwitterCoordinatesEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsLocationAvailable = c.Boolean(nullable: false),
                        TwitterPlaceEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TwitterPlaceEntities", t => t.TwitterPlaceEntity_Id)
                .Index(t => t.TwitterPlaceEntity_Id);
            
            CreateTable(
                "dbo.TwitterPlaceEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryCode = c.String(),
                        Country = c.String(),
                        PlaceType = c.String(),
                        Url = c.String(),
                        FullName = c.String(),
                        ContainedWithin_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TwitterPlaceEntities", t => t.ContainedWithin_Id)
                .Index(t => t.ContainedWithin_Id);
            
            CreateTable(
                "dbo.TwitterStatusEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScreenName = c.String(),
                        SinceID = c.Int(nullable: false),
                        MaxID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Cursor = c.Long(nullable: false),
                        IncludeRetweets = c.Boolean(nullable: false),
                        ExcludeReplies = c.Boolean(nullable: false),
                        IncludeEntities = c.Boolean(nullable: false),
                        IncludeUserEntities = c.Boolean(nullable: false),
                        IncludeMyRetweet = c.Boolean(nullable: false),
                        OEmbedUrl = c.String(),
                        OEmbedMaxWidth = c.Int(nullable: false),
                        OEmbedHideMedia = c.Boolean(nullable: false),
                        OEmbedHideThread = c.Boolean(nullable: false),
                        OEmbedOmitScript = c.Boolean(nullable: false),
                        OEmbedRelated = c.String(),
                        OEmbedLanguage = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Text = c.String(),
                        Source = c.String(),
                        Truncated = c.Boolean(nullable: false),
                        InReplyToStatusID = c.Long(nullable: false),
                        InReplyToUserID = c.Long(nullable: false),
                        FavoriteCount = c.Int(),
                        Favorited = c.Boolean(nullable: false),
                        InReplyToScreenName = c.String(),
                        TrimUser = c.Boolean(nullable: false),
                        IncludeContributorDetails = c.Boolean(nullable: false),
                        RetweetCount = c.Int(nullable: false),
                        Retweeted = c.Boolean(nullable: false),
                        PossiblySensitive = c.Boolean(nullable: false),
                        CurrentUserRetweet = c.Int(nullable: false),
                        QuotedStatusId = c.Int(nullable: false),
                        WithheldCopyright = c.Boolean(nullable: false),
                        WithheldScope = c.String(),
                        Lang = c.String(),
                        Map = c.Boolean(nullable: false),
                        TweetIDs = c.String(),
                        Coordinates_Id = c.Int(),
                        Place_Id = c.Int(),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TwitterCoordinatesEntities", t => t.Coordinates_Id)
                .ForeignKey("dbo.TwitterPlaceEntities", t => t.Place_Id)
                .ForeignKey("dbo.TwitterUserEntities", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Coordinates_Id)
                .Index(t => t.Place_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TwitterUserEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        UserId = c.Long(nullable: false),
                        UserIdList = c.String(),
                        ScreenName = c.String(),
                        ScreenNameList = c.String(),
                        Page = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Cursor = c.Long(nullable: false),
                        Slug = c.String(),
                        Query = c.String(),
                        IncludeEntities = c.Boolean(nullable: false),
                        SkipStatus = c.Boolean(nullable: false),
                        UserIdResponse = c.String(),
                        ScreenNameResponse = c.String(),
                        Name = c.String(),
                        Location = c.String(),
                        Description = c.String(),
                        ProfileImageUrl = c.String(),
                        ProfileImageUrlHttps = c.String(),
                        DefaultProfileImage = c.Boolean(nullable: false),
                        Url = c.String(),
                        DefaultProfile = c.Boolean(nullable: false),
                        Protected = c.Boolean(nullable: false),
                        FollowersCount = c.Int(nullable: false),
                        ProfileBackgroundColor = c.String(),
                        ProfileTextColor = c.String(),
                        ProfileLinkColor = c.String(),
                        ProfileSidebarFillColor = c.String(),
                        ProfileSidebarBorderColor = c.String(),
                        FriendsCount = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        FavoritesCount = c.Int(nullable: false),
                        UtcOffset = c.Int(nullable: false),
                        TimeZone = c.String(),
                        ProfileBackgroundImageUrl = c.String(),
                        ProfileBackgroundImageUrlHttps = c.String(),
                        ProfileBackgroundTile = c.Boolean(nullable: false),
                        ProfileUseBackgroundImage = c.Boolean(nullable: false),
                        StatusesCount = c.Int(nullable: false),
                        Notifications = c.Boolean(nullable: false),
                        GeoEnabled = c.Boolean(nullable: false),
                        Verified = c.Boolean(nullable: false),
                        ContributorsEnabled = c.Boolean(nullable: false),
                        IsTranslator = c.Boolean(nullable: false),
                        Following = c.Boolean(nullable: false),
                        Lang = c.String(),
                        LangResponse = c.String(),
                        ShowAllInlineMedia = c.Boolean(nullable: false),
                        ListedCount = c.Int(nullable: false),
                        FollowRequestSent = c.Boolean(nullable: false),
                        ProfileImage = c.String(),
                        ProfileBannerUrl = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TwitterStatusEntities", "User_Id", "dbo.TwitterUserEntities");
            DropForeignKey("dbo.TwitterStatusEntities", "Place_Id", "dbo.TwitterPlaceEntities");
            DropForeignKey("dbo.TwitterStatusEntities", "Coordinates_Id", "dbo.TwitterCoordinatesEntities");
            DropForeignKey("dbo.TwitterCoordinatesEntities", "TwitterPlaceEntity_Id", "dbo.TwitterPlaceEntities");
            DropForeignKey("dbo.TwitterPlaceEntities", "ContainedWithin_Id", "dbo.TwitterPlaceEntities");
            DropIndex("dbo.TwitterStatusEntities", new[] { "User_Id" });
            DropIndex("dbo.TwitterStatusEntities", new[] { "Place_Id" });
            DropIndex("dbo.TwitterStatusEntities", new[] { "Coordinates_Id" });
            DropIndex("dbo.TwitterPlaceEntities", new[] { "ContainedWithin_Id" });
            DropIndex("dbo.TwitterCoordinatesEntities", new[] { "TwitterPlaceEntity_Id" });
            DropTable("dbo.TwitterUserEntities");
            DropTable("dbo.TwitterStatusEntities");
            DropTable("dbo.TwitterPlaceEntities");
            DropTable("dbo.TwitterCoordinatesEntities");
        }
    }
}
