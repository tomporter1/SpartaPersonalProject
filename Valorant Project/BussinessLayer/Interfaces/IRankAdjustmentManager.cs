
namespace BussinessLayer.Interfaces
{
    public interface IRankAdjustmentManager : IBasicManager
    {
        string GetRankAdjustmentDataStr(object selectedRankAdjustment, Managers.RankAdjustmentManager.Fields field);
    }
}