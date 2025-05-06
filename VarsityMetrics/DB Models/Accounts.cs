using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace VarsityMetrics.DB_Models
{
    [Table("Accounts")]
    public class Accounts : BaseModel
    {

        /*[PrimaryKey]
        public string id { get; set; }*/
        [PrimaryKey, Column("Email")]
        public String Email { get; set; }

        [Column("First Name")]
        public String FirstName { get; set; }

        [Column("Last Name")]
        public String LastName { get; set; }

        [Column("Role")]
        public Constants.Role Role { get; set; }

    }

    // TODO add any necessary security measures (salt, pepper, hashing)
}
