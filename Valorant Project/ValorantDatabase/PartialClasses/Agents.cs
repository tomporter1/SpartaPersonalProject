using System;
using System.Collections.Generic;

namespace ValorantDatabase
{
    public partial class Agents : IEquatable<Agents>
    {
        private int _agentId;
        private string _agentName;
        private int _agentTypeId;
        private string _signatureAbilityName;
        private string _signatureAbilityDiscription;
        private string _ultamateAbilityName;
        private string _ultamateAbilityDiscription;
        private string _abilityOneName;
        private string _abilityOneDiscription;
        private string _abilityTwoName;
        private string _abilityTwoDiscription;
        private string _bio;
        private string _imagePath;

        public override bool Equals(object obj)
        {
            return Equals(obj as Agents);
        }

        public bool Equals(Agents other)
        {
            return other != null &&
                   _agentId == other._agentId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_agentId);
        }

        public override string ToString() => AgentName;

        public static bool operator ==(Agents left, Agents right)
        {
            return EqualityComparer<Agents>.Default.Equals(left, right);
        }

        public static bool operator !=(Agents left, Agents right)
        {
            return !(left == right);
        }
    }
}
