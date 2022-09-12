using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTNewNpc
    {
        public PKTNewNpc(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public NpcStruct npcStruct;
        public UInt64 u64_0;
        public Byte b_0;
        public Byte b_1;
        public Byte b_2;
        public Byte b_3;
    }
}
