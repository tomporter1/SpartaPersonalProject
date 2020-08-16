using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer.Managers
{
    public class GameModesManager : SuperManager, IModeManager
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
            ValorantContext db = _context ?? new ValorantContext();
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
            ValorantContext db = _context ?? new ValorantContext();

            List<object> output = db.GameModes.OrderBy(m => m.ModeName).ToList<object>();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
            return output;
        }

        public override void RemoveEntry(object selectedMode)
        {
            ValorantContext db = _context ?? new ValorantContext();
            GameModes gameModeToRemove = (GameModes)selectedMode;
            db.GameModes.Remove(gameModeToRemove);
            db.SaveChanges();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public override void UpdateEntry(object selectedEntry, SuperArgs args)
        {
            ValorantContext db = _context ?? new ValorantContext();
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

        public bool IsRanked(object selectedItem)
        {
            GameModes mode = (GameModes)selectedItem;
            ValorantContext db = _context ?? new ValorantContext();
            GameModes rankedMode = db.GameModes.Where(m => m.ModeName == "Ranked").FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return mode == rankedMode;
        }
    }
}