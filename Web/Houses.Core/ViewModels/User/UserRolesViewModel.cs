﻿namespace Houses.Core.ViewModels.User
{
    public class UserRolesViewModel
    {
        public UserRolesViewModel()
        {
            UserId = new Guid().ToString();
        }

        public string UserId { get; set; }

        public string Name { get; set; } = null!;

        public string[] RoleNames { get; set; } = null!;
    }
}
