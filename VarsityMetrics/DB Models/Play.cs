﻿using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        
    }
    public class PlayView
    {
        public Play Base { get; }
        public PlayView(Play play)
        {
            Base = play;
        }

        public string Name => Base.name;
        public string type => Base.type;
        public int TimesCalled => Base.times_called;
        public string ImageSource => Base.uri;

        public string YardsDisplay =>
            Base.yards_gained != null && Base.yards_gained.Length > 0
            ? $"Most recent yardage: {string.Join(", ", Base.yards_gained.Skip(Math.Max(0, Base.yards_gained.Length - 10))):F1}" :
            "";

        public string Average =>
            Base.yards_gained != null && Base.yards_gained.Length > 0
            ? $"Average: {Base.yards_gained.Average():F1} yds" :
            "";
    }

    public class PlayGroup : ObservableCollection<PlayView>
    {
        public string Type {  get; set; }

        public PlayGroup(string type, IEnumerable<PlayView> plays) : base(plays)
        {
            Type = type;
        }

    }
    
}