namespace BussinessLayer.Interfaces
{
    public interface IRanksManager : IBasicManager
    {
        public enum Fields
        {
            Name,
            ImagePath
        }

        string GetRankDataStr(object selectedRank, Fields field);
    }
}