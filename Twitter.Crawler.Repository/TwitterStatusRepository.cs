using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using LinqToTwitter;
using Twitter.Crawler.Data;
using Twitter.Crawler.Model.Entities;

namespace Twitter.Crawler.Repository
{
    public class TwitterStatusRepository
    {
        private readonly TwitterCrawlerContext _twitterDbContext;
        public TwitterStatusRepository(TwitterCrawlerContext twitterDbContext = null)
        {
            _twitterDbContext = twitterDbContext ?? new TwitterCrawlerContext();
        }

        public void CreateOrUpdateStatus(Status statusToAdd)
        {
            using (_twitterDbContext)
            {
                var translatedStatus = new TwitterStatusEntity(statusToAdd);
                _twitterDbContext.TwitterStatusEntities.Add(translatedStatus);
            }
        }

        public void CreateOrUpdateStatus(IEnumerable<Status> statusesToAdd)
        {
            List<TwitterStatusEntity> statusesTranslatedToAdd = new List<TwitterStatusEntity>();
            foreach (var statusToAdd in statusesToAdd)
            {
                try
                {
                    statusesTranslatedToAdd.Add(new TwitterStatusEntity(statusToAdd));
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error translating record");
                }
            }
            using (_twitterDbContext)
            {
               
                _twitterDbContext.TwitterStatusEntities.AddRange(statusesTranslatedToAdd);
            }
        }
    }
}