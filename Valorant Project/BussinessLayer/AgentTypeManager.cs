using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class AgentTypeManager : SuperManager
    {
        public enum Fields
        {
            Name
        }

        public override List<object> GetAllEntries()
        {
            using ValorantContext db = new ValorantContext();
            return db.AgentType.ToList<object>();
        }

        public override void RemoveEntry(object selectedType)
        {
            using ValorantContext db = new ValorantContext();
            AgentType agentToRemove = (AgentType)selectedType;
            db.AgentType.Remove(agentToRemove);
            db.SaveChanges();
        }

        public override void AddNewEntry(string name)
        {
            using ValorantContext db = new ValorantContext();
            AgentType newAgentType = new AgentType()
            {
                TypeName = name
            };
            db.AgentType.Add(newAgentType);
            db.SaveChanges();
        }

        public override void UpdateEntry(object selectedEntry, string newName)
        {
            using ValorantContext db = new ValorantContext();
            AgentType typeToUpdate = db.AgentType.Where(a => a.TypeId == ((AgentType)selectedEntry).TypeId).FirstOrDefault();
            if (typeToUpdate != null)
            {
                typeToUpdate.TypeName = newName;

                db.SaveChanges();
            }
        }

        public string GetTypeData(object selectedType, Fields field)
        {
            using ValorantContext db = new ValorantContext();
            AgentType type = (AgentType)selectedType;
            switch (field)
            {
                case Fields.Name:
                    return type.TypeName;
                default:
                    return "";
            }
        }
    }
}