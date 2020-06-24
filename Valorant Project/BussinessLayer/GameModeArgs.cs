using System;

namespace BussinessLayer
{
    public class GameModeArgs : SuperArgs
    {
        public string Name { get; private set; }
        public string Discription { get; private set; }

        public GameModeArgs(string name, string discription)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Discription = discription;
        }
    }
}