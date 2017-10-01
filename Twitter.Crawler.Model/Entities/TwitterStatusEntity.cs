using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LinqToTwitter;

namespace Twitter.Crawler.Model.Entities
{
    public class TwitterStatusEntity
    {
        //private Status _linqToTwitterStatus;
        public TwitterStatusEntity()
        {

        }

        public TwitterStatusEntity(Status linqToTwitterStatus)
        {
            var _linqToTwitterStatus = linqToTwitterStatus;
            ScreenName = _linqToTwitterStatus.User.ScreenNameResponse;
            StatusId = _linqToTwitterStatus.StatusID;
            Text = _linqToTwitterStatus.Text;
            CreatedAt = _linqToTwitterStatus.CreatedAt;
            Truncated = _linqToTwitterStatus.Truncated;
            
        }
        public int Id { get; set; }
        
        public string ScreenName { get; set; }

        public decimal StatusId { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public string Text { get; set; }
        
        public bool Truncated { get; set; }
        
        public bool? TrainingResult { get; set; }
    }
}