using System;
using ValorantDatabase;

namespace BussinessLayer.Args
{
    public class GameLogArgs : SuperArgs
    {
        public int ModeID { get; private set; }
        public int MapId { get; private set; }
        public int AgentId { get; private set; }
        public int TeamScore { get; private set; }
        public int OpponentScore { get; private set; }
        public int Kills { get; private set; }
        public int Deaths { get; private set; }
        public int Assists { get; private set; }
        public float ADR { get; private set; }
        public DateTime DateLogged { get; private set; }
        public int Season { get; private set; }
        public int? RankID { get; private set; }

        public GameLogArgs(object gameModeObj, object mapObj, object agentObj, int teamScore, int opponentScore, int kills, int deaths, int assists, float aDR, DateTime dateLogged, int seasonNum, object rank)
        {
            ModeID = ((GameModes)gameModeObj).ModeID;
            MapId = ((Maps)mapObj).MapId;
            AgentId = ((Agents)agentObj).AgentId;
            TeamScore = teamScore;
            OpponentScore = opponentScore;
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
            ADR = aDR;
            DateLogged = dateLogged;
            Season = seasonNum;
            RankID = rank == null ? (int?)null : ((Ranks)rank).RankID;
        }

        public GameLogArgs(int gameModeId, int mapId, int agentId, int teamScore, int opponentScore, int kills, int deaths, int assists, float aDR, DateTime dateLogged, int seasonNum, object rank)
        {
            ModeID = gameModeId;
            MapId = mapId;
            AgentId = agentId;
            TeamScore = teamScore;
            OpponentScore = opponentScore;
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
            ADR = aDR;
            DateLogged = dateLogged;
            Season = seasonNum;
            RankID = rank == null ? (int?)null : ((Ranks)rank).RankID;
        }
    }
}