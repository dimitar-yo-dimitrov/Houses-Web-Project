using Houses.Core.ViewModels.Post;

namespace Houses.Core.ViewModels.Property
{
    public class PropertyPostQueryViewModel
    {
        public PropertyServiceViewModel? PropertyDto { get; set; }

        public IEnumerable<CreatePostInputViewModel>? Posts { get; set; }
    }
}
