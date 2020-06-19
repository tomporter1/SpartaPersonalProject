using System;
using System.Collections.Generic;

namespace ValorantDatabase
{
    public partial class AgentType
    {
        public AgentType()
        {
            Agents = new HashSet<Agents>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<Agents> Agents { get; set; }
    }
}
