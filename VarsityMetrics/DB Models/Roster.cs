using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("Roster")]
    public class Roster
    {

        [PrimaryKey, AutoIncrement, Column("Pk")]
        public int Pk { get; set; }

        [Column("fname")]
        public String? Fname { get; set; }

        [Column("lname")]
        public String? Lname { get; set; }

        [Column("position")]
        public String? Position { get; set; }

        [Column("height")]
        public String? Height { get; set; }

        [Column("weight")]
        public String? Weight { get; set; }

        [Column("number")]
        public int? Number { get; set; }

        [Column("depth")]
        public int Depth { get; set; } = 0;

        [Column("ir_status")]
        public bool IRStatus { get; set; } = false;

        [Column("injury")]
        public String? Injury { get; set; } = null;
    }
}
