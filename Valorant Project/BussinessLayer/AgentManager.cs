using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class AgentManager : SuperManager
    {
        public enum Fields
        {
            Name,
            SignatureAbilityName,
            SignatureAbilityDiscription,
            UltamateAbilityName,
            UltamateAbilityDiscription,
            AbilityOneName,
            AbilityOneDiscription,
            AbilityTwoName,
            AbilityTwoDiscription,
            Bio,
            ID,
            Type,
            ImagePath
        }

        public override List<object> GetAllEntries()
        {
            using ValorantContext db = new ValorantContext();
            return db.Agents.OrderBy(a => a.AgentName).ToList<object>();
        }

        public override void RemoveEntry(object selectedAgent)
        {
            using ValorantContext db = new ValorantContext();
            Agents agentToRemove = (Agents)selectedAgent;
            db.Agents.Remove(agentToRemove);
            db.SaveChanges();
        }

        public override void AddNewEntry(SuperArgs args)
        {
            using ValorantContext db = new ValorantContext();
            AgentArgs agentArgs = (AgentArgs)args;
            AgentType type = db.AgentType.Where(t => t.TypeId == agentArgs.TypeID).FirstOrDefault();
            Agents newAgent = new Agents()
            {
                AgentName = agentArgs.Name,
                AgentType = type,
                SignatureAbilityName = agentArgs.SignatureAbilityName,
                SignatureAbilityDiscription = agentArgs.SignatureAbilityDiscription,
                UltamateAbilityName = agentArgs.UltamateAbilityName,
                UltamateAbilityDiscription = agentArgs.UltamateAbilityDiscription,
                AbilityOneName = agentArgs.AbilityOneName,
                AbilityOneDiscription = agentArgs.AbilityOneDiscription,
                AbilityTwoName = agentArgs.AbilityTwoName,
                AbilityTwoDiscription = agentArgs.AbilityTwoDiscription,
                Bio = agentArgs.Bio
            };

            db.Agents.Add(newAgent);
            db.SaveChanges();
        }

        public object GetAgentTypeObj(object selectedAgent)
        {
            using ValorantContext db = new ValorantContext();
            Agents agent = (Agents)selectedAgent;
            return db.Agents.Where(a => a.AgentId == agent.AgentId).Include(a => a.AgentType).Select(a => a.AgentType).FirstOrDefault();
        }

        public List<string> GetAgentsAbilities(object selectedItem)
        {
            using ValorantContext db = new ValorantContext();
            Agents agent = (Agents)selectedItem;

            return new List<string>
            {
                db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.SignatureAbilityName).FirstOrDefault(),
                db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.UltamateAbilityName).FirstOrDefault(),
                db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.AbilityOneName).FirstOrDefault(),
                db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.AbilityTwoName).FirstOrDefault()
            };
        }

        public string GetAbilityDiscription(object selectedAgent, object selectedItem)
        {
            string abilityName = (string)selectedItem;
            Agents agent = (Agents)selectedAgent;
            using ValorantContext db = new ValorantContext();

            if (agent.SignatureAbilityName == abilityName)
                return GetAgentDataStr(selectedAgent, Fields.SignatureAbilityDiscription);
            if (agent.UltamateAbilityName == abilityName)
                return GetAgentDataStr(selectedAgent, Fields.UltamateAbilityDiscription);
            if (agent.AbilityOneName == abilityName)
                return GetAgentDataStr(selectedAgent, Fields.AbilityOneDiscription);
            if (agent.AbilityTwoName == abilityName)
                return GetAgentDataStr(selectedAgent, Fields.AbilityTwoDiscription);
            return "";
        }

        public override void UpdateEntry(object selectedAgent, SuperArgs args)
        {
            using ValorantContext db = new ValorantContext();
            AgentArgs agentArgs = (AgentArgs)args;
            Agents agentToUpdate = db.Agents
                .Where(a => a.AgentId == ((Agents)selectedAgent).AgentId)
                .FirstOrDefault();

            if (agentToUpdate != null)
            {
                agentToUpdate.AgentName = agentArgs.Name;
                agentToUpdate.AgentTypeId = agentArgs.TypeID;
                agentToUpdate.SignatureAbilityName = agentArgs.SignatureAbilityName;
                agentToUpdate.SignatureAbilityDiscription = agentArgs.SignatureAbilityDiscription;
                agentToUpdate.UltamateAbilityName = agentArgs.UltamateAbilityName;
                agentToUpdate.UltamateAbilityDiscription = agentArgs.UltamateAbilityDiscription;
                agentToUpdate.AbilityOneName = agentArgs.AbilityOneName;
                agentToUpdate.AbilityOneDiscription = agentArgs.AbilityOneDiscription;
                agentToUpdate.AbilityTwoName = agentArgs.AbilityTwoName;
                agentToUpdate.AbilityTwoDiscription = agentArgs.AbilityTwoDiscription;
                agentToUpdate.Bio = agentArgs.Bio;

                db.SaveChanges();
            }
        }

        public string GetAgentDataStr(object selectedAgent, Fields field)
        {
            using ValorantContext db = new ValorantContext();
            Agents agent = (Agents)selectedAgent;
            IQueryable<Agents> agentQuery = db.Agents.Where(a => a.AgentId == agent.AgentId);
            switch (field)
            {
                case Fields.ID:
                    return agentQuery.Select(a => a.AgentId).ToString();
                case Fields.Name:
                    return agentQuery.Select(a => a.AgentName).FirstOrDefault();
                case Fields.Type:
                    return agentQuery.Include(a=>a.AgentType).Select(a => a.AgentType).FirstOrDefault().ToString();
                case Fields.SignatureAbilityName:
                    return agentQuery.Select(a => a.SignatureAbilityName).FirstOrDefault();
                case Fields.SignatureAbilityDiscription:
                    return agentQuery.Select(a => a.SignatureAbilityDiscription).FirstOrDefault();
                case Fields.UltamateAbilityName:
                    return agentQuery.Select(a => a.UltamateAbilityName).FirstOrDefault();
                case Fields.UltamateAbilityDiscription:
                    return agentQuery.Select(a => a.UltamateAbilityDiscription).FirstOrDefault();
                case Fields.AbilityOneName:
                    return agentQuery.Select(a => a.AbilityOneName).FirstOrDefault();
                case Fields.AbilityOneDiscription:
                    return agentQuery.Select(a => a.AbilityOneDiscription).FirstOrDefault();
                case Fields.AbilityTwoName:
                    return agentQuery.Select(a => a.AbilityTwoName).FirstOrDefault();
                case Fields.AbilityTwoDiscription:
                    return agentQuery.Select(a => a.AbilityTwoDiscription).FirstOrDefault();
                case Fields.Bio:
                    return agentQuery.Select(a => a.Bio).FirstOrDefault();
                case Fields.ImagePath:
                    return agentQuery.Select(a => a.ImagePath).FirstOrDefault();
                default:
                    return "";
            }
        }
    }
}