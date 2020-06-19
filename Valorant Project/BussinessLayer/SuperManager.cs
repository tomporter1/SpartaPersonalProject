using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public abstract class SuperManager
    {
        public abstract List<object> GetAllEntries();

        public abstract void RemoveEntry(object selectedMap);
    }
}
