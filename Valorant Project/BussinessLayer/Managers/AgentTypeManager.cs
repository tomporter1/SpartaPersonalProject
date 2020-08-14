using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class AgentTypeManager : SuperManager
    {
        public enum Fields
        {
            Name,
            ImagePath
        }

        private ValorantContext _context;

        public AgentTypeManager(ValorantContext context = null)
        {
            _context = context;
        }

        public override List<object> GetAllEntries()
        {
            ValorantContext db = (_context ?? new ValorantContext());

            List<object> output = db.AgentType.OrderBy(a => a.TypeName).ToList<object>();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public override void RemoveEntry(object selectedType)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            AgentType agentToRemove = (AgentType)selectedType;
            db.AgentType.Remove(agentToRemove);
            db.SaveChanges();
        }

        public override void AddNewEntry(SuperArgs args)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            AgentTypeArgs typeArgs = (AgentTypeArgs)args;
            AgentType newAgentType = new AgentType()
            {
                TypeName = typeArgs.Name
            };
            db.AgentType.Add(newAgentType);
            db.SaveChanges();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public override void UpdateEntry(object selectedEntry, SuperArgs args)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            AgentTypeArgs typeArgs = (AgentTypeArgs)args;
            AgentType typeToUpdate = db.AgentType.Where(a => a.TypeId == ((AgentType)selectedEntry).TypeId).FirstOrDefault();
            if (typeToUpdate != null)
            {
                typeToUpdate.TypeName = typeArgs.Name;

                db.SaveChanges();
            }

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public string GetTypeDataStr(object selectedType, Fields field)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            AgentType type = (AgentType)selectedType;

            IQueryable<AgentType> typeQuery = db.AgentType.Where(t => t.TypeId == type.TypeId);

            string output = "";
            switch (field)
            {
                case Fields.Name:
                    output = typeQuery.Select(t => t.TypeName).FirstOrDefault();
                    break;
                case Fields.ImagePath:
                    output = typeQuery.Select(t => t.ImagePath).FirstOrDefault();
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