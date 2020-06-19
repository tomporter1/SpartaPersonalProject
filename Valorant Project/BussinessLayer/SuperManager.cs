using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public abstract class SuperManager
    {
        public abstract List<object> GetAllEntries();

        public abstract void RemoveEntry(object selectedMap);

        public abstract void AddNewEntry(SuperArgs args);

        public abstract void UpdateEntry(object selectedEntry, SuperArgs args);
    }
}
