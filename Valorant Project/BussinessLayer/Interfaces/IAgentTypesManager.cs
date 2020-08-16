using BussinessLayer.Managers;

namespace BussinessLayer.Interfaces
{
    public interface IAgentTypesManager : IBasicManager
    {
        string GetTypeDataStr(object selectedType, AgentTypeManager.Fields field);
    }
}