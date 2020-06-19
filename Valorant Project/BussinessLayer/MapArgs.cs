namespace BussinessLayer
{
    public class MapArgs : SuperArgs
    {
        public string Name { get; private set; }

        public MapArgs(string name)
        {
            Name = name;
        }
    }
}