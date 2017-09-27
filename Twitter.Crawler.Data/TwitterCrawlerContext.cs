using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Crawler.Model.Entities;

namespace Twitter.Crawler.Data
{
    public class TwitterCrawlerContext:DbContext
    {
        public DbSet< TwitterCoordinatesEntity> TwitterCoordinatesEntities { get; set; }
        public DbSet< TwitterPlaceEntity> TwitterPlaceEntities { get; set; }
        public DbSet< TwitterStatusEntity> TwitterStatusEntities { get; set; }
        public DbSet< TwitterUserEntity> TwitterUserEntities { get; set; }



    }
}
