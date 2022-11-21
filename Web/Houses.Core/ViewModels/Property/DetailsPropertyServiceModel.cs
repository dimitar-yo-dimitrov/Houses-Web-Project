using Houses.Infrastructure.Data.Identity;

namespace Houses.Core.ViewModels.Property
{
    public class DetailsPropertyServiceModel : PropertyServiceViewModel
    {
        public string PropertyType { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;
    }
}
