using Houses.Infrastructure.Data.Identity;

namespace Houses.Core.ViewModels.Property
{
    public class DetailsPropertyServiceModel : DetailsPropertyViewModel
    {
        public string? PropertyType { get; set; } = null!;

        public ApplicationUser? User { get; set; } = null!;

        public PropertyServiceViewModel? PropertyDto { get; set; }
    }
}
