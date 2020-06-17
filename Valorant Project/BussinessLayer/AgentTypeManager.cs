using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class AgentTypeManager
    {
        public enum Fields
        {
            Name
        }

        public List<AgentType> GetAllTypes()
        {
            using ValorantContext db = new ValorantContext();
            return db.AgentType.ToList();
        }

        public void AddNewAgentType(string name)
        {
            using ValorantContext db = new ValorantContext();
            AgentType newAgentType = new AgentType()
            {
                TypeName = name
            };
            db.AgentType.Add(newAgentType);
            db.SaveChanges();
        }

        public void RemoveAgentType(object selectedType)
        {
            using ValorantContext db = new ValorantContext();
            AgentType agentToRemove = (AgentType)selectedType;
            db.AgentType.Remove(agentToRemove);
            db.SaveChanges();
        }

        public void UpdateAgentType(object SelectedType,string newName)
        {
            using ValorantContext db = new ValorantContext();
            AgentType typeToUpdate = db.AgentType.Where(a => a.TypeId == ((AgentType)SelectedType).TypeId).FirstOrDefault();
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
