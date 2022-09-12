using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class subPKTInitPC29
    {
        public subPKTInitPC29(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public Int64 p64_0;
        public Int64 p64_1;
        public UInt64 u64_0;
        public UInt16 u16_0;
        public Byte b_0;
        public Byte b_1;
        public Byte b_2;
    }
}
