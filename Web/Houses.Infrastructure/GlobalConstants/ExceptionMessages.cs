﻿namespace Houses.Infrastructure.GlobalConstants
{
    public static class ExceptionMessages
    {
        public const string PropertyNotFound = "Property with id {0} is not found.";

        public const string IdIsNull = "Id is not found.";

        public const string UserNotFound = "Unable to load user with ID '{0}'.";

        public const string InvalidOperation = "Invalid Operation!";

        public const string InvalidLogin = "Invalid login attempt!";

        public const string AddPropertyToCollectionNotFound = "This property is already in your collection.";
    }
}