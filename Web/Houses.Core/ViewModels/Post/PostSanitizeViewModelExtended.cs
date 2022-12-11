using System.ComponentModel.DataAnnotations;

namespace Houses.Core.ViewModels.Post
{
    public class PostSanitizeViewModelExtended
    {
        public PostSanitizeViewModelExtended()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
    }
}
