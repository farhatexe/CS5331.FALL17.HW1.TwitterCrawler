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
        //public DbSet< TwitterCoordinatesEntity> TwitterCoordinatesEntities { get; set; }
        //public DbSet< TwitterPlaceEntity> TwitterPlaceEntities { get; set; }
        public DbSet<TwitterCrawlerClassEntity> TwitterCrawlerClasses { get; set; }
        public DbSet< TwitterStatusEntity> TwitterStatusEntities { get; set; }
        //public DbSet< TwitterUserEntity> TwitterUserEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));
            //modelBuilder.Properties<decimal>()
            //    .Configure(c => c.HasColumnType("DECIMAL(20,0)"));
            modelBuilder.Entity<TwitterStatusEntity>().Property(x => x.StatusId).HasPrecision(20, 0);
            base.OnModelCreating(modelBuilder);
        }
    }
}
