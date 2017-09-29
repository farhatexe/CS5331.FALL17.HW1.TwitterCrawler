using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;
using TweetSharp;
using Twitter.Crawler.Helpers;
using Twitter.Crawler.Interfaces;

namespace Twitter.Crawler.Access
{
    public class LinqtoTwitter
    {
        private static TwitterContext _twitterContext;

        public LinqtoTwitter(IAccessCredentials accessCredentials)
        {
            var authorization = LinqToTwitterAuthorizer.DoSingleUserAuth(accessCredentials);
            _twitterContext = new TwitterContext(authorization);

        }

        public LinqtoTwitter(string pathToAccessCredentialXml)
        {
            var accessCredentials = new AccessCredentials(pathToAccessCredentialXml);
            var authorization = LinqToTwitterAuthorizer.DoSingleUserAuth(accessCredentials);
            _twitterContext = new TwitterContext(authorization);
            //Console.WriteLine("Rate Limit Remaining: {0}", _twitterContext.ResponseHeaders["X-FeatureRateLimit-Remaining"]);

        }

        public void GetLimits()
        {
            //_twitterContext.RateLimitRemaining
            //Console.WriteLine("Rate Limit Remaining: {0}", _twitterContext.ResponseHeaders["x-rate-limit-remaining"]);
            Console.WriteLine("Rate Limit Remaining: {0}",_twitterContext.RateLimitRemaining);
            Console.ReadLine();
        }


        public async Task GettingRateLimitsAsync()

        {

            var twitterCtx = _twitterContext;
            var helpResponse =

                await

                    (from help in twitterCtx.Help

                        where help.Type == HelpType.RateLimits

                        select help)

                    .SingleOrDefaultAsync();



            if (helpResponse != null && helpResponse.RateLimits != null)

                foreach (var category in helpResponse.RateLimits)

                {

                    Console.WriteLine("\nCategory: {0}", category.Key);



                    foreach (var limit in category.Value)

                    {

                        Console.WriteLine(

                            "\n  Resource: {0}\n    Remaining: {1}\n    Reset: {2}\n    Limit: {3}",

                            limit.Resource, limit.Remaining, limit.Reset, limit.Limit);

                    }

                }

        }
        public async Task DoSearchAsync()

        {
            var twitterCtx = _twitterContext;
            var searchTerm = "HARVEY OR HURRICANEHARVEY OR HOUSTONSTRONG OR HOUSTON";


            Search searchResponse =

                await

                (from search in twitterCtx.Search

                    where search.Type == SearchType.Search &&
                          search.Query == searchTerm &&
                          search.Count == 100  &&
                          search.IncludeEntities == true &&
                          search.GeoCode == "29.74894,-95.35619,50mi"
                          && search.SearchLanguage == "en"
                          select search).SingleOrDefaultAsync();

            if (searchResponse?.Statuses != null)
            {
                
                searchResponse.Statuses.ForEach(tweet =>

                    Console.WriteLine(

                        "\n  User: {0} ({1})\n  Tweet: {2}",

                        tweet.User.ScreenNameResponse,

                        tweet.User.UserIDResponse,

                        tweet.Text ?? tweet.Text));
            }
            else

                Console.WriteLine("No entries found.");

            

        }
        //NextResults	"?max_id=906971238853214210&q=GAME%20VS%20%40NFL%20-filter%3Aretweets%20-filter%3Amedia%20filter%3Asafe&lang=en&count=100&include_entities=1"	string

        public async Task<string> DoSearchReturnResultsAsync()

        {
            var twitterCtx = _twitterContext;
            var searchTerm = "HARVEY OR HURRICANEHARVEY OR HOUSTONSTRONG OR HOUSTON";
            var returnValue = "No entries found";

            Search searchResponse =

                await

                (from search in twitterCtx.Search

                    where search.Type == SearchType.Search &&
                          search.Query == searchTerm &&
                          search.Count == 100 &&
                          search.IncludeEntities == true &&
                          search.GeoCode == "29.74894,-95.35619,50mi"
                          && search.SearchLanguage == "en"
                    select search).SingleOrDefaultAsync();

            if (searchResponse?.Statuses != null)
            {
                returnValue = searchResponse.Serialize();
                //searchResponse.Statuses.ForEach(tweet =>

                //    Console.WriteLine(

                //        "\n  User: {0} ({1})\n  Tweet: {2}",

                //        tweet.User.ScreenNameResponse,

                //        tweet.User.UserIDResponse,

                //        tweet.Text ?? tweet.Text));
            }
            //else

                //Console.WriteLine("No entries found.");

            return returnValue;

        }
        public async Task<List<Status>> DoSearchReturnTweetsAsync()

        {
            var twitterCtx = _twitterContext;
            var searchTerm = "GAME VS @NFL -filter:retweets -filter:media filter:safe";
            //var searchTerm = " VS \"PLAY AGAINST\" \"WATCHING FOOTBALL\" -\"WATCHING FOOTBALL ANYMORE\" -\"DONE WATCHING FOOTBALL\" -\"NOT WATCHING\" -@TheNFLBoycott -boycott @NFL -filter:retweets -filter:media filter:safe";
            var returnValue = new List<Status>();

            Search searchResponse =

                await

                (from search in twitterCtx.Search

                    where search.Type == SearchType.Search &&
                          search.Query == searchTerm &&
                          search.Count == 100 &&
                          search.IncludeEntities == true &&
                          search.SearchLanguage == "en"
                          
                    select search).SingleOrDefaultAsync();

            if (searchResponse?.Statuses != null)
            {
                returnValue = searchResponse.Statuses;
                //searchResponse.Statuses.ForEach(tweet =>

                //    Console.WriteLine(

                //        "\n  User: {0} ({1})\n  Tweet: {2}",

                //        tweet.User.ScreenNameResponse,

                //        tweet.User.UserIDResponse,

                //        tweet.Text ?? tweet.Text));
            }
            //else
            Console.WriteLine("Next Result Max_ID: {0}",searchResponse.SearchMetaData.NextResults);
            Console.WriteLine("Next Result Max_ID: {0}", searchResponse.SearchMetaData.MaxID);
            Console.WriteLine("Found {0} Results",returnValue.Count);
            
            return returnValue;

        }
        public async Task<List<Status>> DoSearchReturnTweetsToMaxAsync()

        {
            var twitterCtx = _twitterContext;
            var searchTerm = "@NFL -filter:retweets -filter:media filter:safe";
            var returnValue = new List<Status>();
            const int maxTotalResults = 1000;



            //string searchTerm = "twitter";



            // oldest id you already have for this search term

            ulong sinceID = 1;



            // used after the first query to track current session

            ulong maxID;



            var combinedSearchResults = new List<Status>();



            List<Status> searchResponse =

                await

                (from search in twitterCtx.Search

                    where search.Type == SearchType.Search &&
                          search.Query == searchTerm &&
                          search.Count == 100 &&
                          search.IncludeEntities == true &&
                          search.SearchLanguage == "en" &&
                          search.SinceID == sinceID

                 select search.Statuses)

                .SingleOrDefaultAsync();



            if (searchResponse != null)

            {

                combinedSearchResults.AddRange(searchResponse);

                ulong previousMaxID = ulong.MaxValue;

                do

                {

                    // one less than the newest id you've just queried

                    maxID = searchResponse.Min(status => status.StatusID) - 1;



                    Debug.Assert(maxID < previousMaxID);

                    previousMaxID = maxID;



                    searchResponse =

                        await

                        (from search in twitterCtx.Search

                         where search.Type == SearchType.Search &&
                                     search.Query == searchTerm &&
                                     search.Count == 100 &&
                                     search.IncludeEntities == true &&
                                     search.SearchLanguage == "en" &&
                                     search.MaxID == maxID &&
                                     search.SinceID == sinceID

                         select search.Statuses)

                        .SingleOrDefaultAsync();
                    var numberOfCallsRemaining =
                        Convert.ToUInt16(_twitterContext.ResponseHeaders["x-rate-limit-remaining"]);
                    Console.WriteLine("Rate Limit Remaining: {0}", numberOfCallsRemaining);

                    combinedSearchResults.AddRange(searchResponse);


                } while (searchResponse.Any() && combinedSearchResults.Count < maxTotalResults);

                returnValue = combinedSearchResults;

                //combinedSearchResults.ForEach(tweet =>

                //    Console.WriteLine(

                //        "\n  User: {0} ({1})\n  Tweet: {2}",

                //        tweet.User.ScreenNameResponse,

                //        tweet.User.UserIDResponse,

                //        tweet.Text ?? tweet.FullText));

            }

            else

            {

                Console.WriteLine("No entries found.");

            }
            Console.WriteLine(returnValue.Count);
            return returnValue;

        }

    }
}