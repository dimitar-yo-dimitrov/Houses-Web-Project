namespace Houses.Core.ViewModels.Post
{
    public class PostQueryViewModel
    {
        public int TotalPostCount { get; set; }

        public IEnumerable<PostServiceViewModel> Posts { get; set; }
            = new HashSet<PostServiceViewModel>();
    }
}
