using System;
using System.Collections.Generic;
using System.Text;

namespace ValorantDatabase
{
    public partial class GameModes : IEquatable<GameModes>
    {
        private int _modeID;
        private string _modeName;
        private string _modeDiscription;

        public override bool Equals(object obj)
        {
            return Equals(obj as GameModes);
        }

        public bool Equals(GameModes other)
        {
            return other != null &&
                   _modeID == other._modeID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_modeID);
        }

        public static bool operator ==(GameModes left, GameModes right)
        {
            return EqualityComparer<GameModes>.Default.Equals(left, right);
        }

        public static bool operator !=(GameModes left, GameModes right)
        {
            return !(left == right);
        }
    }
}
