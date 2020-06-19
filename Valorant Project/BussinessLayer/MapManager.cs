using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class MapManager: SuperManager
    {
        public enum Fields
        {
            Name,
            ImagePath,
            LayoutImagePath

        }

        public override List<object> GetAllEntries()
        {
            using ValorantContext db = new ValorantContext();
            return db.Maps.ToList<object>();
        }

        public override void RemoveEntry(object selectedMap)
        {
            using ValorantContext db = new ValorantContext();
            Maps mapToRemove = (Maps)selectedMap;
            db.Maps.Remove(mapToRemove);
            db.SaveChanges();
        }

        public override void AddNewEntry(SuperArgs args)
        {
            using ValorantContext db = new ValorantContext();
            MapArgs mapArgs = (MapArgs)args;
            Maps newMap = new Maps()
            {
                MapName = mapArgs.Name
            };
            db.Maps.Add(newMap);
            db.SaveChanges();
        }        

        public override void UpdateEntry(object selectedEntry, SuperArgs args)
        {
            using ValorantContext db = new ValorantContext();
            MapArgs mapArgs = (MapArgs)args;
            Maps mapToUpdate = db.Maps.Where(m => m.MapId == ((Maps)selectedEntry).MapId).FirstOrDefault();
            if (mapToUpdate != null)
            {
                mapToUpdate.MapName = mapArgs.Name;

                db.SaveChanges();
            }
        }

        public string GetMapsData(object selectedMap, Fields field)
        {
            using ValorantContext db = new ValorantContext();
            Maps map = (Maps)selectedMap;
            switch (field)
            {
                case Fields.Name:
                    return map.MapName;
                case Fields.ImagePath:
                    return db.Maps.Where(m => m.MapId == map.MapId).Select(a => a.ImagePath).FirstOrDefault();
                case Fields.LayoutImagePath:
                    return db.Maps.Where(m => m.MapId == map.MapId).Select(a => a.LayoutImagePath).FirstOrDefault();
                default:
                    return "";
            }
        }
    }
}
