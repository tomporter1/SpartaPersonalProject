namespace BussinessLayer.Args
{
    public class AgentTypeArgs : SuperArgs
    {
        public string Name { get; private set; }

        public AgentTypeArgs(string name)
        {
            Name = name;
        }
    }
}