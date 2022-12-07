namespace Houses.Core.ViewModels.Post
{
    public class PostServiceViewModel
    {
        public PostServiceViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Sender { get; set; } = null!;

        public string Date { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}
