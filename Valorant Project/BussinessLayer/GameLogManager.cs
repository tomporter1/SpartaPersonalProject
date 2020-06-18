using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValorantDatabase;

namespace BussinessLayer
{
    public class GameLogManager
    {
        public enum Fields
        {
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
            return db.GameLogs.ToList();
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
                gameToUpdate.AgentId= args.AgentId;
                gameToUpdate.Kills= args.Kills;
                gameToUpdate.Assits= args.Assists;
                gameToUpdate.Deaths= args.Deaths;
                gameToUpdate.Adr= args.ADR;
                gameToUpdate.DateLogged= args.DateLogged;


                db.SaveChanges();
            }
        }
    }
}
