﻿using Houses.Core.ViewModels.Post;
using Houses.Infrastructure.Data.Identity;

namespace Houses.Core.ViewModels.Property
{
    public class DetailsPropertyServiceModel
    {
        public string PropertyType { get; set; } = null!;

        public PropertyServiceViewModel? PropertyDto { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public IEnumerable<CreatePostInputViewModel>? Posts { get; set; }
    }
}
