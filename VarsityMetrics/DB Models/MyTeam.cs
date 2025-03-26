using SQLite;

namespace VarsityMetrics.DB_Models;
[Table("MyTeam")]
public class MyTeam
{
  [PrimaryKey, AutoIncrement, Column("Pk")]
  public int Pk { get; set; }

  [NotNull, Column("name")]
  public String? Name { get; set; }

  [NotNull, Column("role")]
  public String? Role { get; set; }
}
    
