using BussinessLayer.Managers;

namespace BussinessLayer.Interfaces
{
    public interface IRanksManger : IBasicManager
    {
        string GetRankDataStr(object selectedRank, RankManager.Fields field);

    }
}