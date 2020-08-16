using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IModeManager : IBasicManager
    {
        bool IsRanked(object selectedItem);
    }
}