namespace Houses.Infrastructure.Constants
{
    public static class ValidationConstants
    {
        public static class User
        {
            public const int UserNameMinLength = 5;
            public const int UserNameMaxLength = 50;

            public const int UserFirstNameMaxLength = 50;
            public const int UserLastNameMaxLength = 50;

            public const int EmailMinLength = 10;
            public const int EmailMaxLength = 60;

            public const int PasswordMaxLength = 5;
            public const int PasswordMinLength = 20;
        }
    }
}
