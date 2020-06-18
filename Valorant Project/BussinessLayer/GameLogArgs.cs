using System;
using ValorantDatabase;

namespace BussinessLayer
{
    public class GameLogArgs
    {
        public int MapId { get; private set; }
        public int AgentId { get; private set; }
        public int TeamScore { get; private set; }
        public int OpponentScore { get; private set; }
        public int Kills { get; private set; }
        public int Deaths { get; private set; }
        public int Assists { get; private set; }
        public int ADR { get; private set; }
        public DateTime DateLogged { get; private set; }
       
        public GameLogArgs(Maps map, Agents agent, int teamScore, int opponentScore, int kills, int deaths, int assists, int aDR, DateTime dateLogged)
        {
            MapId = map.MapId;
            AgentId = agent.AgentId;
            TeamScore = teamScore;
            OpponentScore = opponentScore;
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
            ADR = aDR;
            DateLogged = dateLogged;
        }

        public GameLogArgs(int mapId, int agentId, int teamScore, int opponentScore, int kills, int deaths, int assists, int aDR, DateTime dateLogged)
        {
            MapId = mapId;
            AgentId = agentId;
            TeamScore = teamScore;
            OpponentScore = opponentScore;
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
            ADR = aDR;
            DateLogged = dateLogged;
        }
    }
}