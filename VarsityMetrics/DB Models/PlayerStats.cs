﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("PlayerStats")]
    public class PlayerStats
    {
        [PrimaryKey, AutoIncrement, Column("Pk")]
        public int Pk { get; set; }

        [Column("fname")]
        public String? Fname { get; set; }

        [Column("lname")]
        public String? Lname { get; set; }

        [Column("position")]
        public String? Position { get; set; }

        // Passing Stats
        [Column("pass_att")]
        public int? PassAtt{ get; set; }
        
        [Column("pass_comp")]

        public int? PassComp{ get; set; }
        [Column("passing_yards")]
        public int? PassingYards { get; set; }
        
        [Column("passing_tds")]
        public int? PassingTDs{ get; set; }

        [Column("interceptions")]
        public int? Interceptions{ get; set; }

        // Rushing Stats

        [Column("rush_att")]
        public int? RushAtt { get; set; }

        [Column("rushing_yards")]
        public int? RushingYards { get; set; }
        
        [Column("rushing_tds")]
        public int? RushingTDs { get; set; }

        [Column("fumbles")]
        public int? fumbles { get; set; }

        // Receiving Stats
        
        [Column("targets")]
        public int? Targets { get; set; }
        
        [Column("receptions")]
        public int? Receptions { get; set; }

        [Column("receiving_yards")]
        public int? ReceivingYards { get; set; }

        [Column("receiving_tds")]
        public int? ReceivingTDs { get; set; }
    }
}
