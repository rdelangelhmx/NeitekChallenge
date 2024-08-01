namespace Client.Classes;

public class ConfigApp
{
    public CfgApi Api { get; set; }

    public class CfgApi
    {
        public string Client { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
    }
}
