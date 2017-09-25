using System.Collections.Generic;

namespace Twitter.Crawler.Model.Entities
{
    public class TwitterPlaceEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public string Country { get; set; }

        public string PlaceType { get; set; }

        public string Url { get; set; }

        public string FullName { get; set; }
        
        public List<TwitterCoordinatesEntity> Geometry { get; set; }

        //public List<string> PolyLines { get; set; }

        public TwitterPlaceEntity ContainedWithin { get; set; }
    }
}