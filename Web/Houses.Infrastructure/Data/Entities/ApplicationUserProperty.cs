﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Houses.Infrastructure.Data.Identity;

namespace Houses.Infrastructure.Data.Entities
{
    public class ApplicationUserProperty
    {
        [Key]
        public string ApplicationUserId { get; set; } = null!;

        [Key]
        public string PropertyId { get; set; } = null!;

        [ForeignKey(nameof(ApplicationUserId))]
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; } = null!;
    }
}
