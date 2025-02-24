using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("Footage")]
    public class Footage
    {

        [PrimaryKey, AutoIncrement, Column("Pk")]
        public int Pk { get; set; }

        [Column("url")]
        public String? YtId { get; set; }

        [Indexed, Column("game_id")]
        public int? GameId { get; set; }

        [Indexed, Column("play_id")]
        public int? Date { get; set; }

        string? HtmlString => string.IsNullOrEmpty(YtId) ? "" : $@"
    <html>
    <body style='margin:0;padding:0;'>
        <iframe width='100%' height='100%' 
                src='https://www.youtube.com/embed/{YtId}?autoplay=1&controls=1'
                frameborder='0' allowfullscreen>
        </iframe>
    </body>
    </html>";
    }

}
