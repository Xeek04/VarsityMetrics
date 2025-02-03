using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("Play")]
    public class Play
    {

        [PrimaryKey, AutoIncrement, Column("Pk")]
        public int Pk { get; set; }

        [Column("play_name")]
        public String? PlayName { get; set; }

        [Column("imgsrc")]
        public String? ImageSource { get; set; } // file location on computer
    }
}
