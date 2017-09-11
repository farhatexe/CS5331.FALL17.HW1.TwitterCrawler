using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;
using TweetSharp;

namespace Twitter.Crawler.Access
{
    public class LinqtoTwitter
    {
        private static TwitterContext _twitterContext;

        public LinqtoTwitter()
        {
            var Authorization = DoSingleUserAuth();// testApplicationOnlyAuthorizer = new ApplicationOnlyAuthorizer();
            _twitterContext = new TwitterContext(Authorization);
            //await DoSearchAsync(twitterContext);

        }



        static IAuthorizer DoSingleUserAuth()
         {

            var auth = new SingleUserAuthorizer

            {

                CredentialStore = new SingleUserInMemoryCredentialStore

                {

                    ConsumerKey = "jhG2yRAjc99MarGR8zgSdiSQa",

                    ConsumerSecret = "63e3WsUGBmY9maeHwIith8XwO2vhYNMVe8FUVZ9jt4rca8AqBf",

                    AccessToken = "905253320884879361-FKRVrhPBf0vOSjTlAZqqe3NOvf2ITI3",

                    AccessTokenSecret = "6ZgAiCqf6dADMO9PjumR5xoToanlwSFw6xZ1Qh48K8QDX"

                }

            };



            return auth;

        }

        public async Task DoSearchAsync()

        {
            TwitterContext twitterCtx = _twitterContext;
            string searchTerm = "HARVEY OR HURRICANEHARVEY OR HOUSTONSTRONG OR HOUSTON";

            //string searchTerm = "#ömer -RT -instagram news source%3Afoursquare";



            Search searchResponse =

                await

                (from search in twitterCtx.Search

                    where search.Type == SearchType.Search &&

                          search.Query == searchTerm &&
                          search.Count == 100  &&
                          search.IncludeEntities == true &&
                          search.GeoCode == "29.74894,-95.35619,50mi"

                     select search)

                    .SingleOrDefaultAsync();



            if (searchResponse?.Statuses != null)

                searchResponse.Statuses.ForEach(tweet =>

                    Console.WriteLine(

                        "\n  User: {0} ({1})\n  Tweet: {2}",

                        tweet.User.ScreenNameResponse,

                        tweet.User.UserIDResponse,

                        tweet.Text ?? tweet.Text));

            else

                Console.WriteLine("No entries found.");

        }


    }
}