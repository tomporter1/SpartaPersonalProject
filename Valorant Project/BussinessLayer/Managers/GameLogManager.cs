using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer.Managers
{
    public class GameLogManager : SuperManager, IGameLogManager
    {
        private int _currentSeasonNum;
        private readonly ValorantContext _context;
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

        public enum Results
        {
            Win,
            Loss,
            Draw
        }

        public GameLogManager(ValorantContext context = null)
        {
            _context = context;

            ValorantContext db = (_context ?? new ValorantContext());

            int? foundSeasonNum = db.GameLogs.Max(gl => gl.Season).GetValueOrDefault();
            CurrentSeasonNum = (foundSeasonNum == null ? 1 : (int)foundSeasonNum);

            //Disposes of the db context if it is not running off a set context
            db.Dispose();
        }

        public override List<object> GetAllEntries()
        {
            ValorantContext db = (_context ?? new ValorantContext());
            List<object> output = db.GameLogs.OrderByDescending(gl => gl.DateLogged).ToList<object>();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
            return output;
        }

        public override void RemoveEntry(object selectedGame)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            GameLogs gameToRemove = (GameLogs)selectedGame;
            db.GameLogs.Remove(gameToRemove);
            db.SaveChanges();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public override void AddNewEntry(SuperArgs args)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            GameLogArgs logArgs = (GameLogArgs)args;
            Agents agent = db.Agents.Where(a => a.AgentId == logArgs.AgentId).FirstOrDefault();
            Maps map = db.Maps.Where(m => m.MapId == logArgs.MapId).FirstOrDefault();
            GameModes mode = db.GameModes.Where(gm => gm.ModeID == logArgs.ModeID).FirstOrDefault();
            Ranks rank = db.Ranks.Where(r => r.RankID == logArgs.RankID).FirstOrDefault();
            RankAdjustments rankAdjustment = db.RankAdjustments.Where(r => r.AdjustmentID == logArgs.RankAdjustmentID).FirstOrDefault();
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
                Season = CurrentSeasonNum,
                Rank = rank,
                RankAdjustment = rankAdjustment
            };

            db.GameLogs.Add(game);
            db.SaveChanges();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public object GetGameMode(object selectedGame)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            GameLogs game = (GameLogs)selectedGame;
            object output = db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.GameMode).Select(gl => gl.GameMode).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public object GetRankObj(object selectedGame)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            GameLogs game = (GameLogs)selectedGame;
            object output = db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.Rank).Select(gl => gl.Rank).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public object GetRankAdjustmentObj(object selectedGame)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            GameLogs game = (GameLogs)selectedGame;
            object output = db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.RankAdjustment).Select(gl => gl.RankAdjustment).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public override void UpdateEntry(object selectedGame, SuperArgs args)
        {
            ValorantContext db = (_context ?? new ValorantContext());
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
                gameToUpdate.RankID = logArgs.RankID;
                db.SaveChanges();
            }

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public List<object> GetGamesForGameMode(object selectedGameMode, string season)
        {
            ValorantContext db = (_context ?? new ValorantContext());

            List<object> output = db.GameLogs.AsEnumerable().Where(gl => gl.ModeID == ((GameModes)selectedGameMode).ModeID && gl.SeasonChecker(season)).OrderByDescending(gl => gl.DateLogged).ToList<object>();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public string GetGameDataStr(object selectedGame, Fields field)
        {
            ValorantContext db = (_context ?? new ValorantContext());
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

        public object GetGameRankObj(object selectedRank)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            GameLogs game = (GameLogs)selectedRank;
            object output = db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.Rank).Select(gl => gl.Rank).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }



        public object GetGameMapObj(object selectedGame)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            GameLogs game = (GameLogs)selectedGame;
            object output = db.GameLogs.Where(gl => gl.GameId == game.GameId).Include(gl => gl.Map).Select(gl => gl.Map).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public object GetGameAgentObj(object selectedGame)
        {
            ValorantContext db = (_context ?? new ValorantContext());
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

        public DateTime GetDatePlayed(object selectedGame)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            DateTime output = db.GameLogs.AsEnumerable().Where(gl => gl.GameId == ((GameLogs)selectedGame).GameId).Select(gl => gl.DateLogged).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public Results GetMatchResult(object listItem)
        {
            GameLogs game = (GameLogs)listItem;
            return game.TeamScore > game.OpponentScore ? Results.Win : (game.TeamScore < game.OpponentScore ? Results.Loss : Results.Draw);
        }        
    }
}