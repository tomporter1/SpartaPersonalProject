using System.Collections.Generic;

namespace ValorantDatabase
{
    public partial class AgentType
    {
        public AgentType()
        {
            Agents = new HashSet<Agents>();
        }

        public int TypeId
        {
            get => _typeId;
            set => _typeId = value;
        }
        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value.Length <= 20 ? value : value.Substring(0, 20); }
        }
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value.Length <= 35 ? value : value.Substring(0, 35); }
        }

        public virtual ICollection<Agents> Agents { get; set; }
    }
}
