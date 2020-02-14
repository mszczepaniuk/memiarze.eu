using memiarzeEu.Data;
using memiarzeEu.Interfaces;
using memiarzeEu.Models;
using memiarzeEu.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Repositories
{
    // Testing EfRepository<T> without specifications is useless. I consider creating those tests as a training for me.
    public class MemeRepositoryWithoutSpecification
    {
        private ApplicationDbContext dbContext;
        private EfRepository<Meme> memeRepo;

        public MemeRepositoryWithoutSpecification()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDB1")
                .Options;
            dbContext = new ApplicationDbContext(dbOptions);
            memeRepo = new EfRepository<Meme>(dbContext);
            SeedData();
        }

        [Fact]
        public async Task GetById()
        {
            var mockMeme = dbContext.Memes.Find(1);

            var result = await memeRepo.GetByIdAsync(1);

            Assert.Equal(mockMeme, result);
        }

        [Fact]
        public async Task GetAll()
        {
            var expected = dbContext.Memes.ToList();
            
            var result = await memeRepo.GetAllAsync();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Count()
        {
            var expected = dbContext.Memes.Count();

            var result = await memeRepo.CountAsync(new BaseSpecification<Meme>());

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Add()
        {
            var mockMeme = new Meme
            {
                Title = "Add",
                ImagePath = "Add",
                UserId = "Add"
            };
            await memeRepo.AddAsync(mockMeme);

            var expected = dbContext.Memes.Where(x => x.Title == "Add").FirstOrDefault();

            Assert.Equal(expected, mockMeme);
        }

        [Fact]
        public async Task Update()
        {
            var mockMeme = dbContext.Memes.FirstOrDefault();
            mockMeme.Title = "Update";
            await memeRepo.UpdateAsync(mockMeme);

            var expected = dbContext.Memes.Where(x => x.Title == "Update").FirstOrDefault();

            Assert.Equal(expected, mockMeme);
        }

        [Fact]
        public async Task Delete()
        {
            var firstCount = dbContext.Memes.Count();
            var mockMeme = dbContext.Memes.Last();
            await memeRepo.DeleteAsync(mockMeme);
            var secondCount = dbContext.Memes.Count();

            Assert.Equal(firstCount, secondCount + 1);
        }

        private void SeedData()
        {
            var mockMemes = new List<Meme> {
                new Meme
                {
                Title = "Test1",
                ImagePath = "test1",
                UserId = "abc1"
                },
                new Meme
                {
                Title = "Test2",
                ImagePath = "test2",
                UserId = "abc2"
                },
                new Meme
                {
                Title = "Test3",
                ImagePath = "test3",
                UserId = "abc3"
                },
            };
            dbContext.AddRange(mockMemes);
            dbContext.SaveChanges();
        }
    }
}
