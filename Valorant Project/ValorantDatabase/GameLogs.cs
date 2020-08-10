using System;

namespace ValorantDatabase
{
    public partial class GameLogs
    {
        public int GameId
        {
            get => _gameId;
            set => _gameId = value;
        }
        public int MapId
        {
            get => _mapId;
            set => _mapId = value;
        }
        public int AgentId
        {
            get => _agentId;
            set => _agentId = value;
        }
        public int ModeID
        {
            get => _modeId;
            set => _modeId = value;
        }
        public int TeamScore
        {
            get => _teamScore;
            set => _teamScore = value < 0 ? 0 : value;
        }
        public int OpponentScore
        {
            get => _opponentScore;
            set => _opponentScore = value < 0 ? 0 : value;
        }
        public int? Kills
        {
            get => _kills;
            set => _kills = value < 0 ? 0 : value;
        }
        public int? Deaths
        {
            get => _deaths;
            set => _deaths = value < 0 ? 0 : value;
        }
        public int? Assits
        {
            get => _assits;
            set => _assits = value < 0 ? 0 : value;
        }
        public float? Adr
        {
            get => _adr;
            set => _adr = value < 0 ? 0 : value;
        }
        public DateTime DateLogged
        {
            get => _dateLogged;
            set => _dateLogged = value;
        }
        public int? Season
        {
            get => _season;
            set => _season = value > 0 ? 1 : value;
        }

        public virtual Agents Agent { get; set; }
        public virtual Maps Map { get; set; }
        public virtual GameModes GameMode { get; set; }
    }
}
