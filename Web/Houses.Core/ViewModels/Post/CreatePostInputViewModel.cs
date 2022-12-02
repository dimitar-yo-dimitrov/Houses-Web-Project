namespace Houses.Core.ViewModels.Post
{
    public class CreatePostInputViewModel
    {
        public CreatePostInputViewModel()
        {
            Id = new Guid().ToString();
        }

        public string Id { get; set; }

        public string AuthorName { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}
