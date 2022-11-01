using System.ComponentModel.DataAnnotations;

namespace Houses.Infrastructure.Data.Entities
{
    public class Image
    {
        public Image()
        {
            Id = new Guid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string? PictureUrl { get; set; }
    }
}
