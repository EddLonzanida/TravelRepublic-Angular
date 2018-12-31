namespace TravelRepublic.Contracts.Infrastructure
    {
        public enum eUserRoles
        {
            Users = 1,
            Admins = 4,
            UserManagers = 16
        }
    
        public static class Authorize
        {
            public const string Users = "Users";
    
            public const string Admins = "Admins";
    
            public const string UserManagers = "UserManagers";
        }
        
        public static class DuplicateNameAction
        {
            public const string Edit = "Edit";
        
            public const string Create = "Create";
        }
    }
