using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class GameLogManager: SuperManager
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

        public override List<object> GetAllEntries()
        {
            using ValorantContext db = new ValorantContext();
            return db.GameLogs.OrderByDescending(gl => gl.DateLogged).ToList<object>();
        }

        public override void RemoveEntry(object selectedGame)
        {
            using ValorantContext db = new ValorantContext();
            GameLogs gameToRemove = (GameLogs)selectedGame;
            db.GameLogs.Remove(gameToRemove);
            db.SaveChanges();
        }

        public override void AddNewEntry(SuperArgs args)
        {
            using ValorantContext db = new ValorantContext();
            GameLogArgs logArgs = (GameLogArgs)args;
            Agents agent = db.Agents.Where(a => a.AgentId == logArgs.AgentId).FirstOrDefault();
            Maps map = db.Maps.Where(m => m.MapId == logArgs.MapId).FirstOrDefault();
            GameLogs game = new GameLogs()
            {
                TeamScore = logArgs.TeamScore,
                OpponentScore = logArgs.OpponentScore,
                Kills = logArgs.Kills,
                Deaths = logArgs.Deaths,
                Assits = logArgs.Assists,
                Adr = logArgs.ADR,
                DateLogged = logArgs.DateLogged,
                Map = map,
                Agent = agent
            };

            db.GameLogs.Add(game);
            db.SaveChanges();
        }

        public override void UpdateEntry(object selectedGame, SuperArgs args)
        {
            using ValorantContext db = new ValorantContext();
            GameLogArgs logArgs = (GameLogArgs)args;

            GameLogs gameToUpdate = db.GameLogs
                .Where(gl => gl.GameId == ((GameLogs)selectedGame).GameId)
                .FirstOrDefault();

            if (gameToUpdate != null)
            {
                gameToUpdate.TeamScore = logArgs.TeamScore;
                gameToUpdate.OpponentScore = logArgs.OpponentScore;
                gameToUpdate.MapId = logArgs.MapId;
                gameToUpdate.AgentId = logArgs.AgentId;
                gameToUpdate.Kills = logArgs.Kills;
                gameToUpdate.Assits = logArgs.Assists;
                gameToUpdate.Deaths = logArgs.Deaths;
                gameToUpdate.Adr = logArgs.ADR;
                gameToUpdate.DateLogged = logArgs.DateLogged;

                db.SaveChanges();
            }
        }

        public object GetMostPlayedAgent()
        {
            using ValorantContext db = new ValorantContext();
            int favAgentID = db.GameLogs
                    .GroupBy(
                        gl => gl.AgentId,
                        gl => gl,
                        (agent, games) => new
                        {
                            AgentId = agent,
                            GamesPlayed = games.Count()
                        })
                    .OrderByDescending(m => m.GamesPlayed)
                    .FirstOrDefault().AgentId;

            return db.Agents.Where(m => m.AgentId == favAgentID).FirstOrDefault();
        }

        public object GetBestMap()
        {
            using ValorantContext db = new ValorantContext();
            int bestMapID = db.GameLogs
                    .Where(gl => gl.TeamScore > gl.OpponentScore)
                    .GroupBy(
                        gl => gl.MapId,
                        gl => gl,
                        (map, games) => new
                        {
                            MapID = map,
                            GamesWon = games.Count()
                        })
                    .OrderByDescending(m => m.GamesWon)
                    .FirstOrDefault().MapID;

            return db.Maps.Where(m => m.MapId == bestMapID).FirstOrDefault();
        }

        public int GetTotals(Fields field)
        {
            using ValorantContext db = new ValorantContext();
            switch (field)
            {
                case Fields.Kills:
                    return (int)db.GameLogs.Select(gl => gl.Kills).Sum();
                case Fields.Deaths:
                    return (int)db.GameLogs.Select(gl => gl.Deaths).Sum();
                default:
                    return 0;
            }
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
            float totalKills = GetTotals(Fields.Kills);
            float totalDeaths = GetTotals(Fields.Deaths);

            if (totalDeaths == 0)
                return totalKills;
            return totalKills / totalDeaths;
        }

        public float GetTotalWinLoss()
        {
            using ValorantContext db = new ValorantContext();
            float totalWins = db.GameLogs.Where(gl => gl.TeamScore > gl.OpponentScore).Count();
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
                    return db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.Agent).Select(gl => gl.Agent).FirstOrDefault().ToString();
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

        
    }
}
