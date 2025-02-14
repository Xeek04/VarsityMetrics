using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    public class DBAccess {

        string path;
        private SQLiteAsyncConnection conn;

        public async Task Init()
        {
            Trace.WriteLine($"DBAccess: Init() (file path: {path}");
            if (conn is not null)
            {
                return;
            }

            conn = new SQLiteAsyncConnection(path, Constants.Flags);
            // create all tables
            await conn.CreateTablesAsync<Game, Play, Player, Accounts>();
            await conn.CreateTablesAsync<Footage, Roster>(); // tables with foreign keys
        }

        public DBAccess(String databasePath)
        {
            Trace.WriteLine($"Database Constructor used with path {databasePath}");
            path = databasePath;
        }
         

        //private async Task Function<T>() where T : class, new() // use for new functions which return object T
        //{
        //  
        //}

        public async Task<bool> CheckLoginAsync(string username, string password)
        {
            await Init();

            var matches = await conn.Table<Accounts>().Where(a => (a.Username == username) & (a.Password == password)).ToListAsync(); // queries accounts table for records with username and password matching input
            return matches.Any(); // if the list is length 0 then return false else true
        }

        // returns true if there are no duplicates and a nonzero amount of records were inserted
        public async Task<bool> InsertAccountAsync(string username, string password, string email)
        {
            // returning an empty task is the async equivalent of void
            await Init();

            // TODO add validation
            var matches = await conn.Table<Accounts>().Where(a => (a.Username == username)).ToListAsync();

            if (matches.Any())
            {
                return false;
            }
            else
            {
                int addedRecords = await conn.InsertAsync(new Accounts { Username = username, Password = password, Email = email, Role = Constants.Role.Coach }); //insert record with identical person
                if (addedRecords != 0) { return true; } else { return false; }
            }
        }

        public async Task<bool> UploadPictureAsync(string path, string name)
        {
            await Init();

            int addedPlays = await conn.InsertAsync(new Play { PlayName = name, ImageSource =  path});
            if (addedPlays != 0) {return true;} else { return false; }
        }
    }
}
