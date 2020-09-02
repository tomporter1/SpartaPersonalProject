using System.Collections.Generic;

namespace ValorantDatabase
{
    public partial class RankAdjustments
    {
        public RankAdjustments()
        {
            GameLogs = new HashSet<GameLogs>();
        }

        public int AdjustmentID
        {
            get => _adjustmentID;
            set => _adjustmentID = value;
        }
        public string AdjustmentName
        {
            get => _adjustmentName;
            set => _adjustmentName = value.Length <= 20 ? value : value.Substring(0, 20);
        }
        public string ImagePath
        {
            get => _imagePath;
            set => _imagePath = value.Length <= 35 ? value : value.Substring(0, 35);
        }

        public virtual ICollection<GameLogs> GameLogs { get; set; }
    }
}