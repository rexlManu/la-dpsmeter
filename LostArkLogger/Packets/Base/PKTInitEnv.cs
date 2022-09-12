using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTInitEnv
    {
        public PKTInitEnv(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public UInt64 PlayerId;
        public UInt64 s64_0;
        public List<UInt16> u16list_0;
        public subPKTInitEnv8 subPKTInitEnv8;
        public UInt64 s64_1;
        public UInt32 u32_0;
        public UInt32 u32_1;
        public Byte b_0;
    }
}
