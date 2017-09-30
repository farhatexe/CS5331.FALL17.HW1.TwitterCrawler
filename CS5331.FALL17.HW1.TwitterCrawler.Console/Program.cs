using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Crawler.Access;
using Twitter.Crawler.Helpers;
using Twitter.Crawler.Repository;

namespace CS5331.FALL17.HW1.TwitterCrawler.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var twitterConnection = new ConnectToTwitter();
            //Console.WriteLine("Application Complete");
            var test = new LinqtoTwitter("AccessCredentials.xml");
            //test.DoSearchAsync().Wait();
            //string results = test.DoSearchReturnResultsAsync().Result;//.Wait();
            //results.SaveToFile("Results.xml");
            //var results = test.DoSearchReturnTweetsAsync();
            var repo = new TwitterStatusRepository();
            ///var maxValue = (ulong)repo.GetMaxStatusId();
            ///var results = test.DoSearchReturnTweetsToMaxAsync(ulong.MaxValue, maxValue).Result;//.Wait();
            //results.SaveToFile("ResultTweets.xml");
            ///if (results.Count>0) 
            ///    repo.CreateOrUpdateStatus(results);
            Console.WriteLine("Max Status: {0}", repo.GetMaxStatusId());
            Console.WriteLine("Min Status: {0}", repo.GetMinStatusId());
            Console.WriteLine(repo.RemoveDuplicates());
            repo.GetAllTags();
            Console.ReadLine();
            //test.GetLimits();
        }


    }
}
