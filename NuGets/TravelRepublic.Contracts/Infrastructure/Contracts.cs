namespace TravelRepublic.Contracts.Infrastructure
{
    public enum UserRoles
    {
        Users = 1,
        Admins = 4,
        UserManagers = 16
    }
    public enum Area
    {
        Admins = 2,
        Users = 8,
        UserManagers = 16,
        Registration = 32
    }

    public static class Contracts
    {
        public const string Master = "Master contact";

        public const string Standard = "Standard contract";
    }

    public static class Authorize
    {
        public const string Users = "Users";

        public const string Admins = "Admins";

        public const string UserManagers = "UserManagers";

    }
    
    public static class GetDuplicateActionNames
    {
        public const string Edit = "Edit";

        public const string Create = "Create";
    }
}
