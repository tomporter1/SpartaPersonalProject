using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class AgentManager
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
            Type
        }

        public List<Agents> GetAllAgents()
        {
            using ValorantContext db = new ValorantContext();
            return db.Agents.ToList();
        }

        public void AddNewAgent(AgentManagerArgs args)
        {
            using ValorantContext db = new ValorantContext();
            AgentType type = db.AgentType.Where(t => t.TypeId == args.TypeID).FirstOrDefault();
            Agents newAgent = new Agents()
            {
                AgentName = args.Name,
                AgentType = type,
                SignatureAbilityName = args.SignatureAbilityName,
                SignatureAbilityDiscription = args.SignatureAbilityDiscription,
                UltamateAbilityName = args.UltamateAbilityName,
                UltamateAbilityDiscription = args.UltamateAbilityDiscription,
                AbilityOneName = args.AbilityOneName,
                AbilityOneDiscription = args.AbilityOneDiscription,
                AbilityTwoName = args.AbilityTwoName,
                AbilityTwoDiscription = args.AbilityTwoDiscription,
                Bio = args.Bio
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
                return GetAgentData(selectedAgent, Fields.SignatureAbilityDiscription);
            if (agent.UltamateAbilityName == abilityName)
                return GetAgentData(selectedAgent, Fields.UltamateAbilityDiscription);
            if (agent.AbilityOneName == abilityName)
                return GetAgentData(selectedAgent, Fields.AbilityOneDiscription);
            if (agent.AbilityTwoName == abilityName)
                return GetAgentData(selectedAgent, Fields.AbilityTwoDiscription);
            return "";
        }

        public void RemoveAgent(object selectedAgent)
        {
            using ValorantContext db = new ValorantContext();
            Agents agentToRemove = (Agents)selectedAgent;
            db.Agents.Remove(agentToRemove);
            db.SaveChanges();
        }

        public void UpdateAgent(object selectedAgent, AgentManagerArgs args)
        {
            using ValorantContext db = new ValorantContext();

            Agents agentToUpdate = db.Agents
                .Where(a => a.AgentId == ((Agents)selectedAgent).AgentId)
                .FirstOrDefault();

            if (agentToUpdate != null)
            {
                agentToUpdate.AgentName = args.Name;
                agentToUpdate.AgentTypeId = args.TypeID;
                agentToUpdate.SignatureAbilityName = args.SignatureAbilityName;
                agentToUpdate.SignatureAbilityDiscription = args.SignatureAbilityDiscription;
                agentToUpdate.UltamateAbilityName = args.UltamateAbilityName;
                agentToUpdate.UltamateAbilityDiscription = args.UltamateAbilityDiscription;
                agentToUpdate.AbilityOneName = args.AbilityOneName;
                agentToUpdate.AbilityOneDiscription = args.AbilityOneDiscription;
                agentToUpdate.AbilityTwoName = args.AbilityTwoName;
                agentToUpdate.AbilityTwoDiscription = args.AbilityTwoDiscription;
                agentToUpdate.Bio = args.Bio;

                db.SaveChanges();
            }
        }

        public string GetAgentData(object selectedAgent, Fields field)
        {
            using ValorantContext db = new ValorantContext();
            Agents agent = (Agents)selectedAgent;
            switch (field)
            {
                case Fields.ID:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.AgentId).ToString();
                case Fields.Name:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.AgentName).FirstOrDefault();
                case Fields.Type:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.AgentTypeId).FirstOrDefault().ToString();
                case Fields.SignatureAbilityName:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.SignatureAbilityName).FirstOrDefault();
                case Fields.SignatureAbilityDiscription:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.SignatureAbilityDiscription).FirstOrDefault();
                case Fields.UltamateAbilityName:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.UltamateAbilityName).FirstOrDefault();
                case Fields.UltamateAbilityDiscription:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.UltamateAbilityDiscription).FirstOrDefault();
                case Fields.AbilityOneName:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.AbilityOneName).FirstOrDefault();
                case Fields.AbilityOneDiscription:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.AbilityOneDiscription).FirstOrDefault();
                case Fields.AbilityTwoName:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.AbilityTwoName).FirstOrDefault();
                case Fields.AbilityTwoDiscription:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.AbilityTwoDiscription).FirstOrDefault();
                case Fields.Bio:
                    return db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.Bio).FirstOrDefault();
                default:
                    return "";
            }
        }
    }
}