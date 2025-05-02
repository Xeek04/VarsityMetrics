using SQLite;

namespace VarsityMetrics.DB_Models;
[Table("MyTeam")]
public class MyTeam
{
  [Column("Pk")]
  public int Pk { get; set; }

  [Column("name")]
  public String? Name { get; set; }

  [Column("role")]
  public String? Role { get; set; }
}
    
