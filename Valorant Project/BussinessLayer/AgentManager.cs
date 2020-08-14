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

        private ValorantContext _context;

        public AgentManager(ValorantContext context = null)
        {
            _context = context;
        }

        public override List<object> GetAllEntries()
        {
            ValorantContext db = (_context ?? new ValorantContext());
            List<object> output = db.Agents.OrderBy(a => a.AgentName).ToList<object>();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
            return output;
        }

        public override void RemoveEntry(object selectedAgent)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            Agents agentToRemove = (Agents)selectedAgent;
            db.Agents.Remove(agentToRemove);
            db.SaveChanges();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public override void AddNewEntry(SuperArgs args)
        {
            ValorantContext db = (_context ?? new ValorantContext());
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

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public object GetAgentTypeObj(object selectedAgent)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            Agents agent = (Agents)selectedAgent;
            object output = db.Agents.Where(a => a.AgentId == agent.AgentId).Include(a => a.AgentType).Select(a => a.AgentType).FirstOrDefault();

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public List<string> GetAgentsAbilities(object selectedItem)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            Agents agent = (Agents)selectedItem;

            List<string> output = new List<string>
            {
               db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.SignatureAbilityName).FirstOrDefault(),
               db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.UltamateAbilityName).FirstOrDefault(),
               db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.AbilityOneName).FirstOrDefault(),
               db.Agents.Where(a => a.AgentId == agent.AgentId).Select(a => a.AbilityTwoName).FirstOrDefault()
            };

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();

            return output;
        }

        public string GetAbilityDiscription(object selectedAgent, object selectedItem)
        {
            string abilityName = (string)selectedItem;
            Agents agent = (Agents)selectedAgent;

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
            ValorantContext db = (_context ?? new ValorantContext());
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

            //Disposes of the db context if it is not running off a set context
            if (_context == null)
                db.Dispose();
        }

        public string GetAgentDataStr(object selectedAgent, Fields field)
        {
            ValorantContext db = (_context ?? new ValorantContext());
            Agents agent = (Agents)selectedAgent;
            IQueryable<Agents> agentQuery = db.Agents.Where(a => a.AgentId == agent.AgentId);
            string output = "";
            switch (field)
            {
                case Fields.ID:
                    output = agentQuery.Select(a => a.AgentId).ToString();
                    break;
                case Fields.Name:
                    output = agentQuery.Select(a => a.AgentName).FirstOrDefault();
                    break;
                case Fields.Type:
                    output = agentQuery.Include(a => a.AgentType).Select(a => a.AgentType).FirstOrDefault().ToString();
                    break;
                case Fields.SignatureAbilityName:
                    output = agentQuery.Select(a => a.SignatureAbilityName).FirstOrDefault();
                    break;
                case Fields.SignatureAbilityDiscription:
                    output = agentQuery.Select(a => a.SignatureAbilityDiscription).FirstOrDefault();
                    break;
                case Fields.UltamateAbilityName:
                    output = agentQuery.Select(a => a.UltamateAbilityName).FirstOrDefault();
                    break;
                case Fields.UltamateAbilityDiscription:
                    output = agentQuery.Select(a => a.UltamateAbilityDiscription).FirstOrDefault();
                    break;
                case Fields.AbilityOneName:
                    output = agentQuery.Select(a => a.AbilityOneName).FirstOrDefault();
                    break;
                case Fields.AbilityOneDiscription:
                    output = agentQuery.Select(a => a.AbilityOneDiscription).FirstOrDefault();
                    break;
                case Fields.AbilityTwoName:
                    output = agentQuery.Select(a => a.AbilityTwoName).FirstOrDefault();
                    break;
                case Fields.AbilityTwoDiscription:
                    output = agentQuery.Select(a => a.AbilityTwoDiscription).FirstOrDefault();
                    break;
                case Fields.Bio:
                    output = agentQuery.Select(a => a.Bio).FirstOrDefault();
                    break;
                case Fields.ImagePath:
                    output = agentQuery.Select(a => a.ImagePath).FirstOrDefault();
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