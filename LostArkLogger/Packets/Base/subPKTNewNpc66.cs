using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class subPKTNewNpc66
    {
        public subPKTNewNpc66(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public String str_0;
        public List<ItemInfo> itemInfos;
        public subPKTNewNpc5 subPKTNewNpc5;
        public UInt64 u64_0;
        public UInt16 u16_0;
        public Byte b_0;
        public Byte b_1;
        public Byte b_2;
    }
}
