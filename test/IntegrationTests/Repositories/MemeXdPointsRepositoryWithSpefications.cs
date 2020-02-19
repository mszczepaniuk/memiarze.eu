using memiarzeEu.Data;
using memiarzeEu.Interfaces;
using memiarzeEu.Models;
using memiarzeEu.Specifications;
using memiarzeEu.Specifications.MemeSpec;
using memiarzeEu.Specifications.XdPointSpec;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Repositories
{
    public class MemeXdPointsRepositoryWithSpefications
    {
        private ApplicationDbContext dbContext;
        private EfRepository<MemeXdPoint> memeXdPointRepo;

        public MemeXdPointsRepositoryWithSpefications()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDB4")
                .Options;
            dbContext = new ApplicationDbContext(dbOptions);
            memeXdPointRepo = new EfRepository<MemeXdPoint>(dbContext);
            SeedData();
        }

        ~MemeXdPointsRepositoryWithSpefications()
        {
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task ConcreteUserIdAndMemeIdSpec_UserId0MemeId0Exists()
        {
            var memes = await memeXdPointRepo.GetAsync(new MemeXdPointConcreteUserIdAndMemeIdSpec("0", 0));
            var meme = memes.FirstOrDefault();

            Assert.True(meme != null);
        }

        [Fact]
        public async Task ConcreteUserIdAndMemeIdSpec_UserId1MemeId0DoesntExist()
        {
            var memes = await memeXdPointRepo.GetAsync(new MemeXdPointConcreteUserIdAndMemeIdSpec("1", 0));
            var meme = memes.FirstOrDefault();

            Assert.True(meme == null);
        }

        private void SeedData()
        {
            var mockPoints = new List<MemeXdPoint>();
            for (int i = 0; i < 20; i++)
            {
                mockPoints.Add(new MemeXdPoint
                {
                    MemeId = i,
                    UserId = i.ToString()
                });
            }
            dbContext.AddRange(mockPoints);
            dbContext.SaveChanges();
        }
    }
}
