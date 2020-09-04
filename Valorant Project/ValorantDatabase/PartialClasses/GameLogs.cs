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
        private int? _rankID;
        private int? _rankAdjustmentID;

        public double KD { get => _kills == null || _deaths == null ? 0 : Math.Round((float)_kills / (float)_deaths, 3); }

        public bool SeasonChecker(string season) => !int.TryParse(season, out int seasonNum) || Season == seasonNum;

        public string GameScore { get => $"{_teamScore}:{_opponentScore}"; }

        public string GameResult { get => _teamScore > _opponentScore ? "Win" : (_teamScore < _opponentScore ? "Loss" : "Draw"); }

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
            Ranks rank = db.GameLogs.Where(gl => gl.RankID == this.RankID).Include(gl => gl.Rank).Select(gl => gl.Rank).FirstOrDefault();

            //return $"{GameResult} on {map} as {agent} - Season: {Season}{(RankID == null ? "" : $" {rank}")}";
            return $"{GameScore} on {map} as {agent} - Season: {Season}";
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
