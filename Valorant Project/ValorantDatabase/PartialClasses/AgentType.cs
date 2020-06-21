using System;
using System.Collections.Generic;

namespace ValorantDatabase
{
    public partial class AgentType : IEquatable<AgentType>
    {
        private int _typeId;
        private string _typeName;
        private string _imagePath;

        public override bool Equals(object obj)
        {
            return Equals(obj as AgentType);
        }

        public bool Equals(AgentType other)
        {
            return other != null &&
                   TypeId == other.TypeId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TypeId);
        }

        public override string ToString() => TypeName;

        public static bool operator ==(AgentType left, AgentType right)
        {
            return EqualityComparer<AgentType>.Default.Equals(left, right);
        }

        public static bool operator !=(AgentType left, AgentType right)
        {
            return !(left == right);
        }
    }
}
