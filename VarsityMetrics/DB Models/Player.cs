using SQLite;
namespace VarsityMetrics.DB_Models
{
    [Table("Player")]
    public class Player
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        [Column("Pk")]
        public int Pk { get; set; }
        [Column("fname")]
        public String Fname { get; set; }
        [Column("lname")]
        public String Lname { get; set; }
        [Column("position")]
        public String Position { get; set; }
        [Column("running_yards")]
        public int RunningYards { get; set; }

    }
}
