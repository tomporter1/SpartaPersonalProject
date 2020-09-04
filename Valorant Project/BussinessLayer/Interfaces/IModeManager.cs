namespace BussinessLayer.Interfaces
{
    public interface IModeManager : IBasicManager
    {
        public enum Fields
        {
            Name,
            Discription
        }

        bool IsRanked(object selectedItem);
    }
}