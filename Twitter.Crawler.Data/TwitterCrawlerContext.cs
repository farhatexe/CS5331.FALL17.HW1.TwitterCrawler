using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Twitter.Crawler.Model.Entities;

namespace Twitter.Crawler.Data
{
    public class TwitterCrawlerContext:DbContext
    {
        public DbSet< TwitterCoordinatesEntity> TwitterCoordinatesEntities { get; set; }
        public DbSet< TwitterPlaceEntity> TwitterPlaceEntities { get; set; }
        public DbSet< TwitterStatusEntity> TwitterStatusEntities { get; set; }
        public DbSet< TwitterUserEntity> TwitterUserEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=10.0.75.1,1433;Persist Security Info=True;User ID=SA;Password=P@ssword1");
        }

    }
}
