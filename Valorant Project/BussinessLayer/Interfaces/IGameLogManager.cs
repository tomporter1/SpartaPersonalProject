using BussinessLayer.Managers;
using System;
using System.Collections.Generic;

namespace BussinessLayer.Interfaces
{
    public interface IGameLogManager : IBasicManager
    {
        int CurrentSeasonNum { get; set; }
        List<object> GetGamesForGameMode(object selectedGameMode, string season);
        string GetGameDataStr(object selectedGame, GameLogManager.Fields field);
        object GetGameLogDataAsObj(object selectedGame, GameLogManager.Fields field);
        DateTime GetDatePlayed(object selectedGame);
        GameLogManager.Results GetMatchResult(object listItem);
    }
}