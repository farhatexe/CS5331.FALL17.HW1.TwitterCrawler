using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Crawler.Access;
using Twitter.Crawler.Helpers;

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
            var results = test.DoSearchReturnTweetsToMaxAsync();
            results.SaveToFile("ResultTweets.xml");
            //Console.WriteLine("Results: {0}", results.);
            //test.GettingRateLimitsAsync().Wait();
            test.GetLimits();
        }


    }
}
