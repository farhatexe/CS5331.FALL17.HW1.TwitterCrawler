namespace Twitter.Crawler.Model.Entities
{
    public class TwitterCoordinatesEntity
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool IsLocationAvailable { get; set; }
    }
}