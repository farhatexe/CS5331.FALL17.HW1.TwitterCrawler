using LinqToTwitter;
using Twitter.Crawler.Interfaces;

namespace Twitter.Crawler.Access
{
    public static class LinqToTwitterAuthorizer
    {
        public static IAuthorizer DoSingleUserAuth(IAccessCredentials accessCredential)
        {

            var auth = new SingleUserAuthorizer

            {

                CredentialStore = new SingleUserInMemoryCredentialStore

                {

                    ConsumerKey = accessCredential.ConsumerKey,

                    ConsumerSecret = accessCredential.ConsumerSecret,

                    AccessToken = accessCredential.AccessToken,

                    AccessTokenSecret = accessCredential.AccessTokenSecret

                }

            };



            return auth;

        }
    }
}