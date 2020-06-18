using System;

namespace ValorantDatabase
{
    public partial class GameLogs
    {
        public int GameId { get; set; }
        public int MapId { get; set; }
        public int AgentId { get; set; }
        public int TeamScore { get; set; }
        public int OpponentScore { get; set; }
        public int? Kills { get; set; }
        public int? Deaths { get; set; }
        public int? Assits { get; set; }
        public int? Adr { get; set; }
        public DateTime? DateLogged { get; set; }

        public virtual Agents Agent { get; set; }
        public virtual Maps Map { get; set; }
    }
}
