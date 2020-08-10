using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ValorantDatabase
{
    public partial class GameLogs : IEquatable<GameLogs>
    {
        private int _gameId;
        private int _mapId;
        private int _agentId;
        private int _modeId;
        private int _teamScore;
        private int _opponentScore;
        private int? _kills;
        private int? _deaths;
        private int? _assits;
        private float? _adr;
        private DateTime _dateLogged;
        private int? _season;

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

            return $"{(TeamScore > OpponentScore ? "Victory" : (TeamScore < OpponentScore ? "Defeat" : "Draw"))} on {map} as {agent} - Season: {Season}";
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
