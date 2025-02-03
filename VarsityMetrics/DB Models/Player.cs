using SQLite;
namespace VarsityMetrics.DB_Models
{
    [Table("Player")]
    public class Player
    {
        [PrimaryKey, AutoIncrement, Column("Pk")]
        public int? Pk { get; set; }
        [Column("fname")]
        public String? Fname { get; set; }
        [Column("lname")]
        public String? Lname { get; set; }
        [Column("position")]
        public String? Position { get; set; }
        [Column("rushing_yards")]
        public int? RushingYards { get; set; }
        //TODO implement stats tracked

        public Player(String? firstName, String? lastName, String? position, int? rushingYards)
        {
            Fname = firstName;
            Lname = lastName;
            Position = position;
            RushingYards = rushingYards;
        }
    }
}
