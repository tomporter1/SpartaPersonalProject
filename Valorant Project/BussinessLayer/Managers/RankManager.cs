using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer.Managers
{
    public class RankManager : SuperManager, IRanksManager
    {
        private readonly ValorantContext _context;

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

        public string GetRankDataStr(object selectedRank, IRanksManager.Fields field)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            Ranks rank = (Ranks)selectedRank;
            if (rank == null)
            {
                if (_context == null)
                    db.Dispose();

                return "";
            }

            IQueryable<Ranks> rankQuery = db.Ranks.Where(r => r.RankID == rank.RankID);
            string output = "";
            switch (field)
            {
                case IRanksManager.Fields.ImagePath:
                    output = rankQuery.Select(r => r.ImagePath).FirstOrDefault();
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