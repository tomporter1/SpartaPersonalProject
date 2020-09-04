namespace BussinessLayer.Interfaces
{
    public interface IRankAdjustmentManager : IBasicManager
    {
        public enum Fields
        {
            Name,
            ImagePath
        }

        string GetRankAdjustmentDataStr(object selectedRankAdjustment, Fields field);
    }
}