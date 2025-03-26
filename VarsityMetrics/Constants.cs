using Microsoft.Extensions.Logging;

namespace VarsityMetrics
{
    public static class Constants
    {
        public const string DBFilename = "Database.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public readonly static string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DBFilename);
        public readonly static string supabaseURL = "https://lyzcsopymcmkdixrccjh.supabase.co";
        public readonly static string supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imx5emNzb3B5bWNta2RpeHJjY2poIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTc0MjE1OTA0NiwiZXhwIjoyMDU3NzM1MDQ2fQ.W2e1Z4ypnLP8pO1J-xwpBdozu9IC7nLeKaqF0KlLUYo";

        // Create statements for database
        public const String CreateGame = $"CREATE TABLE \"Game\" (\r\n\t\"Pk\"\tINTEGER NOT NULL,\r\n\t\"opponent\"\tTEXT,\r\n\t\"date\"\tTEXT,\r\n\tPRIMARY KEY(\"Pk\" AUTOINCREMENT)\r\n)";
        public const String CreatePlayer = $"CREATE TABLE \"Player\" (\r\n\t\"Pk\"\tINTEGER NOT NULL,\r\n\t\"fname\"\tTEXT,\r\n\t\"lname\"\tTEXT,\r\n\t\"position\"\tTEXT,\r\n\t\"running_yards\"\tINTEGER,\r\n\tPRIMARY KEY(\"Pk\" AUTOINCREMENT)\r\n)";
        public const String CreatePlay = $"CREATE TABLE \"Play\" (\r\n\t\"Pk\"\tINTEGER NOT NULL,\r\n\t\"play_name\"\tTEXT,\r\n\t\"imgsrc\"\tTEXT,\r\n\tPRIMARY KEY(\"Pk\" AUTOINCREMENT)\r\n)";
        public const String CreateRoster = $"CREATE TABLE \"Roster\" (\r\n\t\"Pk\"\tINTEGER NOT NULL,\r\n\t\"game_id\"\tINTEGER,\r\n\t\"player_id\"\tINTEGER,\r\n\tPRIMARY KEY(\"Pk\" AUTOINCREMENT),\r\n\tFOREIGN KEY(\"game_id\") REFERENCES \"Game\"(\"Pk\"),\r\n\tFOREIGN KEY(\"player_id\") REFERENCES \"Player\"(\"Pk\")\r\n)";
        public const String CreateFootage = $"CREATE TABLE \"Footage\" (\r\n\t\"Pk\"\tINTEGER NOT NULL,\r\n\t\"url\"\tTEXT,\r\n\t\"game_id\"\tINTEGER,\r\n\t\"play_id\"\tINTEGER,\r\n\tPRIMARY KEY(\"Pk\" AUTOINCREMENT),\r\n\tFOREIGN KEY(\"game_id\") REFERENCES \"Game\"(\"Pk\"),\r\n\tFOREIGN KEY(\"play_id\") REFERENCES \"Play\"(\"Pk\")\r\n)";
        public const String CreatePlaterStats = $"CREATE TABLE \"PlayerStats\" (\r\n\t\"Pk\"\tINTEGER NOT NULL),\r\n\t\"fname\"\tTEXT,\r\n\t\"lname\"\tTEXT";
        //public const String Create = $"";

        //role enum
        public enum Role
        {
            // higher access levels should go at the top
            Coach,
            Statistician,
            Player
        }
    }
}