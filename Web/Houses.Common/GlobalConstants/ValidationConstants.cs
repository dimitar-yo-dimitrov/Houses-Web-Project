namespace Houses.Common.GlobalConstants
{
    public static class ValidationConstants
    {
        public static class User
        {
            public const int UserNameMinLength = 1;
            public const int UserNameMaxLength = 50;

            public const int UserFirstNameMinLength = 1;
            public const int UserFirstNameMaxLength = 50;

            public const int UserLastNameMinLength = 5;
            public const int UserLastNameMaxLength = 50;

            public const int EmailMinLength = 10;
            public const int EmailMaxLength = 60;

            public const int PasswordMaxLength = 6;
            public const int PasswordMinLength = 20;

            public const int PhoneNumberMaxLength = 15;

            public const string RegexPhoneNumber = @"^(\d{4})\-?(\d{3})\-?(\d{3})$";
            public const string RegexPhoneNumberError = "Phone Number is not corect!";
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

            public const int MaxUrl = 500;

            public const string PriceMinLength = "0.00";
            public const string PriceMaxLength = "1000000000.00";

            public const string RegexAddress = @"^[A-Za-z-. ]+,\s[A-Za-z-. ]+,\s[\d-]{1,4},\s[\d]{4,4}";
            public const string RegexAddressError = "Enter address in the format: City name, street name, number, post code";

            public const string SquareMetersMin = "1.00";
            public const string SquareMetersMax = "100000.00";
        }

        public static class City
        {
            public const int CityMinName = 2;
            public const int CityMaxName = 50;
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
        public static class AdminConstants
        {
            public const string AdministratorRoleName = "Admin";
            public const string OwnerRoleName = "Owner";
            public const string UserRoleName = "User";

            public const string AreaName = "Admin";
        }
    }
}
