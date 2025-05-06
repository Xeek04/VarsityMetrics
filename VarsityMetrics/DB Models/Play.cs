﻿using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("Play")]
    public class Play : BaseModel
    {
        [PrimaryKey("play_id")]
        public int play_id { get; set; }

        [Column("name")]
        public String name { get; set; }

        [Column("formation")]
        public String? formation { get; set; }

        [Column("type")]
        public String? type { get; set; }

        [Column("times_called")]
        public int times_called { get; set; }

        [Column("yards_gained")]
        public int[]? yards_gained { get; set; }

        [Column("uri")]
        public string? uri { get; set; }
        public void SetURI()
        {
            // TODO implement
        }
    }

    public class PlayGroup : List<Play>
    {
        public bool IsOffense;

        public PlayGroup(bool isOffense)
        {
            IsOffense = isOffense;
        }

        public string GroupName => IsOffense == true ? "Offense" : "Defense";
    }
    
}