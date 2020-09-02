using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValorantDatabase;

namespace BussinessLayer.Managers
{
    public class RankAdjustmentManager : SuperManager, IRankAdjustmentManager
    {
        public enum Fields
        {
            Name,
            ImagePath
        }

        private readonly ValorantContext _context;

        public RankAdjustmentManager(ValorantContext context = null)
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

            List<object> output = db.RankAdjustments.ToList<object>();

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

        public string GetRankAdjustmentDataStr(object selectedRankAdjustment, Fields field)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            RankAdjustments rankAdjustment = (RankAdjustments)selectedRankAdjustment;
            if (rankAdjustment == null)
            {
                if (_context == null)
                    db.Dispose();

                return "";
            }

            IQueryable<RankAdjustments> rankQuery = db.RankAdjustments.Where(r => r.AdjustmentID == rankAdjustment.AdjustmentID);
            string output = "";
            switch (field)
            {
                case Fields.ImagePath:
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