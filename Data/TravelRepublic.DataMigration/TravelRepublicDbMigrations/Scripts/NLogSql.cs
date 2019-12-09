namespace TravelRepublic.DataMigration.TravelRepublicDbMigrations.Scripts
{
	public sealed class NLogSql
    {
        public static string GetCreateLogTable()
        {
            var createLogTableSql = @"
              SET ANSI_NULLS ON
              SET QUOTED_IDENTIFIER ON
              CREATE TABLE Logs(
	                Id int IDENTITY(1,1) NOT NULL,
	                Created DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	                Message NVARCHAR(MAX) NULL,
	                StackTrace NVARCHAR(MAX) NULL,
	                InnerException NVARCHAR(MAX) NULL,
	                LogLevel NVARCHAR(32) NULL,
	                MachineName NVARCHAR(200) NOT NULL,
	                CallSite NVARCHAR(512) NULL,
	                Type NVARCHAR(32) NULL,
	                AdditionalInfo NVARCHAR(MAX) NULL,
                CONSTRAINT [PK_dbo.Logs] PRIMARY KEY CLUSTERED (Id ASC)
                  WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
              ) ON [PRIMARY]";

            return createLogTableSql;
        }

        public static string GetInsertLogSp()
        {
            const string spInsertLog = @"
            CREATE PROCEDURE usp_Logs_Insert
                @machineName NVARCHAR(200),
                @logLevel NVARCHAR(32),
                @callSite NVARCHAR(512),
                @type NVARCHAR(32),
                @message NVARCHAR(MAX),
                @stackTrace NVARCHAR(MAX),
                @innerException NVARCHAR(MAX),
                @additionalInfo NVARCHAR(MAX)
            AS
            BEGIN
                SET NOCOUNT ON;
                INSERT INTO Logs(MachineName, LogLevel, CallSite, Type, Message, StackTrace, InnerException, AdditionalInfo) 
                VALUES (@machineName, @logLevel, @callSite, @type, @message, @stackTrace, @innerException, @additionalInfo);
            END";

            return spInsertLog;
        }

        public static string GetDropSp()
        {
            return "DROP PROCEDURE IF EXISTS usp_Logs_Insert;";
        }
    }
}
