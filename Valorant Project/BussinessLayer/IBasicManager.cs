using System.Collections.Generic;

namespace BussinessLayer
{
    interface IBasicManager
    {
        List<object> GetAllEntries();
        void RemoveEntry(object selectedMap);

         void AddNewEntry(SuperArgs args);

         void UpdateEntry(object selectedEntry, SuperArgs args);
    }
}