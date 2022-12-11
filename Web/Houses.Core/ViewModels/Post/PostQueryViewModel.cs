using Houses.Core.ViewModels.Property;

namespace Houses.Core.ViewModels.Post
{
    public class PostQueryViewModel : DetailsPropertyServiceModel
    {
        public int TotalPostCount { get; set; }

        public IEnumerable<PostServiceViewModel> Posts { get; set; }
            = new HashSet<PostServiceViewModel>();
    }
}
