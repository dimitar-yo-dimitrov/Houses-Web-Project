namespace Houses.Infrastructure.Constants
{
    public static class ValidationConstants
    {
        public static class User
        {
            public const int UserNameMinLength = 5;
            public const int UserNameMaxLength = 50;

            public const int UserFirstNameMinLength = 5;
            public const int UserFirstNameMaxLength = 50;

            public const int UserLastNameMinLength = 5;
            public const int UserLastNameMaxLength = 50;

            public const int EmailMinLength = 10;
            public const int EmailMaxLength = 60;

            public const int PasswordMaxLength = 6;
            public const int PasswordMinLength = 20;
        }

        public static class Property
        {
            public const int PropertyMinTitle = 5;
            public const int PropertyMaxTitle = 50;

            public const int OwnerMinLength = 5;
            public const int OwnerMaxLength = 50;

            public const int PropertyMinDescription = 20;
            public const int PropertyMaxDescription = 5000;

            public const int HomeMinAddress = 10;
            public const int HomeMaxAddress = 100;

            public const string PriceMinLength = "0.1";
            public const string PriceMaxLength = "1000000000";

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

        public class FormattingConstant
        {
            public const string NormalDateFormat = "dd.MM.yyyy";
        }
    }
}
