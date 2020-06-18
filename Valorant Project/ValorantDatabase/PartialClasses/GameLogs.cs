using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public override string ToString()
        {
            using ValorantContext db = new ValorantContext();
            Maps map = db.GameLogs.Where(gl => gl.GameId == this.GameId).Include(gl => gl.Map).Select(gl => gl.Map).FirstOrDefault();
            Agents agent = db.GameLogs.Where(gl => gl.GameId == this.GameId).Include(gl => gl.Agent).Select(gl => gl.Agent).FirstOrDefault();

            return $"{(this.TeamScore > this.OpponentScore ? "Vicotry" : "Defeat")} on {map} as {agent} - Played on: {DateLogged}";
        }

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
