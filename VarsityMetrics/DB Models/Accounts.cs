using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("Accounts")]
    public class Accounts
    {

        [PrimaryKey, AutoIncrement, Column("Pk")]
        public int Pk { get; set; }

        [NotNull, Column("username")]
        public String Username { get; set; }

        [NotNull, Column("password")]
        public String Password { get; set; }

        [NotNull, Column("role")]
        public Constants.Role Role { get; set; }
    }

    // TODO add any necessary security measures (salt, pepper, hashing)
}
