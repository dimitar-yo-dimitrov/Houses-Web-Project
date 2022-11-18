using System.ComponentModel.DataAnnotations;
using static Houses.Common.GlobalConstants.ValidationConstants.Property;

namespace Houses.Infrastructure.Data.Entities
{
    public class PropertyType
    {
        public PropertyType()
        {
            Id = new Guid().ToString();
            Properties = new HashSet<Property>();
        }

        [Key]
        public string Id { get; set; }

        [MaxLength(PropertyMaxTitle)]
        public string Title { get; set; } = null!;

        public virtual ICollection<Property> Properties { get; set; }
    }
}
