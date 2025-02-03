using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    public class DBAccess {

        SQLiteAsyncConnection connection;

        public async Task Init()
        {
            if (connection is not null)
            {
                return;
            }

            connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await connection.CreateTablesAsync<Game, Opponent, Player>();
        }

        // From a silly youtube video that is probably wrong
        public SQLiteAsyncConnection Connect()
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            connection.QueryAsync(map)
        }

        //Change this function so that its useful, all the tables are made in init

        //private async Task MakeTable<T>() where T : class, new() // only for use in Connect()
        //{
        //  
        //}
    }
}
