using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class GameModesManager : SuperManager
    {
        private ValorantContext _context;

        public GameModesManager(ValorantContext context = null)
        {
            _context = context;
        }

        public enum Fields
        {
            Name,
            Discription
        }

        public override void AddNewEntry(SuperArgs args)
        {
            ValorantContext db = _context == null ? new ValorantContext() : _context;
            GameModeArgs modeArgs = (GameModeArgs)args;
            GameModes newMode = new GameModes()
            {
                ModeName = modeArgs.Name,
                ModeDiscription = modeArgs.Discription
            };
            db.GameModes.Add(newMode);
            db.SaveChanges();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public override List<object> GetAllEntries()
        {
            ValorantContext db = (_context == null ? new ValorantContext() : _context);

            List<object> output = db.GameModes.OrderBy(m => m.ModeName).ToList<object>();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
            return output;
        }

        public override void RemoveEntry(object selectedMode)
        {
            ValorantContext db = _context == null ? new ValorantContext() : _context;
            GameModes gameModeToRemove = (GameModes)selectedMode;
            db.GameModes.Remove(gameModeToRemove);
            db.SaveChanges();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public override void UpdateEntry(object selectedEntry, SuperArgs args)
        {
            ValorantContext db = _context == null ? new ValorantContext() : _context;
            GameModeArgs modeArgs = (GameModeArgs)args;
            GameModes gameModeToUpdate = db.GameModes.Where(m => m.ModeID == ((GameModes)selectedEntry).ModeID).FirstOrDefault();

            if (gameModeToUpdate != null)
            {
                gameModeToUpdate.ModeName = modeArgs.Name;
                gameModeToUpdate.ModeDiscription = modeArgs.Discription;

                db.SaveChanges();
            }

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }
    }
}