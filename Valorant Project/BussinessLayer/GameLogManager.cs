﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class GameLogManager
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
            DateLogged
        }

        public List<GameLogs> GetAllGames()
        {
            using ValorantContext db = new ValorantContext();
            return db.GameLogs.OrderByDescending(gl=>gl.DateLogged).ToList();
        }

        public void AddNewGame(GameLogArgs args)
        {
            using ValorantContext db = new ValorantContext();
            Agents agent = db.Agents.Where(a => a.AgentId == args.AgentId).FirstOrDefault();
            Maps map = db.Maps.Where(m => m.MapId == args.MapId).FirstOrDefault();
            GameLogs game = new GameLogs()
            {
                TeamScore = args.TeamScore,
                OpponentScore = args.OpponentScore,
                Kills = args.Kills,
                Deaths = args.Deaths,
                Assits = args.Assists,
                Adr = args.ADR,
                DateLogged = args.DateLogged,
                Map = map,
                Agent = agent
            };

            db.GameLogs.Add(game);
            db.SaveChanges();
        }

        public DateTime GetDatePlayed(object selectedGame)
        {
            GameLogs game = (GameLogs)selectedGame;
            using ValorantContext db = new ValorantContext();
            return db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.DateLogged).FirstOrDefault();
            throw new NotImplementedException();
        }

        public float GetTotalKD()
        {
            using ValorantContext db = new ValorantContext();
            float totalKills = (float)db.GameLogs.Select(gl=>gl.Kills).Sum();
            float totalDeaths = (float)db.GameLogs.Select(gl=>gl.Deaths).Sum();

            if (totalDeaths == 0)
                return totalKills;
            return totalKills / totalDeaths;
        }

        public float GetTotalWinLoss()
        {
            using ValorantContext db = new ValorantContext();
            float totalWins = db.GameLogs.Where(gl =>gl.TeamScore > gl.OpponentScore).Count();
            float totalLosses = db.GameLogs.Where(gl => gl.TeamScore < gl.OpponentScore).Count();
            
            if (totalLosses == 0)
                return totalWins;
            return totalWins / totalLosses;
        }

        public string GetGameData(object selectedGame, Fields field)
        {
            using ValorantContext db = new ValorantContext();
            GameLogs game = (GameLogs)selectedGame;
            switch (field)
            {
                case Fields.GameID:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.GameId).FirstOrDefault().ToString();
                case Fields.AgentID:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.AgentId).FirstOrDefault().ToString();
                case Fields.Agent:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl=>gl.Agent).Select(gl => gl.Agent).FirstOrDefault().ToString();
                case Fields.MapId:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.MapId).FirstOrDefault().ToString();
                case Fields.Map:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.Map).Select(gl => gl.Map).FirstOrDefault().ToString();
                case Fields.TeamScore:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.TeamScore).FirstOrDefault().ToString();
                case Fields.OpponentScore:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.OpponentScore).FirstOrDefault().ToString();
                case Fields.Kills:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.Kills).FirstOrDefault().ToString();
                case Fields.Deaths:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.Deaths).FirstOrDefault().ToString();
                case Fields.Assists:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.Assits).FirstOrDefault().ToString();
                case Fields.ADR:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.Adr).FirstOrDefault().ToString();
                case Fields.DateLogged:
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.DateLogged).FirstOrDefault().ToString();
                default:
                    return "";
            }
        }

        public object GetGameMap(object selectedGame)
        {
            using ValorantContext db = new ValorantContext();
            GameLogs game = (GameLogs)selectedGame;
            return db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.Map).Select(gl => gl.Map).FirstOrDefault();
        }

        public object GetGameAgent(object selectedGame)
        {
            using ValorantContext db = new ValorantContext();
            GameLogs game = (GameLogs)selectedGame;
            return db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.Agent).Select(gl => gl.Agent).FirstOrDefault();
        }

        public void RemoveGame(object selectedGame)
        {
            using ValorantContext db = new ValorantContext();
            GameLogs gameToRemove = (GameLogs)selectedGame;
            db.GameLogs.Remove(gameToRemove);
            db.SaveChanges();
        }

        public void UpdateGame(object selectedGame, GameLogArgs args)
        {
            using ValorantContext db = new ValorantContext();

            GameLogs gameToUpdate = db.GameLogs
                .Where(gl => gl.GameId == ((GameLogs)selectedGame).GameId)
                .FirstOrDefault();

            if (gameToUpdate != null)
            {
                gameToUpdate.TeamScore = args.TeamScore;
                gameToUpdate.OpponentScore = args.OpponentScore;
                gameToUpdate.MapId = args.MapId;
                gameToUpdate.AgentId = args.AgentId;
                gameToUpdate.Kills = args.Kills;
                gameToUpdate.Assits = args.Assists;
                gameToUpdate.Deaths = args.Deaths;
                gameToUpdate.Adr = args.ADR;
                gameToUpdate.DateLogged = args.DateLogged;


                db.SaveChanges();
            }
        }
    }
}
