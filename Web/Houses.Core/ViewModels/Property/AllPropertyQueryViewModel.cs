using System.ComponentModel.DataAnnotations;
using Houses.Core.ViewModels.Property.Enums;

namespace Houses.Core.ViewModels.Property
{
    public class AllPropertyQueryViewModel
    {
        public const int HousesPerPage = 3;

        public string? PropertyType { get; init; }

        [Display(Name = "Search by text")]
        public string? SearchTerm { get; init; }

        public PropertySorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalHousesCount { get; set; }

        public IEnumerable<string> PropertyTypes { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<PropertyServiceViewModel> Properties { get; set; } = new HashSet<PropertyServiceViewModel>();
    }
}
