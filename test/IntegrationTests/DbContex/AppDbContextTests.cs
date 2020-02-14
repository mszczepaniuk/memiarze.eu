using memiarzeEu.Data;
using memiarzeEu.Interfaces;
using memiarzeEu.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.DbContext
{
    public class AppDbContextTests
    {
        private ApplicationDbContext dbContext;

        public AppDbContextTests()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDB2")
                .Options;
            dbContext = new ApplicationDbContext(dbOptions);
            dbContext.Memes.AddRange(new Meme { Title = "test1", ImagePath = "test1", UserId = "test1" }, new Meme { Title = "test2", ImagePath = "test2", UserId = "test2" });
            dbContext.SaveChanges();
        }

        [Fact]
        public void AutomaticCreationDate()
        {
            var currentTime = DateTime.Now;

            var mockMeme = new Meme { Title = "mock1", ImagePath = "mock1", UserId = "mock1" };
            dbContext.Add(mockMeme);
            dbContext.SaveChanges();

            Assert.Equal(currentTime.Hour, mockMeme.CreationDate.Hour);
            Assert.Equal(currentTime.Minute, mockMeme.CreationDate.Minute);
        }

        [Fact]
        public void AutomaticUpdateDate()
        {
            var currentTime = DateTime.Now;

            var mockMeme = dbContext.Memes.Find(1);
            mockMeme.Title = "afterTest";
            dbContext.Update(mockMeme);
            dbContext.SaveChanges();

            Assert.Equal(currentTime.Hour, mockMeme.UpdateDate.Hour);
            Assert.Equal(currentTime.Minute, mockMeme.UpdateDate.Minute);
        }
    }
}
