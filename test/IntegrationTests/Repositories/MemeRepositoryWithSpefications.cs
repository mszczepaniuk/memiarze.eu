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
            if (!dbContext.Memes.Any())
            {
                SeedData();
            }
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
        public async void PageOfMemesUserTopSpec_FirstElementHas2Points()
        {
            int page = 1;
            var result = await memeRepo.CountAsync(new PageOfMemesUserTopSpec("2000", page));

            Assert.True(result == 2);
        }

        [Fact]
        public async void PageOfMemesUserTopSpec_SecondElementHas1Point()
        {
            int page = 1;
            var result = await memeRepo.GetAsync(new PageOfMemesUserTopSpec("2000", page));

            Assert.True(result[1].XdPoints.Count == 1);
        }

        [Fact]
        public async void ConcreteUserIdAndElementIdSpec_FindsOneResult()
        {
            var result = await memeRepo.GetAsync(new ConcreteUserIdAndElementIdSpec<Meme>("2000", 1));

            Assert.True(result.Count == 1);
        }

        [Fact]
        public async void ConcreteUserIdAndElementIdSpec_NullWhenBadInputs()
        {
            var result = await memeRepo.GetAsync(new ConcreteUserIdAndElementIdSpec<Meme>("2000", 5));

            Assert.True(!result.Any());
        }

        private void SeedData()
        {
            dbContext.Memes.Add(new Meme
            {
                Title = "2000",
                UserId = "2000",
                ImagePath = "2000",
                XdPoints = new List<MemeXdPoint> { new MemeXdPoint() }
            });
            var mockMemes = new List<Meme>();
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
            dbContext.Memes.Add(new Meme
            {
                Title = "0",
                UserId = "2000",
                ImagePath = "0",
                XdPoints = new List<MemeXdPoint> { new MemeXdPoint(), new MemeXdPoint() }
            });
            dbContext.SaveChanges();
        }
    }
}
