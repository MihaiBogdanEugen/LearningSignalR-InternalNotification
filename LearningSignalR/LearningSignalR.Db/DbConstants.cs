using LearningSignalR.Db.Models.Identity;

namespace LearningSignalR.Db
{
    public static class DbConstants
    {
        public const int StringLength128 = 128;
        public const int StringLength = 256;
        public const int StringLength512 = 512;
        public const int BigStringLength = 2048;
        public const int StringLength4096 = 4096;
        public const byte DecimalPrecision = 18;
        public const byte DecimalScale = 4;
        public const byte GeographyDecimalPrecision = 18;
        public const byte GeographyDecimalScale = 15;

        public static class DefaultAdministratorUser
        {
            public const string FirstName = "Bogdan";
            public const string LastName = "Mihai";
            public const string Email = "mbe1224@gmail.com";
            public const string PhoneNo = "0721601985";
            public const string Password = "danila8C@";
        }

        public static class RoleNames
        {
            public const string Administratives = RoleNames.Administrator + "," + RoleNames.PowerUser;
            public const string Administrator = "Administrator";
            public const string PowerUser = "PowerUser";
            public const string User = "User";
            public const string Client = "Client";
        }

        public static class Roles
        {
            public static Role Administrator => new Role { Id = 1, Name = RoleNames.Administrator };
            public static Role PowerUser => new Role { Id = 2, Name = RoleNames.PowerUser };
            public static Role User => new Role { Id = 3, Name = RoleNames.User };
            public static Role Client => new Role { Id = 4, Name = RoleNames.Client };

            public static Role[] All => new[] { Roles.Administrator, Roles.PowerUser, Roles.User, Roles.Client };
        }
    }
}