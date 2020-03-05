namespace memiarzeEu.Specifications.MemeSpec
{
    public class PageOfMemesOrderedByDateSpec : PageOfMemesSpec
    {
        public PageOfMemesOrderedByDateSpec(int page) : base(page)
        {
            OrderByDescending = x => x.CreationDate;
        }
    }
}
