using System;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace BussinessLayer
{
    public class AgentManager
    {
        public enum Fields
        {
            ID,
            Name,
            Type,
            SignatureAbilityName,
            SignatureAbilityDiscription,
            UltamateAbilityName,
            UltamateAbilityDiscription,
            AbilityOneName,
            AbilityOneDiscription,
            AbilityTwoName,
            AbilityTwoDiscription,
            Bio
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

        public void RemoveAgent(object selectedAgent)
        {
            using ValorantContext db = new ValorantContext();
            Agents agentToRemove = (Agents)selectedAgent;
            db.Agents.Remove(agentToRemove);
            db.SaveChanges();
        }

        public void UpdateAgent(int id, AgentManagerArgs args)
        {
            using ValorantContext db = new ValorantContext();
            Agents agentToUpdate = db.Agents.Where(a => a.AgentId == id).FirstOrDefault();
            if(agentToUpdate!= null)
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
                    return agent.AgentId.ToString();
                case Fields.Name:
                    return agent.AgentName;
                case Fields.Type:
                    return agent.AgentTypeId.ToString();
                case Fields.SignatureAbilityName:
                    return agent.SignatureAbilityName;
                case Fields.SignatureAbilityDiscription:
                    return agent.SignatureAbilityDiscription;
                case Fields.UltamateAbilityName:
                    return agent.UltamateAbilityName;
                case Fields.UltamateAbilityDiscription:
                    return agent.UltamateAbilityDiscription;
                case Fields.AbilityOneName:
                    return agent.AbilityOneName;
                case Fields.AbilityOneDiscription:
                    return agent.AbilityOneDiscription;
                case Fields.AbilityTwoName:
                    return agent.AbilityTwoName;
                case Fields.AbilityTwoDiscription:
                    return agent.AbilityTwoDiscription;
                case Fields.Bio:
                    return agent.Bio;
                default:
                    return "";
            }
        }
    }
}