using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hammock;
using TweetSharp;

namespace Twitter.Crawler.Access
{
    public class ConnectToTwitter
    {
        private string accessConsumerKey = "jhG2yRAjc99MarGR8zgSdiSQa";
        private string accessApiSecret = "63e3WsUGBmY9maeHwIith8XwO2vhYNMVe8FUVZ9jt4rca8AqBf";

        private string accessToken = "905253320884879361-ulTFzR0yIloiRMVx1u8OA9l93RolkPU";
        private string accessTokenSecret = "95f61KxcjJzaxPLTkyTzWFuhhbiucp6eN2VAANckc1jvk";


        public ConnectToTwitter()
        {

            var twitterService = new TwitterService(accessConsumerKey, accessApiSecret);

            // Step 1 - Retrieve an OAuth Request Token
            OAuthRequestToken requestToken = twitterService.GetRequestToken();

            // Step 2 - Redirect to the OAuth Authorization URL
            Uri uri = twitterService.GetAuthorizationUri(requestToken);
            Process.Start(uri.ToString());

            Console.WriteLine("Enter the access token: ");
            var verifier = Console.ReadLine();
            // Step 3 - Exchange the Request Token for an Access Token
            //string verifier = "9242027"; // <-- This is input into your application by your user
            //OAuthAccessToken access = twitterService.GetAccessToken(requestToken, verifier);
            //OAuthAccessToken access = twitterService.GetAccessToken()

            // Step 4 - User authenticates using the Access Token
            twitterService.AuthenticateWith(accessConsumerKey,accessApiSecret,accessToken,accessTokenSecret);
            //twitterService.AuthenticateWith(access.Token, access.TokenSecret);
            var searchOption = new SearchOptions();
            searchOption.Q = "HARVEY";
            searchOption.Count = 100;
            searchOption.Geocode = new TwitterGeoLocationSearch(29.74894, -95.35619,50, TwitterGeoLocationSearch.RadiusType.Mi);
            
        
            var results = twitterService.Search(searchOption);
            Console.WriteLine(results.Statuses.Count());
            //twitterService.SearchForUser();

            TwitterRateLimitStatus rate = twitterService.Response.RateLimitStatus;
            Console.WriteLine("You have used " + rate.RemainingHits + " out of your " + rate.HourlyLimit);
            

           // TwitterService service = new TwitterService(accessConsumerKey, accessApiSecret);
           // service.AuthenticateWith("accessToken", "accessTokenSecret");

           // Prepare an OAuth Echo request to TwitPic
           //RestRequest request = service.PrepareEchoRequest();
           // request.Path = "uploadAndPost.xml";
           // request.AddFile("media", "failwhale", "failwhale.jpg", "image/jpeg");
           // request.AddField("key", "apiKey"); // <-- Sign up with TwitPic to get an API key
           // request.AddField("message", "Failwhale!");

           // Post photo to TwitPic with Hammock
           // RestClient client = new RestClient { Authority = "http://api.twitpic.com/", VersionPath = "2" };
           // RestResponse response = client.Request(request);
        }

    }
}
