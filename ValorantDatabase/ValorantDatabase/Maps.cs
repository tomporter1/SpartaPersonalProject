using System;
using System.Collections.Generic;

namespace ValorantDatabase
{
    public partial class Maps
    {
        public Maps()
        {
            GameLogs = new HashSet<GameLogs>();
        }

        public int MapId { get; set; }
        public string MapName { get; set; }

        public virtual ICollection<GameLogs> GameLogs { get; set; }
    }
}
