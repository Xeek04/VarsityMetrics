using SQLite;
namespace VarsityMetrics.DB_Models
{
    [Table("Game")]
    public class Game
    {

        [PrimaryKey, AutoIncrement, Column("Pk")]
        public int Pk { get; set; }

        [Column("opponent")]
        public String? Opponent { get; set; }

        [Column("date")]
        public String? Date { get; set; }

        [Column("our_score")]
        public int? OurScore { get; set; }

        [Column("their_score")]
        public int? TheirScore { get; set; }

        public string ScoreDisplay => OurScore.HasValue && TheirScore.HasValue ? $"{OurScore} - {TheirScore}" : "";
        public string BannerText => OurScore.HasValue && TheirScore.HasValue ? $"Our Team {OurScore} vs {TheirScore} {Opponent}" : "";

    }
}
