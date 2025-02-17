using Microsoft.Maui.ApplicationModel.Communication;
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

        public async Task<bool> InsertGameAsync(string opponent, int year, int month, int day, int? ourScore = null, int? theirScore = null) {
            
            await Init();
            // input validation
            // if date is invalid return false
            if ((1000 < year) || (year > 9999))
            {
                return false;
            }
            if ((1 > month) || (12 < month))
            {
                return false;
            }
            if ((1 > day) || (31 < day))
            {
                return false;
            }
            // if ourScore and theirScore are not both null or both ints return false
            if ((ourScore == null) ^ (theirScore == null))
            {
                return false;
            }

            string date = year + "-" + month + "-" + day;
            int addedRecords = await conn.InsertAsync(new Game { Opponent = opponent, Date = date, OurScore = ourScore, TheirScore = theirScore }); //insert record with identical person
            if (addedRecords != 0) { return true; } else { return false; }
        }

        public async Task<List<Game>> GetGamesAsync()
        {
            await Init();
            return await conn.Table<Game>().ToListAsync();
        }
    }
}
