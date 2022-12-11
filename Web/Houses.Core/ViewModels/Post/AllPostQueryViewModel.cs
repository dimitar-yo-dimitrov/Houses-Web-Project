using System.ComponentModel.DataAnnotations;
using Houses.Core.ViewModels.Post.Enums;

namespace Houses.Core.ViewModels.Post
{
    public class AllPostQueryViewModel : PostServiceViewModel
    {
        public const int PostPerPage = 3;

        [Display(Name = "Search by text")]
        public string? SearchTerm { get; init; }

        // public string? PropertyId { get; init; }

        public PostSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPostCount { get; set; }
    }
}
