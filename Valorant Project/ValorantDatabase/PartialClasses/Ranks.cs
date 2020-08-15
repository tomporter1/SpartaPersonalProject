using System;

namespace ValorantDatabase
{
    public partial class Ranks : IEquatable<Ranks>
    {
        private int _rankID;
        private string _rankName;
        private string _imagePath;

        public override bool Equals(object obj)
        {
            return Equals(obj as Ranks);
        }

        public bool Equals(Ranks other)
        {
            return other != null &&
                   _rankID == other._rankID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_rankID);
        }

        public override string ToString() => _rankName;
    }
}