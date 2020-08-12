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

        private ValorantContext _context;

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
        private bool SeasonChecker(GameLogs gl, string season) => int.TryParse(season, out int seasonNum) ? gl.Season == seasonNum : true;

        public List<object> GetGamesForGameMode(object selectedGameMode, string season)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);

            List<object> output = null;
            if (int.TryParse(season, out int seasonNum))
                output = db.GameLogs.Where(gl => gl.ModeID == ((GameModes)selectedGameMode).ModeID && gl.Season == seasonNum).OrderByDescending(gl => gl.DateLogged).ToList<object>();
            else
                output = db.GameLogs.Where(gl => gl.ModeID == ((GameModes)selectedGameMode).ModeID).OrderByDescending(gl => gl.DateLogged).ToList<object>();

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
            switch (field)
            {
                case Fields.GameID:
                    output = logQuery.Select(gl => gl.GameId).FirstOrDefault().ToString();
                    break;

                case Fields.AgentID:
                    output = logQuery.Select(gl => gl.AgentId).FirstOrDefault().ToString();
                    break;

                case Fields.Agent:
                    output = logQuery.Include(gl => gl.Agent).Select(gl => gl.Agent).FirstOrDefault().ToString();
                    break;

                case Fields.MapId:
                    output = logQuery.Select(gl => gl.MapId).FirstOrDefault().ToString();
                    break;

                case Fields.Map:
                    output = logQuery.Include(gl => gl.Map).Select(gl => gl.Map).FirstOrDefault().ToString();
                    break;

                case Fields.TeamScore:
                    output = logQuery.Select(gl => gl.TeamScore).FirstOrDefault().ToString();
                    break;

                case Fields.OpponentScore:
                    output = logQuery.Select(gl => gl.OpponentScore).FirstOrDefault().ToString();
                    break;

                case Fields.Kills:
                    output = logQuery.Select(gl => gl.Kills).FirstOrDefault().ToString();
                    break;

                case Fields.Deaths:
                    output = logQuery.Select(gl => gl.Deaths).FirstOrDefault().ToString();
                    break;

                case Fields.Assists:
                    output = logQuery.Select(gl => gl.Assits).FirstOrDefault().ToString();
                    break;

                case Fields.ADR:
                    output = logQuery.Select(gl => gl.Adr).FirstOrDefault().ToString();
                    break;

                case Fields.DateLogged:
                    output = logQuery.Select(gl => gl.DateLogged).FirstOrDefault().ToString();
                    break;

                case Fields.SeasonNum:
                    output = logQuery.Select(gl => gl.Season).FirstOrDefault().ToString();
                    break;

                default:
                    output = "";
                    break;
            }

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
            object output = db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.Agent).Select(gl => gl.Agent).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        #region GameStatMethods

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
            IQueryable<GameLogs> MapsWithWinsQuery = null;
            if (int.TryParse(season, out int seasonNum))
                MapsWithWinsQuery = db.GameLogs.Where(gl => gl.Season == seasonNum && gl.TeamScore > gl.OpponentScore && gl.ModeID == ((GameModes)selectedGameMode).ModeID);
            else
                MapsWithWinsQuery = db.GameLogs.Where(gl => gl.TeamScore > gl.OpponentScore && gl.ModeID == ((GameModes)selectedGameMode).ModeID);

            //if there are no entries in the dbs then it returns null
            if (MapsWithWinsQuery.ToList().Count == 0)
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

        public int GetTotals(Fields field, string season)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            int output = 0;

            IQueryable<GameLogs> queryBase = null;
            if (int.TryParse(season, out int seasonNum))
                queryBase = db.GameLogs.Where(gl => gl.Season == seasonNum);
            else
                queryBase = db.GameLogs;

            switch (field)
            {
                case Fields.Kills:
                    output = (int)queryBase.Select(gl => gl.Kills).Sum();
                    break;

                case Fields.Deaths:
                    output = (int)queryBase.Select(gl => gl.Deaths).Sum();
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

        public int GetTotals(Fields field, object selectedGameMode, string season)
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            int output = 0;

            IQueryable<GameLogs> query = null;
            if (int.TryParse(season, out int seasonNum))
                query = db.GameLogs.Where(gl => gl.Season == seasonNum && gl.ModeID == ((GameModes)selectedGameMode).ModeID);
            else
                query = db.GameLogs.Where(gl => gl.ModeID == ((GameModes)selectedGameMode).ModeID);

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
            GameLogs game = (GameLogs)selectedGame;
            ValorantContext db = (_context == null ? new ValorantContext() : _context);
            DateTime output = db.GameLogs.Where(gl => gl.GameId == game.GameId).Select(gl => gl.DateLogged).FirstOrDefault();

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

            if (int.TryParse(season, out int seasonNum))
            {
                totalWins = db.GameLogs.Where(gl => gl.Season == seasonNum && gl.TeamScore > gl.OpponentScore && gl.ModeID == ((GameModes)selectedGameMode).ModeID).Count();
                totalLosses = db.GameLogs.Where(gl => gl.Season == seasonNum && gl.TeamScore < gl.OpponentScore && gl.ModeID == ((GameModes)selectedGameMode).ModeID).Count();
            }
            else
            {
                totalWins = db.GameLogs.Where(gl => gl.TeamScore > gl.OpponentScore && gl.ModeID == ((GameModes)selectedGameMode).ModeID).Count();
                totalLosses = db.GameLogs.Where(gl => gl.TeamScore < gl.OpponentScore && gl.ModeID == ((GameModes)selectedGameMode).ModeID).Count();
            }

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