using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    public class DBAccess {

        private SQLiteAsyncConnection conn;

        public async Task Init()
        {
            if (conn is not null)
            {
                return;
            }

            conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            // create all tables
            await conn.CreateTablesAsync<Game, Play, Player, Accounts>();
        }
         

        //private async Task Function<T>() where T : class, new() // use for new functions which return object T
        //{
        //  
        //}
    }
}
