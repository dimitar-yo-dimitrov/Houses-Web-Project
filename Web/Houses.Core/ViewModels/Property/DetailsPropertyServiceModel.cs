using Houses.Infrastructure.Data.Identity;

namespace Houses.Core.ViewModels.Property
{
    public class DetailsPropertyServiceModel : DetailsPropertyViewModel
    {
        public ApplicationUser? User { get; set; }

        public PropertyServiceViewModel? PropertyDto { get; set; }
    }
}
