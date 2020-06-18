using System;
using System.Collections.Generic;

namespace ValorantDatabase
{
    public partial class GameLogs : IEquatable<GameLogs>
    {
        public override bool Equals(object obj)
        {
            return Equals(obj as GameLogs);
        }

        public bool Equals(GameLogs other)
        {
            return other != null &&
                   GameId == other.GameId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GameId);
        }

        public override string ToString() => $"Game number {GameId}, played on: {DateLogged}";

        public static bool operator ==(GameLogs left, GameLogs right)
        {
            return EqualityComparer<GameLogs>.Default.Equals(left, right);
        }

        public static bool operator !=(GameLogs left, GameLogs right)
        {
            return !(left == right);
        }
    }
}
