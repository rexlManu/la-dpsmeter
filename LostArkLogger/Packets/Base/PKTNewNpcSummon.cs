using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTNewNpcSummon
    {
        public PKTNewNpcSummon(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public UInt64 OwnerId;
        public NpcStruct npcStruct;
        public Byte[] bytearray_0;
        public Byte[] bytearray_1;
        public Byte b_0;
    }
}
