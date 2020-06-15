using System;
using System.Collections.Generic;

namespace ValorantDatabase
{
    public partial class Agents
    {
        public Agents()
        {
            GameLogs = new HashSet<GameLogs>();
        }

        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public int AgentTypeId { get; set; }
        public string SignatureAbilityName { get; set; }
        public string SignatureAbilityDiscription { get; set; }
        public string UltamateAbilityName { get; set; }
        public string UltamateAbilityDiscription { get; set; }
        public string AbilityOneName { get; set; }
        public string AbilityOneDiscription { get; set; }
        public string AbilityTwoName { get; set; }
        public string AbilityTwoDiscription { get; set; }
        public string Bio { get; set; }

        public virtual AgentType AgentType { get; set; }
        public virtual ICollection<GameLogs> GameLogs { get; set; }
    }
}
