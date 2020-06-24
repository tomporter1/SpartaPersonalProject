using System;
using System.Collections.Generic;
using System.Text;

namespace ValorantDatabase
{
    public partial class GameModes
    {
        public GameModes()
        {
            GameLogs = new HashSet<GameLogs>();
        }

        public int ModeID
        {
            get { return _modeID; }
            set { _modeID = value; }
        }
        public string ModeName
        {
            get { return _modeName; }
            set { _modeName = value.Length <= 20 ? value : value.Substring(0, 20); }
        }
        public string ModeDiscription
        {
            get { return _modeDiscription; }
            set { _modeDiscription = value.Length <= 200 ? value : value.Substring(0, 200); }
        }
        public virtual ICollection<GameLogs> GameLogs { get; set; }
    }
}
