using BussinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IGameLogManager : IBasicManager
    {
        int CurrentSeasonNum { get; set; }

        object GetGameMode(object selectedGame);
        object GetRankObj(object selectedGame);
        List<object> GetGamesForGameMode(object selectedGameMode, string season);
        string GetGameDataStr(object selectedGame, GameLogManager.Fields field);
        object GetGameRankObj(object selectedRank);
        object GetGameMapObj(object selectedGame);
        object GetGameAgentObj(object selectedGame);
        DateTime GetDatePlayed(object selectedGame);
        GameLogManager.Results GetMatchResult(object listItem);
    }
}