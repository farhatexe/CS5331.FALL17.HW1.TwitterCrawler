using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Crawler.Access;

namespace CS5331.FALL17.HW1.TwitterCrawler.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var twitterConnection = new ConnectToTwitter();
            //Console.WriteLine("Application Complete");
            var test = new LinqtoTwitter();
            test.DoSearchAsync();
            Console.ReadLine();
        }
    }
}
