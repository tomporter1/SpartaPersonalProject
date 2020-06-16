using System;
using System.Collections.Generic;
using System.Text;

namespace ValorantDatabase
{
    public partial class Abilities
    {
        public Abilities()
        {
            Agents = new HashSet<Agents>();
        }

        public int AbilitytId { get; set; }
        public string AbilityName { get; set; }
        public string AbilityDiscription { get; set; }
        public int Cost { get; set; }
        public int MaxCharges { get; set; }
        public virtual ICollection<Agents> Agents { get; set; }

    }
}