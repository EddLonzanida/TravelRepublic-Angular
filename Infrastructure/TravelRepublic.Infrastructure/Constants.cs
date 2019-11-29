namespace TravelRepublic.Infrastructure
{
    public static class Constants
    {
        /// <summary>
        /// Used in MEF DI, Swagger, Integration test, Unit tests
        /// </summary>
        public const string ApplicationId = "TravelRepublic";

        /// <summary>
        /// Default value is "Development". Set in Program.cs
        /// </summary>
        public static string CurrentEnvironment = "Development";
    }

    /// Database id for DbMigratorExportAttribute.
    public class DbNames
    {
        public const string TravelRepublic = "TravelRepublic";
    }
}
