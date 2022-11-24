using Houses.Core.ViewModels.Property;

namespace Houses.Core.ViewModels.User
{
    public class UserServiceViewModel
    {
        public UserServiceViewModel()
        {
            Id = new Guid().ToString();
        }

        public string Id { get; set; }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? ProfilePicture { get; set; }

        public IEnumerable<PropertyServiceViewModel> Properties { get; set; } = new HashSet<PropertyServiceViewModel>();
    }
}
