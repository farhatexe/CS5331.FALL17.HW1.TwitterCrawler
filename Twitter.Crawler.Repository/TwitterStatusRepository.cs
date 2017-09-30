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
    public class TwitterStatusRepository : IDisposable
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
                        FieldA = r.StatusId
                    }
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
                    var crawlerClass = new TwitterCrawlerClassEntity(textValue);
                    //if(classList.Contains(crawlerClass))
                    //    continue;
                    classList.Add(crawlerClass);
                }
            }
            _twitterDbContext.TwitterCrawlerClasses.AddRange(classList);
            _twitterDbContext.SaveChanges();
            Console.WriteLine(classList.Count());
        }
        public void Dispose()
        {
            _twitterDbContext?.Dispose();
        }

        public int RemoveDuplicateClasses()
        {
            //var results = _twitterDbContext.TwitterCrawlerClasses.GroupBy(x => x.ClassText).Select(y=>y.FirstOrDefault()).ToList();
            var results = _twitterDbContext.TwitterCrawlerClasses.ToList();
            var removalList = new List<TwitterCrawlerClassEntity>();
            Console.WriteLine(results.Count());
            var testClassAlreadyFound = new HashSet<string>();
            foreach (var VARIABLE in results)
            {
                if (testClassAlreadyFound.Contains(VARIABLE.ClassText))
                {
                    continue;
                }
                VARIABLE.IsUnique = true;
                testClassAlreadyFound.Add(VARIABLE.ClassText);
                //VARIABLE.IsUnique = true;
                //break;
                //if (!results.Contains(VARIABLE))
                //{
                //    removalList.Add(VARIABLE);
                //}
            }
            _twitterDbContext.SaveChanges();
            //Console.WriteLine(updates);
            //var query = collection.GroupBy(x => x.title).Select(y => y.FirstOrDefault());
            //var duplicates = 1;
            //var q = from r in _twitterDbContext.TwitterCrawlerClasses
            //    group r by new
            //    {
            //        FieldA = r.ClassText
            //    }
            //    into g
            //    where g.Count() > 10000
            //    select g;
            //foreach (var g in q)
            //{
            //    var dupes = g.Skip(1).ToList();
            //    foreach (var record in dupes)
            //    {
            //        Console.WriteLine("Duplicate {0} found", record.ClassText);
            //        _twitterDbContext.TwitterCrawlerClasses.Remove(record);//.DeleteObject(record);
            //        duplicates++;
            //    }
            //}
            //_twitterDbContext.SaveChanges();
            return 1;
        }

        public void UpdateCount()
        {
            var results = _twitterDbContext.TwitterCrawlerClasses.Where(x=>x.IsUnique).ToList();
            foreach (var VARIABLE in results)
            {
                var numResults = _twitterDbContext.TwitterCrawlerClasses.Where(x => x.ClassText == VARIABLE.ClassText)
                    .ToList().Count();
                VARIABLE.InstanceCount = numResults;
            }
            _twitterDbContext.SaveChanges();
        }

        public void FlagStopWords()
        {
            var results = _twitterDbContext.TwitterCrawlerClasses.Where(x => x.IsUnique).ToList();
            var stopWordsObj = new StopWords();
            foreach (var VARIABLE in results)
            {
                if (stopWordsObj.StopWordsSet.Contains(VARIABLE.ClassText))
                {
                    VARIABLE.IsStopWord = true;
                }
            }
            _twitterDbContext.SaveChanges();
        }

        public void GetClassesSortedByRanking()
        {
            var results = _twitterDbContext.TwitterCrawlerClasses.Where(x => x.IsUnique).Where(y => !y.IsStopWord).OrderByDescending(o=>o.InstanceCount)
                .ToList();
            for(var i = 0; i<10; i++)
            {
                Console.WriteLine("Result: {0} Count: {1} Id: {2}", results[i].ClassText, results[i].InstanceCount, results[i].Id);
            }
            
        }

    }
}