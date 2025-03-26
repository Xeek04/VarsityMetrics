using Microsoft.Maui.ApplicationModel.Communication;
using SQLite;
using Supabase;
using Supabase.Postgrest.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    public class DBAccess {

        string path;
        private SQLiteAsyncConnection conn;
        private bool modified = false; //set this to true if the database tables have been modified. If you don't change it back then
        // the database will keep deleting itself on startup

        private Client client;

        public async Task Init()
        {
            if (modified)
            {
                File.Delete(path);
                modified = false;
            }
            Trace.WriteLine($"DBAccess: Init() (file path: {path}");
            if (conn is not null)
            {
                return;
            }

            conn = new SQLiteAsyncConnection(path, Constants.Flags);
            // create all tables
            await conn.CreateTablesAsync<Game, Play, Player, Accounts, Footage>();
            //await conn.CreateTablesAsync<Footage, Roster, PlayerStats>(); // tables with foreign keys
        }

        public DBAccess(String databasePath)
        {
            var options = new SupabaseOptions
            {
                AutoConnectRealtime = true
            };
            client = new Client(Constants.supabaseURL, Constants.supabaseKey, options);

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
        public async Task<bool> UploadTeammateAsync(string name, string role)
        {
            await Init();

            int addedPlayers = await conn.InsertAsync(new MyTeam { Name = name, Role = role });
            if (addedPlayers != 0) {return true;} else { return false;}
        }
        public async Task<bool> UploadPictureAsync(string path, string name, string type)
        {
            await Init();

            int addedPlays = await conn.InsertAsync(new Play { PlayName = name, PlayType = type, ImageSource =  path});
            if (addedPlays != 0) {return true;} else { return false; }
        }

        public async Task<List<Roster>> GetRoster()
        {
            //return await conn.Table<Roster>().ToListAsync();
            var result = await client.From<Roster>().Get();
            return result.Models;
        }

        public async Task<List<Roster>> GetRosterByPosition(string position)
        {
            //List<Roster> result = await conn.Table<Roster>().Where(x => (x.Position == position)).ToListAsync();
            //return result.OrderBy(x => x.Number).ToList();
            var result = await client.From<Roster>().Where(x => x.Position == position).Get();
            return result.Models;
        }

        public async Task<bool> AddPlayer(string firstName, string lastName, string position, string height, string weight, int number)
        {
            //await Init();

            //await conn.InsertAsync(new Roster { Fname = firstName, Lname = lastName, Position = position, Height = height, Weight = weight, Number = number });
            //await conn.InsertAsync(new PlayerStats { Fname = firstName, Lname = lastName, Position = position });
            Roster rmodel = new Roster
            {
                Fname = firstName,
                Lname = lastName,
                Position = position,
                Height = height,
                Weight = weight,
                Number = number,
            };
            await client.From<Roster>().Insert(rmodel);
            var current = await client.From<Roster>().Where(x => x.Fname == firstName && x.Lname == lastName).Single();
            PlayerStats smodel = new PlayerStats
            {
                Fname = firstName,
                Lname = lastName,
                Position = position
            };
            await client.From<PlayerStats>().Insert(smodel);
            return true;
        }

        public async Task<bool> AddPlayerStats(PlayerStats stats)
        {
            //await Init();
            //await conn.ExecuteAsync(("UPDATE PlayerStats SET passing_yards = null WHERE Fname = 'Joe' AND Lname = 'Burrow';"));
            PlayerStats current = await client.From<PlayerStats>().Where(x => x.Fname == stats.Fname && x.Lname == stats.Lname).Single();
            if(stats.PassAtt != null)
            {
                await client.From<PlayerStats>()
                        .Where(x => x.Fname == stats.Fname && x.Lname == stats.Lname)
                        .Set(x => x.PassAtt, current.PassAtt + stats.PassAtt ?? stats.PassAtt)
                        .Set(x => x.PassComp, current.PassComp + stats.PassComp ?? stats.PassComp)
                        .Set(x => x.PassYards, current.PassYards + stats.PassYards ?? stats.PassYards)
                        .Set(x => x.PassTDs, current.PassTDs + stats.PassTDs ?? stats.PassTDs)
                        .Set(x => x.Interceptions, current.Interceptions + stats.Interceptions ?? stats.Interceptions)
                        .Update();
            }            
            if(stats.RushAtt != null)
            {
                await client.From<PlayerStats>()
                        .Where(x => x.Fname == stats.Fname && x.Lname == stats.Lname)
                        .Set(x => x.RushAtt, current.RushAtt + stats.RushAtt ?? stats.RushAtt)
                        .Set(x => x.RushYards, current.RushYards + stats.RushYards ?? stats.RushYards)
                        .Set(x => x.RushTDs, current.RushTDs + stats.RushTDs ?? stats.RushTDs)
                        .Set(x => x.Fumbles, current.Fumbles + stats.Fumbles ?? stats.Fumbles)
                        .Update();
            }            
            if(stats.Targets != null)
            {
                await client.From<PlayerStats>()
                        .Where(x => x.Fname == stats.Fname && x.Lname == stats.Lname)
                        .Set(x => x.Targets, current.Targets + stats.Targets ?? stats.Targets)
                        .Set(x => x.Receptions, current.Receptions + stats.Receptions ?? stats.Receptions)
                        .Set(x => x.RecYards, current.RecYards + stats.RecYards ?? stats.RecYards)
                        .Set(x => x.RecTDs, current.RecTDs + stats.RecTDs ?? stats.RecTDs)
                        .Update();
            }
            return true;
        }

        public async Task<PlayerStats> StatQuery(string fname, string lname)
        {
            //return await conn.Table<PlayerStats>().Where(x => (x.Fname == fname && x.Lname == lname)).FirstAsync();
            PlayerStats result = await client.From<PlayerStats>().Where(x => x.Fname == fname && x.Lname == lname).Single();
            return result;
        }

        public async Task<List<string>> GetPlayerList()
        {
            //List<Roster> roster = await conn.Table<Roster>().ToListAsync();
            //roster.OrderBy(x => x.Lname).ToList();
            var query = await client.From<Roster>().Order(x => x.Lname, Supabase.Postgrest.Constants.Ordering.Descending).Get();
            List<Roster> roster = query.Models;
            List<string> result = new List<string>();
            foreach(Roster player in roster)
            {
                result.Add(player.Lname + ", " + player.Fname + " | " + player.Position);
            }
            return result;
        }

        public async Task<bool> ClearRoster()
        {
            //await Init();

            //await conn.DeleteAllAsync<Roster>();
            //await conn.DeleteAllAsync<PlayerStats>();
            await client.From<Roster>().Delete();
            await client.From<PlayerStats>().Delete();
            return true;
        }

        public async Task<List<MyTeam>> RequestTeammateAsync()
        {
            await Init();

            return await conn.Table<MyTeam>().ToListAsync();
        }
       
       
        public async Task<List<Play>> RequestPictureAsync(string type)
        {
            await Init();

            var plays = await conn.Table<Play>().Where(p => p.PlayType == type).ToListAsync();
            return plays.Select(p => new Play
            {
                PlayName = p.PlayName,
                ImageSource = p.ImageSource,
            }).ToList();

        }

        public async Task<List<Play>> RequestOrderedPictureAsync(string type)
        {
            await Init();

            var plays = await conn.Table<Play>().Where(p => p.PlayType == type).ToListAsync();
            return plays.OrderBy(p => p.PlayName)
                .Select(p => new Play
            {
                PlayName = p.PlayName,
                ImageSource = p.ImageSource,
            }).ToList();
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

        public async Task<Footage> GetFootageByGameIdAsync(int gameId)
        {
            Trace.WriteLine($"DBAccess: Fetching footage for game {gameId}");
            await Init();

            var footage = await conn.Table<Footage>().FirstOrDefaultAsync(f => f.GameId == gameId);

            if (footage == null)
            {
                Trace.WriteLine("DBAccess: No footage found.");
                return new Footage();
            }

            Trace.WriteLine("DBAccess: Footage found.");
            return footage;
        }

        public async Task<bool> UploadVideoAsync(int gameId, string video)
        {
            await Init();

            var footage = await conn.Table<Footage>().FirstOrDefaultAsync(f => f.GameId == gameId);

            if (footage == null)
            {
                Trace.WriteLine("DBAccess: No footage found. Inserting...");
                int addedRecords = await conn.InsertAsync(new Footage { Uri = video, GameId = gameId, PlayId = null}); //insert record
                if (addedRecords != 0) { return true; } else { return false; }
            }
            else
            {
                Trace.WriteLine("DBAccess: Footage found. Updating...");
                footage.Uri = video;
                int addedRecords = await conn.UpdateAsync(footage); //insert record
                if (addedRecords != 0) { return true; } else { return false; }
            }
        }
    }
}
