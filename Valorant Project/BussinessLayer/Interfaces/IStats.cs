﻿namespace BussinessLayer.Interfaces
{
    public interface IStats
    {
        object GetMostKillsGame(string season, object gameMode);

        object GetMostKDGame(string season, object gameMode);

        object GetMostPlayedAgent(object selectedGameMode, string season);

        object GetMostPlayedClass(object selectedGameMode, string season);

        object GetMapWithMostWins(object selectedGameMode, string season);

        int GetTotals(IGameLogManager.Fields field, object selectedGameMode, string season);

        float GetTotalKD(object selectedGameMode, string season);

        float GetTotalWinLoss(object selectedGameMode, string season);
    }
}