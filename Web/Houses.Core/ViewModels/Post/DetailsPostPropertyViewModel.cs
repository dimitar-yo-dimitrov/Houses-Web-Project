using Houses.Core.ViewModels.Property;

namespace Houses.Core.ViewModels.Post
{
    public class DetailsPostPropertyViewModel : DetailsPropertyViewModel
    {
        public Infrastructure.Data.Entities.Property Property { get; set; } = null!;
    }
}
