using BussinessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer.Managers
{
    public class StatsManager : IStats
    {
        private readonly ValorantContext _context;

        public StatsManager(ValorantContext context = null)
        {
            _context = context;
        }        

        public object GetMostKillsGame(string season, object gameMode)
        {
            ValorantContext db = (_context ?? new ValorantContext());

            object output = db.GameLogs.AsEnumerable().Where(gl => gl.SeasonChecker(season) && gl.ModeID == ((GameModes)gameMode).ModeID).OrderByDescending(gl => gl.Kills).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public object GetMostKDGame(string season, object gameMode)
        {
            ValorantContext db = (_context ?? new ValorantContext());

            object output = db.GameLogs.AsEnumerable().Where(gl => gl.SeasonChecker(season) && gl.ModeID == ((GameModes)gameMode).ModeID).OrderByDescending(gl => gl.KD).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public object GetMostPlayedAgent(object selectedGameMode, string season)
        {
            ValorantContext db = (_context ?? new ValorantContext());

            //Makes a base query for all seasons or a specific season
            IEnumerable<GameLogs> ModeQuery = db.GameLogs.AsEnumerable().Where(gl => gl.SeasonChecker(season) && gl.ModeID == ((GameModes)selectedGameMode).ModeID);

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
            ValorantContext db = (_context ?? new ValorantContext());

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
            ValorantContext db = (_context ?? new ValorantContext());

            //Makes a base query for all seasons or a specific season
            List<GameLogs> MapsWithWinsQuery = db.GameLogs.AsEnumerable().Where(gl => gl.SeasonChecker(season) && gl.TeamScore > gl.OpponentScore && gl.ModeID == ((GameModes)selectedGameMode).ModeID).ToList();

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

        public int GetTotals(GameLogManager.Fields field, object selectedGameMode, string season)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            int output = 0;

            IEnumerable<GameLogs> query = db.GameLogs.AsEnumerable().Where(gl => gl.SeasonChecker(season) && gl.ModeID == ((GameModes)selectedGameMode).ModeID);

            switch (field)
            {
                case GameLogManager.Fields.Kills:
                    output = (int)query.Select(gl => gl.Kills).Sum();
                    break;
                case GameLogManager.Fields.Deaths:
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

        public float GetTotalKD(object selectedGameMode, string season)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            float totalKills = GetTotals(GameLogManager.Fields.Kills, selectedGameMode, season);
            float totalDeaths = GetTotals(GameLogManager.Fields.Deaths, selectedGameMode, season);

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return totalDeaths == 0 ? totalKills : totalKills / totalDeaths;
        }

        public float GetTotalWinLoss(object selectedGameMode, string season)
        {
            ValorantContext db = (_context ?? new ValorantContext());

            float totalWins = db.GameLogs.AsEnumerable().Where(gl => gl.SeasonChecker(season) && gl.TeamScore > gl.OpponentScore && gl.ModeID == ((GameModes)selectedGameMode).ModeID).Count();
            float totalLosses = db.GameLogs.AsEnumerable().Where(gl => gl.SeasonChecker(season) && gl.TeamScore < gl.OpponentScore && gl.ModeID == ((GameModes)selectedGameMode).ModeID).Count();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return totalLosses == 0 ? totalWins : totalWins / totalLosses;
        }
    }
}