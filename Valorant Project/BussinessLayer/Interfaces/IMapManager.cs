namespace BussinessLayer.Interfaces
{
    public interface IMapManager : IBasicManager
    {
        public enum Fields
        {
            Name,
            ImagePath,
            LayoutImagePath
        }

        string GetMapsDataStr(object selectedMap, Fields field);
    }
}