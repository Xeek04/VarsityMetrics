using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("Teams")]
    public class Teams : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("team_id")]
        public int Team_Id { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("role")]
        public string Role { get; set; }

    }

    // TODO add any necessary security measures (salt, pepper, hashing)
}
