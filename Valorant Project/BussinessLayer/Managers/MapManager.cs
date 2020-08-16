using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer.Managers
{
    public class MapManager : SuperManager, IMapManager
    {
        private ValorantContext _context;

        public MapManager(ValorantContext context = null)
        {
            _context = context;
        }

        public enum Fields
        {
            Name,
            ImagePath,
            LayoutImagePath
        }

        public override List<object> GetAllEntries()
        {
            ValorantContext db = _context ?? new ValorantContext();

            List<object> output = db.Maps.OrderBy(m => m.MapName).ToList<object>();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
            return output;
        }

        public override void RemoveEntry(object selectedMap)
        {
            ValorantContext db = _context ?? new ValorantContext();
            Maps mapToRemove = (Maps)selectedMap;
            db.Maps.Remove(mapToRemove);
            db.SaveChanges();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public override void AddNewEntry(SuperArgs args)
        {
            ValorantContext db = _context ?? new ValorantContext();
            MapArgs mapArgs = (MapArgs)args;
            Maps newMap = new Maps()
            {
                MapName = mapArgs.Name
            };
            db.Maps.Add(newMap);
            db.SaveChanges();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public override void UpdateEntry(object selectedEntry, SuperArgs args)
        {
            ValorantContext db = _context ?? new ValorantContext();
            MapArgs mapArgs = (MapArgs)args;
            Maps mapToUpdate = db.Maps.Where(m => m.MapId == ((Maps)selectedEntry).MapId).FirstOrDefault();
            if (mapToUpdate != null)
            {
                mapToUpdate.MapName = mapArgs.Name;

                db.SaveChanges();
            }
            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public string GetMapsDataStr(object selectedMap, Fields field)
        {
            ValorantContext db = _context ?? new ValorantContext();
            Maps map = (Maps)selectedMap;

            IQueryable<Maps> mapQuery = db.Maps.Where(m => m.MapId == map.MapId);

            string output = "";
            switch (field)
            {
                case Fields.Name:
                    output = map.MapName;
                    break;
                case Fields.ImagePath:
                    output = mapQuery.Select(a => a.ImagePath).FirstOrDefault();
                    break;
                case Fields.LayoutImagePath:
                    output = mapQuery.Select(a => a.LayoutImagePath).FirstOrDefault();
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
    }
}