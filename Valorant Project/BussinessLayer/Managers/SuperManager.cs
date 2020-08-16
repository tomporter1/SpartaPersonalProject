using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using System.Collections.Generic;

namespace BussinessLayer.Managers
{
    public abstract class SuperManager: IBasicManager
    {
        public abstract List<object> GetAllEntries();

        public abstract void RemoveEntry(object selectedMap);

        public abstract void AddNewEntry(SuperArgs args);

        public abstract void UpdateEntry(object selectedEntry, SuperArgs args);
    }
}