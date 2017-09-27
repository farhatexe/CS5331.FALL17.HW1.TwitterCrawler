using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Twitter.Crawler.Model.Entities
{
    public class TwitterStatusEntity
    {
        public int Id { get; set; }
        
        public string ScreenName { get; set; }

        public int SinceID { get; set; }

        public int MaxID { get; set; }

        public int Count { get; set; }

        public long Cursor { get; set; }

        public bool IncludeRetweets { get; set; }

        public bool ExcludeReplies { get; set; }

        public bool IncludeEntities { get; set; }

        public bool IncludeUserEntities { get; set; }

        public bool IncludeMyRetweet { get; set; }

        public string OEmbedUrl { get; set; }

        public int OEmbedMaxWidth { get; set; }

        public bool OEmbedHideMedia { get; set; }

        public bool OEmbedHideThread { get; set; }

        public bool OEmbedOmitScript { get; set; }

        public string OEmbedRelated { get; set; }

        public string OEmbedLanguage { get; set; }

        public DateTime CreatedAt { get; set; }

        //public ulong StatusID { get; set; }

        public string Text { get; set; }

        public string Source { get; set; }

        public bool Truncated { get; set; }

        public long InReplyToStatusID { get; set; }

        public long InReplyToUserID { get; set; }

        public int? FavoriteCount { get; set; }

        public bool Favorited { get; set; }

        public string InReplyToScreenName { get; set; }

        [Required]
        public TwitterUserEntity User { get; set; }

        //public List<long> Users { get; set; }

        public TwitterCoordinatesEntity Coordinates { get; set; }

        public TwitterPlaceEntity Place { get; set; }

        public bool TrimUser { get; set; }

        public bool IncludeContributorDetails { get; set; }

        public int RetweetCount { get; set; }

        public bool Retweeted { get; set; }

        public bool PossiblySensitive { get; set; }

        //public Status RetweetedStatus { get; set; }

        public int CurrentUserRetweet { get; set; }

        public int QuotedStatusId { get; set; }

        //public Status QuotedStatus { get; set; }
     

        public bool WithheldCopyright { get; set; }

 

        public string WithheldScope { get; set; }
        
        public string Lang { get; set; }

        public bool Map { get; set; }

        public string TweetIDs { get; set; }

    }
}