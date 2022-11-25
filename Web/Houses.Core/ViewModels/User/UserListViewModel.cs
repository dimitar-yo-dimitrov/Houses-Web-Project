namespace Houses.Core.ViewModels.User
{
    public class UserListViewModel
    {
        public UserListViewModel()
        {
            Id = new Guid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
