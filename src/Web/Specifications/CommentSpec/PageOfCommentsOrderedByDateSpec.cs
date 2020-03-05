namespace memiarzeEu.Specifications.CommentSpec
{
    public class PageOfCommentsOrderedByDateSpec : PageOfCommentsSpec
    {
        public PageOfCommentsOrderedByDateSpec(int page) : base(page)
        {
            OrderByDescending = x => x.CreationDate;
        }
    }
}
