using BussinessLayer.Managers;

namespace BussinessLayer.Interfaces
{
    public interface IMapManager : IBasicManager
    {
        string GetMapsDataStr(object selectedMap, MapManager.Fields field);
    }
}