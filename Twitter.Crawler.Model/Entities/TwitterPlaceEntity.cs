using System.Collections.Generic;
using LinqToTwitter;
namespace Twitter.Crawler.Model.Entities
{
    public class TwitterPlaceEntity
    {
        public TwitterPlaceEntity(Place linqToTwitterPlace)
        {
            if(linqToTwitterPlace==null) return;
            
            Name = linqToTwitterPlace.Name;
            FullName = linqToTwitterPlace.FullName;
            Url = linqToTwitterPlace.Url;
            PlaceType = linqToTwitterPlace.PlaceType;
            Country = linqToTwitterPlace.Country;
            CountryCode = linqToTwitterPlace.CountryCode;
            ContainedWithin = new TwitterPlaceEntity(linqToTwitterPlace.ContainedWithin);
            Geometry = new List<TwitterCoordinatesEntity>();
            foreach (var coordinate in linqToTwitterPlace.Geometry.Coordinates)
            {
                Geometry.Add(new TwitterCoordinatesEntity(coordinate));
            }
        }
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