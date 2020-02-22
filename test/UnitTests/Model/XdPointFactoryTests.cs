using memiarzeEu.Models;
using System;
using Xunit;

namespace UnitTests.Model
{
    public class XdPointFactoryTests
    {
        [Fact]
        public void Create_CommentXdPoint_GoodCommentId()
        {
            int commentId = 10;
            var mockFactory = new XdPointFactory<CommentXdPoint>();

            var result = mockFactory.Create("10", commentId) as CommentXdPoint;

            Assert.Equal(commentId, result.CommentId);
        }

        [Fact]
        public void Create_MemeXdPoint_GoodMemeId()
        {
            int memeId = 10;
            var mockFactory = new XdPointFactory<MemeXdPoint>();

            var result = mockFactory.Create("10", memeId) as MemeXdPoint;

            Assert.Equal(memeId, result.MemeId);
        }

        [Fact]
        public void Create_XdPoint_BadType()
        {
            int id = 10;
            var mockFactory = new XdPointFactory<XdPoint>();
            Exception initEx = null;

            try
            {
                mockFactory.Create("10", id);
            }
            catch (Exception ex)
            {
                initEx = ex;
            }

            Assert.True(initEx != null);
        }
    }
}
