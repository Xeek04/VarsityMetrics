using CommunityToolkit.Maui;
using Microsoft.Maui.ApplicationModel.Communication;
using SQLite;
using Supabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Collections.ObjectModel;

namespace VarsityMetrics.DB_Models
{
    public class DBAccess {

        
        public Supabase.Gotrue.User CurrentUser => client.Auth.CurrentUser;

        string path;
        private SQLiteAsyncConnection conn;
        private bool modified = false; //set this to true if the database tables have been modified. If you don't change it back then
        // the database will keep deleting itself on startup

        public static Client client;

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
            await conn.CreateTablesAsync<Game, Player, MyTeam>();
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

        // returns null if unsuccessful, otherwise returns user's role

        public async Task LogOutAsync()
        {
            await client.Auth.RefreshSession();
            await client.Auth.SignOut();
        }
        public async Task<Accounts> CheckLoginAsync(string email, string password)
        {
            await client.Auth.RefreshSession();
            try
            {
                var matches = await client.Auth.SignIn(email, password); // queries accounts table for records with username and password matching input
                if (matches == null)
                {
                    return null;
                }
                var currentUser = client.Auth.CurrentUser;

                var response = await client.From<Accounts>()
                    .Where(a => a.Email == currentUser.Email)
                    .Get();

                Accounts? account = response.Models.FirstOrDefault();
                if (account == null)
                {
                    return null;
                }
                else
                {
                    Trace.WriteLine($"DBAccess: Role is {account.Role}");
                    return account;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> ForgotPasswordEmail(string email)
        {
            await client.Auth.RefreshSession();

            try
            {
                var send = client.Auth.ResetPasswordForEmail(email);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> ResetPasswordToken(string Email, string token)
        {
            await client.Auth.RefreshSession();
            try
            {
                var session = await client.Auth.VerifyOTP(Email, token, Supabase.Gotrue.Constants.EmailOtpType.Recovery);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> ResetPassword(string password)
        {
            var att = new Supabase.Gotrue.UserAttributes { Password = password };
            await client.Auth.RefreshSession();

            try
            {
                var res = await client.Auth.Update(att);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            


        }

        // returns null if unsuccessful
        public async Task<Constants.Role?> GetCurrentUserRoleAsync()
        {
            await client.Auth.RefreshSession();
            var user = client.Auth.CurrentUser;
            if (user == null)
                return null;

            var result = await client
                .From<Accounts>()
                .Where(a => a.Email == user.Email)
                .Get();

            if (result.Models.Any())
            {
                return result.Models.FirstOrDefault().Role;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> InsertAccountAsync(string password, string email)
        {
            // returning an empty task is the async equivalent of void
            await client.Auth.RefreshSession();

            try
            {
                var signUp = await client.Auth.SignUp(email, password);
                //var signIn = await client.Auth.SignIn(Supabase.Gotrue.Constants.SignInType.Email, email);

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> ConfirmEmail(string Email, string token, string password, string FirstName, string LastName, Constants.Role role)
        {
            await client.Auth.RefreshSession();
            //var signIn = await client.Auth.SignIn(Supabase.Gotrue.Constants.SignInType.Email, email);
            try
            {
                var session = await client.Auth.VerifyOTP(Email, token, Supabase.Gotrue.Constants.EmailOtpType.Signup);
                var signIn = await client.Auth.SignIn(Email, password);

                if (session != null | signIn != null)
                {
                    var model = new Accounts
                    {
                        Email = Email,
                        FirstName = FirstName,
                        LastName = LastName,
                        Role = role
                    };

                    await client.From<Accounts>()
                        .Insert(model);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex); return false;
            }

        }

        public async Task<bool> UploadTeammateAsync(string name, Constants.Role role)
        {
            await client.Auth.RefreshSession();

            int addedPlayers = await conn.InsertAsync(new MyTeam { Name = name, Role = role });
            if (addedPlayers != 0) { return true; } else { return false; }
        }
        public async Task<bool> UploadPictureAsync(string path, string name, string type)
        {
            await client.Auth.RefreshSession();

            Play model = new Play
            {
                name = name,
                formation = null,
                type = type,
                times_called = 0,
                yards_gained = [],
                uri = null
            };

            await client.From<Play>().Insert(model);
            var fetch = await client.From<Play>().Where(x => x.name == name && x.type == type).Get();
            var update = fetch.Models.FirstOrDefault();

            var fileNaming = Path.Combine(type, update.play_id.ToString());

            await client.Storage.From("plays-images").Upload(path, fileNaming);

            var upload = client.Storage.From("plays-images").GetPublicUrl(fileNaming);


            update.uri = upload;

            await client.From<Play>().Update(update);


            return true;

        }

        public async Task<List<Roster>> GetRoster()
        {
            await client.Auth.RefreshSession();
            //return await conn.Table<Roster>().ToListAsync();
            var result = await client.From<Roster>().Get();
            return result.Models;
        }

        public async Task<List<Roster>> GetUnit(string unit)
        {
            await client.Auth.RefreshSession();
            List<string> searchedUnit = new List<string>();
            switch (unit)
            {
                case "offense":
                    searchedUnit = ["QB", "RB", "WR", "TE", "OL"];
                    break;
                case "defense":
                    searchedUnit = ["DE", "DT", "LB", "CB", "S"];
                    break;
                case "specialteams":
                    searchedUnit = ["K", "P"];
                    break;
                default:
                    searchedUnit = new List<string>();
                    break;
            }
            var result = await client.From<Roster>().Filter(x => x.Position, Supabase.Postgrest.Constants.Operator.In, searchedUnit).Get();
            return result.Models;
        }

        public async Task<List<Roster>> GetRosterByPosition(string position)
        {
            await client.Auth.RefreshSession();
            //List<Roster> result = await conn.Table<Roster>().Where(x => (x.Position == position)).ToListAsync();
            //return result.OrderBy(x => x.Number).ToList();
            var result = await client.From<Roster>().Where(x => x.Position == position).Order(x => x.Number, Supabase.Postgrest.Constants.Ordering.Ascending).Get();
            return result.Models;
        }

        public async Task<bool> AddPlayer(string firstName, string lastName, string position, string height, string weight, int number)
        {
            await client.Auth.RefreshSession();
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
            await client.Auth.RefreshSession();
            //await Init();
            //await conn.ExecuteAsync(("UPDATE PlayerStats SET passing_yards = null WHERE Fname = 'Joe' AND Lname = 'Burrow';"));
            PlayerStats current = await client.From<PlayerStats>().Where(x => x.Fname == stats.Fname && x.Lname == stats.Lname).Single();
            if (stats.PassAtt != null)
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
            if (stats.RushAtt != null)
            {
                await client.From<PlayerStats>()
                        .Where(x => x.Fname == stats.Fname && x.Lname == stats.Lname)
                        .Set(x => x.RushAtt, current.RushAtt + stats.RushAtt ?? stats.RushAtt)
                        .Set(x => x.RushYards, current.RushYards + stats.RushYards ?? stats.RushYards)
                        .Set(x => x.RushTDs, current.RushTDs + stats.RushTDs ?? stats.RushTDs)
                        .Set(x => x.Fumbles, current.Fumbles + stats.Fumbles ?? stats.Fumbles)
                        .Update();
            }
            if (stats.Targets != null)
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
            await client.Auth.RefreshSession();
            //return await conn.Table<PlayerStats>().Where(x => (x.Fname == fname && x.Lname == lname)).FirstAsync();
            PlayerStats result = await client.From<PlayerStats>().Where(x => x.Fname == fname && x.Lname == lname).Single();
            return result;
        }

        public async Task<List<string>> GetPlayerList()
        {
            await client.Auth.RefreshSession();
            //List<Roster> roster = await conn.Table<Roster>().ToListAsync();
            //roster.OrderBy(x => x.Lname).ToList();
            var query = await client.From<Roster>().Order(x => x.Lname, Supabase.Postgrest.Constants.Ordering.Descending).Get();
            List<Roster> roster = query.Models;
            List<string> result = new List<string>();
            foreach (Roster player in roster)
            {
                result.Add(player.Lname + ", " + player.Fname + " | " + player.Position);
            }
            return result;
        }

        public async Task<bool> ClearRoster()
        {
            await client.Auth.RefreshSession();
            //await Init();

            //await conn.DeleteAllAsync<Roster>();
            //await conn.DeleteAllAsync<PlayerStats>();
            await client.From<Roster>().Delete();
            await client.From<PlayerStats>().Delete();
            return true;
        }

        public async Task<List<MyTeam>> RequestTeammateAsync()
        {
            await client.Auth.RefreshSession();

            return await conn.Table<MyTeam>().ToListAsync();
        }
        public class Stats
        {
            public string ImageSource { get; set; }
            public string PlayName { get; set; }
            public int Times_Called { get; set; }
            public int[] Yards { get; set; }

            public string Yards_Gained => Yards != null ? string.Join(", ", Yards) : "";
        }

        public async Task<List<Play>> RequestPictureAsync(string type)
        {
            await client.Auth.RefreshSession();

            var plays = await client.Storage.From("plays-images").List(type);

            var offenseStats = await App.db.RequestPlayStatsAsync();

            List<string> urls = new List<string>();
            
            for (int i = 0; i < plays.Count; i++)
            {
                string imagePath = type + "/" + plays[i].Name;
                var upload = client.Storage.From("plays-images").GetPublicUrl(imagePath);
                urls.Add(upload + "|" + plays[i].Name);
            }


            List<Stats> items = urls.Select(p => 
            {
                var split = p.Split('|');
                return new Stats
                {
                    ImageSource = split[0],
                    PlayName = split[1]

                };
            }).ToList();

            var Playbook = new ObservableCollection<Stats>
                (items.Zip(offenseStats, (img, stat) => new Stats
                {
                    ImageSource = img.ImageSource,
                    PlayName = img.PlayName,
                    Times_Called = stat.times_called,
                    Yards = stat.yards_gained
                }));

            return null;*/
        }

        public async Task UpdatePlayAscync(Play play)
        {
            await client.Auth.RefreshSession();

            await client.From<Play>().Where(p => p.play_id == play.play_id).Set(p => p.yards_gained, play.yards_gained).Update();
            await client.From<Play>().Where(p => p.play_id == play.play_id).Set(p => p.times_called, play.times_called + 1).Update();
        }

        public async Task<List<Play>> RequestOrderedPictureAsync(string type)
        {
            /*await Init();

            var plays = await conn.Table<Play>().Where(p => p.PlayType == type).ToListAsync();
            return plays.OrderBy(p => p.PlayName)
                .Select(p => new Play
            {
                PlayName = p.PlayName,
                ImageSource = p.ImageSource,
            }).ToList();*/
            return null;
        }

        public async Task<bool> AddPlaybookStats(string name, string type, int yards)
        {
            await client.Auth.RefreshSession();
            var fetch = await client.From<Play>().Where(x => x.name == name && x.type == type).Get();

            var y = fetch.Models.FirstOrDefault();

            if (y != null)
            {
                Debug.WriteLine(y);
                y.times_called += 1;
                var yardsUpdated = y.yards_gained.ToList();
                yardsUpdated.Add(yards);
                y.yards_gained = yardsUpdated.ToArray();

                var yardsUpdate = await client.From<Play>().Update(y);
                

                return true;
            }

            Debug.WriteLine("Failed");
            return false;

        }

        public async Task<List<Play>> RequestPlayStatsAsync()
        {
            await client.Auth.RefreshSession();

            var stats = await client.From<Play>().Select(p => new object[] {p.times_called, p.yards_gained}).Get();
            return stats.Models;
        }

        public async Task<List<Play>> GetPlays()
        {
            await client.Auth.RefreshSession();
            var playList = await client.From<Play>().Where(p => p.type == "Offense").Get();
            return playList.Models;
        }

        public async Task<bool> UpdatePlayYardage(int play, int yards)
        {
            await client.Auth.RefreshSession();
            Play curr = await client.From<Play>().Where(p => p.play_id == play).Single();
            int[] currYards = new int[curr.yards_gained.Count()+1];

            currYards[currYards.Count() - 1] = yards;

            await client.From<Play>().Where(p => p.play_id == play)
                                        .Set(p => p.yards_gained, currYards)
                                        .Set(p => p.times_called, currYards.Count())
                                        .Update();
            return true;
        }
        
        public async Task<bool> InsertGameAsync(string opponent, int year, int month, int day, int? ourScore = null, int? theirScore = null)
        {
            await client.Auth.RefreshSession();
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
            await client.Auth.RefreshSession();
            return await conn.Table<Game>().ToListAsync();
        }

        public async Task<Footage> GetFootageByGameIdAsync(int gameId)
        {
            Trace.WriteLine($"DBAccess: Fetching footage for game {gameId}");
            await client.Auth.RefreshSession();

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
            await client.Auth.RefreshSession();

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

        // Gamelog and Schedule
        public async Task<List<Gamelog>> GetSchedule()
        {
            await client.Auth.RefreshSession();
            var schedule = await client.From<Gamelog>().Order(g => g.Date, Supabase.Postgrest.Constants.Ordering.Ascending).Get();
            return schedule.Models;
        }        
        
        public async Task<List<GameStats>> GetScheduleStats()
        {
            await client.Auth.RefreshSession();
            var schedule = await client.From<GameStats>().Get();
            return schedule.Models;
        }

        public async Task<bool> AddGame(string opponent, DateTime date, bool home, string location)
        {
            await client.Auth.RefreshSession();
            Gamelog newGame = new Gamelog
            {
                ForTeam = "Ruston High",
                OppTeam = opponent,
                Date = date,
                HomeGame = home,
                Location = location
            };

            await client.From<Gamelog>().Insert(newGame);
            var id = await client.From<Gamelog>().Get();

            GameStats newStats = new GameStats
            {
                GameId = id.Models.LastOrDefault().GameId
            };
            await client.From<GameStats>().Insert(newStats);
            return true;
        }

        public async Task<List<PlayByPlay>> GetPlayByPlay(int opp, int quarter)
        {
            await client.Auth.RefreshSession();
            var plays = await client.From<PlayByPlay>().Where(p => p.GameId == opp).Where(p => p.Quarter == quarter).Get();
            return plays.Models;
        }

        public async Task<bool> AddPlayToGame(PlayByPlay play)
        {
            await client.Auth.RefreshSession();
            await client.From<PlayByPlay>().Insert(play);
            return true;
        }
    }
}