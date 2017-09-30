namespace Twitter.Crawler.Data
{
    public class TwitterCrawlerClassEntity
    {
        public TwitterCrawlerClassEntity(string text)
        {
            ClassText = text;
            IsHashTag = ClassText.StartsWith("#");
            IsAddress = ClassText.StartsWith("@");
        }
        public int Id { get; set; }
        public string ClassText { get; set; }
        public int InstanceCount { get; set; }
        public bool IsHashTag { get; set; }
        public bool IsAddress { get; set; }
    }
}