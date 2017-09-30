using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using LinqToTwitter;
using Twitter.Crawler.Data;
using Twitter.Crawler.Model.Entities;

namespace Twitter.Crawler.Repository
{
    public class TwitterStatusRepository:IDisposable
    {
        private readonly TwitterCrawlerContext _twitterDbContext;
        public TwitterStatusRepository(TwitterCrawlerContext twitterDbContext = null)
        {
            _twitterDbContext = twitterDbContext ?? new TwitterCrawlerContext();
        }

        public void CreateOrUpdateStatus(Status statusToAdd)
        {
            //using (_twitterDbContext)
            //{
                var translatedStatus = new TwitterStatusEntity(statusToAdd);
                _twitterDbContext.TwitterStatusEntities.Add(translatedStatus);
                _twitterDbContext.SaveChanges();
            //}
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
            //using (_twitterDbContext)
            ///{
               
                _twitterDbContext.TwitterStatusEntities.AddRange(statusesTranslatedToAdd);
                _twitterDbContext.SaveChanges();
            //}
        }

        public decimal GetMaxStatusId()
        {
            //using (_twitterDbContext)
            //{
                var test = _twitterDbContext.TwitterStatusEntities.Select(x => (x.StatusId)).ToList();
                return test.Max();
            //}
            
        }

        public decimal GetMinStatusId()
        {
            /////using (_twitterDbContext)
            ////{
                var test = _twitterDbContext.TwitterStatusEntities.Select(x => (x.StatusId)).ToList();
                return test.Min();
            //}
            
        }

        public int RemoveDuplicates()
        {
            var duplicates = 1;
            var q = from r in _twitterDbContext.TwitterStatusEntities
                group r by new
                {
                    FieldA = r.StatusId}
                    into g
                    where g.Count() > 1
                    select g;
                    foreach (var g in q)
                    {
                    var dupes = g.Skip(1).ToList();
                    foreach (var record in dupes)
                    {
                    _twitterDbContext.TwitterStatusEntities.Remove(record);//.DeleteObject(record);
                        duplicates++;
                    }
            }
            _twitterDbContext.SaveChanges();
            return duplicates;
        }

        public void GetAllTags()
        {
            var deviceIds = _twitterDbContext.TwitterStatusEntities.Select(d => d.Text).AsEnumerable();
            //IEnumerable<string> classList = new List<string>();
            List<TwitterCrawlerClassEntity> classList = new List<TwitterCrawlerClassEntity>();
            foreach (var text in deviceIds)
            {
                var textInLowerCase = text.ToLowerInvariant();
                var textWithoutPunctuation = Regex.Replace(textInLowerCase, @"[^\w\s@#]", "");
                var textSplit = textWithoutPunctuation.Split(new char[0]);
                //classList = classList.AsQueryable().Union(textSplit);
                //_twitterDbContext.TwitterStatusEntities.AddRange(textSplit)
                foreach (var textValue in textSplit)
                {
                    classList.Add(new TwitterCrawlerClassEntity(textValue));
                }
            }
            _twitterDbContext.SaveChanges();
            Console.WriteLine(classList.Count());
        }
        public void Dispose()
        {
            _twitterDbContext?.Dispose();
        }
    }
}