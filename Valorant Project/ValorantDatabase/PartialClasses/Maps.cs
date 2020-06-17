using System;
using System.Collections.Generic;
using System.Text;

namespace ValorantDatabase
{
    public partial class Maps : IEquatable<Maps>
    {
        public override bool Equals(object obj)
        {
            return Equals(obj as Maps);
        }

        public bool Equals(Maps other)
        {
            return other != null &&
                   MapId == other.MapId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MapId);
        }

        public override string ToString() => MapName;

        public static bool operator ==(Maps left, Maps right)
        {
            return EqualityComparer<Maps>.Default.Equals(left, right);
        }

        public static bool operator !=(Maps left, Maps right)
        {
            return !(left == right);
        }
    }
}
