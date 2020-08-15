using System.Collections.Generic;

namespace ValorantDatabase
{
    public partial class Ranks
    {        
        public Ranks()
        {
            GameLogs = new HashSet<GameLogs>();
        }
        
        public int RankID
        {
            get => _rankID;
            set => _rankID = value;
        }
        public string RankName
        {
            get { return _rankName; }
            set { _rankName = value.Length <= 20 ? value : value.Substring(0, 20); }
        }
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value.Length <= 35 ? value : value.Substring(0, 35); }
        }

        public virtual ICollection<GameLogs> GameLogs { get; set; }
    }
}