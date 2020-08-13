using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class GameLogManager : SuperManager
    {
        private int _currentSeasonNum;
        private ValorantContext _context;
        public int CurrentSeasonNum { get => _currentSeasonNum; set => _currentSeasonNum = value > 0 ? value : 1; }

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
            SeasonNum
        }

        public GameLogManager(ValorantContext context = null)
        {
            _context = context;

            ValorantContext db = (_context == null ? new ValorantContext() : _context);

            int? foundSeasonNum = db.GameLogs.Max(gl => gl.Season).GetValueOrDefault();
            CurrentSeasonNum = (foundSeasonNum == null ? 1 : (int)foundSeasonNum);

            //Disposes of the db context if it is not running off a set context
            db.Dispose();
        }

        public override List<object> GetAllEntries()
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            List<object> output = db.GameLogs.OrderByDescending(gl => gl.DateLogged).ToList<object>();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
            return output;
        }

        public override void RemoveEntry(object selectedGame)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            GameLogs gameToRemove = (GameLogs)selectedGame;
            db.GameLogs.Remove(gameToRemove);
            db.SaveChanges();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public override void AddNewEntry(SuperArgs args)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            GameLogArgs logArgs = (GameLogArgs)args;
            Agents agent = db.Agents.Where(a => a.AgentId == logArgs.AgentId).FirstOrDefault();
            Maps map = db.Maps.Where(m => m.MapId == logArgs.MapId).FirstOrDefault();
            GameModes mode = db.GameModes.Where(gm => gm.ModeID == logArgs.ModeID).FirstOrDefault();
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
                Agent = agent,
                GameMode = mode,
                Season = CurrentSeasonNum
            };

            db.GameLogs.Add(game);
            db.SaveChanges();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public object GetGameMode(object selectedGame)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            GameLogs game = (GameLogs)selectedGame;
            object output = db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.GameMode).Select(gl => gl.GameMode).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public override void UpdateEntry(object selectedGame, SuperArgs args)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
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
                gameToUpdate.ModeID = logArgs.ModeID;
                gameToUpdate.Kills = logArgs.Kills;
                gameToUpdate.Assits = logArgs.Assists;
                gameToUpdate.Deaths = logArgs.Deaths;
                gameToUpdate.Adr = logArgs.ADR;
                gameToUpdate.DateLogged = logArgs.DateLogged;
                gameToUpdate.Season = logArgs.Season;

                db.SaveChanges();
            }

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public List<object> GetGamesForGameMode(object selectedGameMode, string season)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);

            List<object> output = db.GameLogs.AsEnumerable()
                .Where(gl => gl.ModeID == ((GameModes)selectedGameMode).ModeID && gl.SeasonChecker(season))
                .OrderByDescending(gl => gl.DateLogged)
                .ToList<object>();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public string GetGameDataStr(object selectedGame, Fields field)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            GameLogs game = (GameLogs)selectedGame;

            IQueryable<GameLogs> logQuery = db.GameLogs.Where(gl => gl.GameId == game.GameId);

            string output = "";
            output = field switch
            {
                Fields.GameID => logQuery.Select(gl => gl.GameId).FirstOrDefault().ToString(),
                Fields.AgentID => logQuery.Select(gl => gl.AgentId).FirstOrDefault().ToString(),
                Fields.Agent => logQuery.Include(gl => gl.Agent).Select(gl => gl.Agent).FirstOrDefault().ToString(),
                Fields.MapId => logQuery.Select(gl => gl.MapId).FirstOrDefault().ToString(),
                Fields.Map => logQuery.Include(gl => gl.Map).Select(gl => gl.Map).FirstOrDefault().ToString(),
                Fields.TeamScore => logQuery.Select(gl => gl.TeamScore).FirstOrDefault().ToString(),
                Fields.OpponentScore => logQuery.Select(gl => gl.OpponentScore).FirstOrDefault().ToString(),
                Fields.Kills => logQuery.Select(gl => gl.Kills).FirstOrDefault().ToString(),
                Fields.Deaths => logQuery.Select(gl => gl.Deaths).FirstOrDefault().ToString(),
                Fields.Assists => logQuery.Select(gl => gl.Assits).FirstOrDefault().ToString(),
                Fields.ADR => logQuery.Select(gl => gl.Adr).FirstOrDefault().ToString(),
                Fields.DateLogged => logQuery.Select(gl => gl.DateLogged).FirstOrDefault().ToString(),
                Fields.SeasonNum => logQuery.Select(gl => gl.Season).FirstOrDefault().ToString(),
                _ => "",
            };

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public object GetGameMapObj(object selectedGame)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            GameLogs game = (GameLogs)selectedGame;
            object output = db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.Map).Select(gl => gl.Map).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public object GetGameAgentObj(object selectedGame)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            GameLogs game = (GameLogs)selectedGame;
            object output = db.GameLogs
                .Where(gl => gl.GameId == game.GameId)
                .Include(gl => gl.Agent)
                .Select(gl => gl.Agent)
                .FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        #region GameStatMethods
        public object GetMostKillsGame(string season, object gameMode)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);

            object output = db.GameLogs.AsEnumerable()
                .Where(gl => gl.SeasonChecker(season) && gl.ModeID == ((GameModes)gameMode).ModeID)
                .OrderByDescending(gl => gl.Kills)
                .FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public object GetMostKDGame(string season, object gameMode)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);

            object output = db.GameLogs.AsEnumerable()
                .Where(gl => gl.SeasonChecker(season) && gl.ModeID == ((GameModes)gameMode).ModeID)
                .OrderByDescending(gl => gl.KD)
                .FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public object GetMostPlayedAgent(object selectedGameMode, string season)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);

            //Makes a base query for all seasons or a specific season
            IQueryable<GameLogs> ModeQuery = null;
            if (int.TryParse(season, out int seasonNum))
                ModeQuery = db.GameLogs.Where(gl => gl.Season == seasonNum && gl.ModeID == ((GameModes)selectedGameMode).ModeID);
            else
                ModeQuery = db.GameLogs.Where(gl => gl.ModeID == ((GameModes)selectedGameMode).ModeID);

            //if there are no entries in the dbs then it returns null
            if (ModeQuery.ToList().Count == 0)
                return null;

            int favAgentID = ModeQuery
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

            object output = db.Agents.Where(m => m.AgentId == favAgentID).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public object GetMostPlayedClass(object selectedGameMode, string season)
        {
            Agents faveAgent = (Agents)GetMostPlayedAgent(selectedGameMode, season);
            ValorantContext db = (_context == null ? new ValorantContext() : _context);

            if (faveAgent == null)
                return null;

            object output = db.AgentType.Where(t => t.TypeId == faveAgent.AgentTypeId).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public object GetMapWithMostWins(object selectedGameMode, string season)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);

            //Makes a base query for all seasons or a specific season
            List<GameLogs> MapsWithWinsQuery = db.GameLogs.AsEnumerable()
                .Where(gl => gl.SeasonChecker(season) && 
                       gl.TeamScore > gl.OpponentScore && 
                       gl.ModeID == ((GameModes)selectedGameMode).ModeID)
                .ToList();

            //if there are no entries in the dbs then it returns null
            if (MapsWithWinsQuery.Count == 0)
                return null;

            int bestMapID = MapsWithWinsQuery
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

            object output = db.Maps.Where(m => m.MapId == bestMapID).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public int GetTotals(Fields field, object selectedGameMode, string season)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            int output = 0;

            IEnumerable<GameLogs> query = db.GameLogs.AsEnumerable()
                .Where(gl => gl.SeasonChecker(season) && 
                       gl.ModeID == ((GameModes)selectedGameMode).ModeID);

            switch (field)
            {
                case Fields.Kills:
                    output = (int)query.Select(gl => gl.Kills).Sum();
                    break;
                case Fields.Deaths:
                    output = (int)query.Select(gl => gl.Deaths).Sum();
                    break;
                default:
                    output = 0;
                    break;
            }

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public DateTime GetDatePlayed(object selectedGame)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            DateTime output = db.GameLogs.AsEnumerable()
                .Where(gl => gl.GameId == ((GameLogs)selectedGame).GameId)
                .Select(gl => gl.DateLogged)
                .FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public float GetTotalKD(object selectedGameMode, string season)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            float totalKills = GetTotals(Fields.Kills, selectedGameMode, season);
            float totalDeaths = GetTotals(Fields.Deaths, selectedGameMode, season);

            float output;
            if (totalDeaths == 0)
            {
                output = totalKills;
            }
            else
            {
                output = totalKills / totalDeaths;
            }

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public float GetTotalWinLoss(object selectedGameMode, string season)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            float totalWins = 0;
            float totalLosses = 0;

            totalWins = db.GameLogs.AsEnumerable().Where(gl => gl.SeasonChecker(season) && gl.TeamScore > gl.OpponentScore && gl.ModeID == ((GameModes)selectedGameMode).ModeID).Count();
            totalLosses = db.GameLogs.AsEnumerable().Where(gl => gl.SeasonChecker(season) && gl.TeamScore < gl.OpponentScore && gl.ModeID == ((GameModes)selectedGameMode).ModeID).Count();

            float output;
            if (totalLosses == 0)
            {
                output = totalWins;
            }
            else
            {
                output = totalWins / totalLosses;
            }

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }
        #endregion GameStatMethods
    }
}