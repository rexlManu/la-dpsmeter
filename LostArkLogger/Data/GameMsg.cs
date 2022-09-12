using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public class GameMsg
    {
        public static Dictionary<String, String> Items = (Dictionary<String, String>) ObjectSerialize.Deserialize(
            LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam
                ? Configuration.ReadXorBinary("GameMsg_English.bin")
                : Configuration.ReadXorBinary("GameMsg.bin"));    }
}
