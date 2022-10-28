using System.ComponentModel.DataAnnotations;

namespace Houses.Infrastructure.Data.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Url]
        public string? PictureUrl { get; set; }
    }
}
