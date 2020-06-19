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

        public virtual void AddNewEntry(string name) { }

        public virtual void UpdateEntry(object selectedEntry, string newName) { }
    }
}
