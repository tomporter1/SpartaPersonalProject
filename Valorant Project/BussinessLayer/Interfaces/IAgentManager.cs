using System.Collections.Generic;

namespace BussinessLayer.Interfaces
{
    public interface IAgentManager : IBasicManager
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

        string GetAgentDataStr(object selectedAgent, Fields field);

        List<string> GetAgentsAbilities(object selectedItem);

        string GetAbilityDiscription(object selectedAgent, object selectedItem);

        object GetAgentTypeObj(object selectedAgent);
    }
}