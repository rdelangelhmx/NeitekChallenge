namespace Server.Classes;

public partial class ConfigApp
{
    public CfgApplication Application { get; set; }
    public CfgDataBase DataBase { get; set; }

    public class CfgApplication
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string WebPage { get; set; }
        public string ApiKey { get; set; }
        public string Scheme { get; set; }
        public string DataBase { get; set; }
    }

    public class CfgDataBase
    {
        public string Server { get; set; }
        public string Name { get; set; }
        public string Uid { get; set; }
        public string Pwd { get; set; }
        public string Conn
        {
            get
            {
                return $"server={Server};uid={Uid};password={EncryptUtils.From64(Pwd)};database={Name};Connect Timeout=30;";
            }
        }
    }
}
