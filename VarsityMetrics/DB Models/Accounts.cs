using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("Accounts")]
    public class Accounts : BaseModel
    {

        [PrimaryKey]
        public string id { get; set; }

        [Column("First Name")]
        public String FirstName { get; set; }

        [Column("Last Name")]
        public String LastName { get; set; }

        [Column("Role")]
        public String Role { get; set; }
    }

    // TODO add any necessary security measures (salt, pepper, hashing)
}
