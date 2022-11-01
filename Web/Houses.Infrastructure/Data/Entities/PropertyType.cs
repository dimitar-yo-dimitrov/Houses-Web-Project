using System.ComponentModel.DataAnnotations;
using static Houses.Infrastructure.Constants.ValidationConstants.Property;

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

        [MaxLength(PropertyMaxName)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Property> Properties { get; set; }
    }
}
