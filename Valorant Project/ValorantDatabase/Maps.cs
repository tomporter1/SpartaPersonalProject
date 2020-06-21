using System.Collections.Generic;

namespace ValorantDatabase
{
    public partial class Maps
    {

        public Maps()
        {
            GameLogs = new HashSet<GameLogs>();
        }

        public int MapId
        {
            get => _mapId;
            set => _mapId = value;
        }
        public string MapName
        {
            get { return _mapName; }
            set { _mapName = value.Length <= 20 ? value : value.Substring(0, 20); }
        }
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value.Length <= 35 ? value : value.Substring(0, 35); }
        }
        public string LayoutImagePath
        {
            get { return _layoutImagePath; }
            set { _layoutImagePath = value.Length <= 35 ? value : value.Substring(0, 35); }
        }

        public virtual ICollection<GameLogs> GameLogs { get; set; }
    }
}