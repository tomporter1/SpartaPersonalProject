using System;
using System.Collections.Generic;

namespace BussinessLayer.Interfaces
{
    public interface IGameLogManager : IBasicManager
    {
        public enum Fields
        {
            GameID,
            MapId,
            AgentID,
            Agent,
            Map,
            TeamScore,
            OpponentScore,
            Kills,
            Deaths,
            Assists,
            ADR,
            DateLogged,
            SeasonNum,
            Score,
            Result,
            KD,
            Mode,
            RankAdjustment,
            Rank
        }

        public enum Results
        {
            Win,
            Loss,
            Draw
        }

        int CurrentSeasonNum { get; set; }

        List<object> GetGamesForGameMode(object selectedGameMode, string season);

        string GetGameDataStr(object selectedGame, Fields field);

        object GetGameLogDataAsObj(object selectedGame, Fields field);

        DateTime GetDatePlayed(object selectedGame);

        Results GetMatchResult(object listItem);
    }
}