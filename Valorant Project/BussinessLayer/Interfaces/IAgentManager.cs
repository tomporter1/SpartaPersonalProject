using BussinessLayer.Managers;
using System.Collections.Generic;

namespace BussinessLayer.Interfaces
{
    public interface IAgentManager : IBasicManager
    {
        string GetAgentDataStr(object selectedAgent, AgentManager.Fields field);
        List<string> GetAgentsAbilities(object selectedItem);
        string GetAbilityDiscription(object selectedAgent, object selectedItem);
        object GetAgentTypeObj(object selectedAgent);
    }
}