using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class MapManager
    {
        public enum Fields
        {
            Name
        }

        public List<Maps> GetAllMaps()
        {
            using ValorantContext db = new ValorantContext();
            return db.Maps.ToList();
        }

        public void AddNewMap(string name)
        {
            using ValorantContext db = new ValorantContext();
            Maps newMap = new Maps()
            {
                MapName = name
            };
            db.Maps.Add(newMap);
            db.SaveChanges();
        }

        public void RemoveMap(object selectedMap)
        {
            using ValorantContext db = new ValorantContext();
            Maps mapToRemove = (Maps)selectedMap;
            db.Maps.Remove(mapToRemove);
            db.SaveChanges();
        }

        public void UpdateMap(object selectedMap, string newName)
        {
            using ValorantContext db = new ValorantContext();
            Maps mapToUpdate = db.Maps.Where(m => m.MapId == ((Maps)selectedMap).MapId).FirstOrDefault();
            if (mapToUpdate != null)
            {
                mapToUpdate.MapName = newName;

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
                default:
                    return "";
            }
        }
    }
}
