namespace Twitter.Crawler.Interfaces
{
    public interface IAccessCredentials
    {
        string ConsumerKey { get; }

        string ConsumerSecret { get; }

        string AccessToken { get; }

        string AccessTokenSecret { get; }

        void Save(string fileName);

        IAccessCredentials Load(string fileName);
    }
}