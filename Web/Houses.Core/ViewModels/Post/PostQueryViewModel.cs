namespace Houses.Core.ViewModels.Post
{
    public class PostQueryViewModel
    {
        public int TotalPostCount { get; set; }

        public IEnumerable<PostInputViewModel> Posts { get; set; }
            = new HashSet<PostInputViewModel>();
    }
}
