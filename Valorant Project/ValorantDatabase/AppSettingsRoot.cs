namespace ValorantDatabase
{
    public class AppSettingsRoot
    {
        public Connectionstrings ConnectionStrings { get; set; }
    }

    public class Connectionstrings
    {
        public string ValorantDbsConnection { get; set; }
    }
}