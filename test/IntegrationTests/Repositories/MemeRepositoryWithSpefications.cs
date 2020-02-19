using memiarzeEu.Data;
using memiarzeEu.Interfaces;
using memiarzeEu.Models;
using memiarzeEu.Specifications;
using memiarzeEu.Specifications.MemeSpec;
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
    public class MemeRepositoryWithSpefications
    {
        private ApplicationDbContext dbContext;
        private EfRepository<Meme> memeRepo;

        public MemeRepositoryWithSpefications()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDB3")
                .Options;
            dbContext = new ApplicationDbContext(dbOptions);
            memeRepo = new EfRepository<Meme>(dbContext);
            SeedData();
        }

        ~MemeRepositoryWithSpefications()
        {
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async void PageOfMemesSpec_22Records_FirstPage10Results()
        {
            int page = 1;
            var result = await memeRepo.GetAsync(new PageOfMemesSpec(page));

            Assert.True(result.Count == 10);
        }

        [Fact]
        public async void PageOfMemesSpec_22Records_ThirdPage2Results()
        {
            int page = 3;
            var result = await memeRepo.GetAsync(new PageOfMemesSpec(page));

            Assert.True(result.Count == 2);
        }

        [Fact]
        public async void PageOfMemesOrderedByDateSpec_SecondRecordOlderThanFirst()
        {
            int page = 1;
            var memes = await memeRepo.GetAsync(new PageOfMemesOrderedByDateSpec(page));
            var result = memes.FirstOrDefault();

            Assert.True(result.UserId == "0");
        }

        private void SeedData()
        {
            var mockMemes = new List<Meme>();
            mockMemes.Add(new Meme
            {
                Title = "2000",
                UserId = "2000",
                ImagePath = "2000"
            });
            for (int i = 0; i < 20; i++)
            {
                mockMemes.Add(new Meme
                {
                    Title = i.ToString(),
                    UserId = i.ToString(),
                    ImagePath = i.ToString()
                });
            }
            dbContext.AddRange(mockMemes);
            dbContext.SaveChanges();
            dbContext.Memes.Add(new Meme
            {
                Title = "0",
                UserId = "0",
                ImagePath = "0"
            });
            dbContext.SaveChanges();
        }
    }
}
