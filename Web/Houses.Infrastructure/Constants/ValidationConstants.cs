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

        public static class Property
        {
            public const int PropertyMinName = 5;
            public const int PropertyMaxName = 50;

            public const int PropertyMinDescription = 20;
            public const int PropertyMaxDescription = 5000;

            public const int HomeMinAddress = 10;
            public const int HomeMaxAddress = 100;

            public const string RegexAddress = @"^[A-Za-z-. ]+,\s[A-Za-z-. ]+,\s[\d-]{1,4},\s[\d]{4,4}";
            public const string RegexAddressError = "Enter address in the format: City name, street name, number, post code";

            public const string FloorMin = "0";
            public const string FloorMax = "100";

            public const string SquareMetersMin = "1";
            public const string SquareMetersMax = "100000";
        }

        public static class City
        {
            public const int CityMinName = 2;
            public const int CityMaxName = 50;
        }

        public static class Neighborhood
        {
            public const int NeighborhoodMinName = 2;
            public const int NeighborhoodMaxName = 50;
        }

        public static class Comment
        {
            public const int MassageMin = 2;
            public const int MassageMax = 2000;
        }
    }
}
