using BussinessLayer.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer.Managers
{
    public class RankManager : SuperManager
    {
        public enum Fields
        {
            Name,
            ImagePath
        }

        private ValorantContext _context;

        public RankManager(ValorantContext context = null)
        {
            _context = context;
        }

        public override void AddNewEntry(SuperArgs args)
        {
            throw new NotImplementedException();
        }

        public override List<object> GetAllEntries()
        {
            ValorantContext db = (_context ?? new ValorantContext());

            List<object> output = db.Ranks.ToList<object>();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public override void RemoveEntry(object selectedMap)
        {
            throw new NotImplementedException();
        }

        public override void UpdateEntry(object selectedEntry, SuperArgs args)
        {
            throw new NotImplementedException();
        }
    }
}