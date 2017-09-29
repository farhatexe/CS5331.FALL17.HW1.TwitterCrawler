using LinqToTwitter;

namespace Twitter.Crawler.Model.Entities
{
    public class TwitterCoordinatesEntity
    {

        public TwitterCoordinatesEntity()
        {
            
        }

        public TwitterCoordinatesEntity(Coordinate _linqToTwitterCoordinates)
        {
            if (_linqToTwitterCoordinates == null) return;
            Latitude = _linqToTwitterCoordinates.Latitude;
            Longitude = _linqToTwitterCoordinates.Longitude;
            IsLocationAvailable = _linqToTwitterCoordinates.IsLocationAvailable;
            Type = _linqToTwitterCoordinates.Type;
            
        }
        public int Id { get; set; }

        public string Type { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool IsLocationAvailable { get; set; }
    }
}