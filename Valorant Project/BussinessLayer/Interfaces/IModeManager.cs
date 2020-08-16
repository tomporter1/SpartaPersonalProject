namespace BussinessLayer.Interfaces
{
    public interface IModeManager : IBasicManager
    {
        bool IsRanked(object selectedItem);
    }
}