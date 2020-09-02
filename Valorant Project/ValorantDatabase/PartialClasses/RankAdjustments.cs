using System;
using System.Collections.Generic;

namespace ValorantDatabase
{
    public partial class RankAdjustments : IEquatable<RankAdjustments>
    {
        private int _adjustmentID;
        private string _adjustmentName;
        private string _imagePath;

        public override bool Equals(object obj)
        {
            return Equals(obj as RankAdjustments);
        }

        public bool Equals(RankAdjustments other)
        {
            return other != null &&
                   _adjustmentID == other._adjustmentID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_adjustmentID);
        }

        public static bool operator ==(RankAdjustments left, RankAdjustments right)
        {
            return EqualityComparer<RankAdjustments>.Default.Equals(left, right);
        }

        public static bool operator !=(RankAdjustments left, RankAdjustments right)
        {
            return !(left == right);
        }

        public override string ToString() => _adjustmentName;
    }
}