using System;
using System.Collections.Generic;
using System.Text;

namespace ValorantDatabase
{
    public partial class Agents : IEquatable<Agents>
    {
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
