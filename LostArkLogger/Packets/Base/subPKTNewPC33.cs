using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class subPKTNewPC33
    {
        public subPKTNewPC33(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public Byte[] bytearray_0;
        public Byte[] bytearray_1;
        public UInt32 u32_0;
        public UInt32 u32_1;
        public Byte b_0;
    }
}
