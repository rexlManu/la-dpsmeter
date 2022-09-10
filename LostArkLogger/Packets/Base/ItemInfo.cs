using System;
using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class ItemInfo
    {
        public ItemInfo(BitReader reader)
        {
            if (Configuration.Region == Region.Steam) SteamDecode(reader);
            if (Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public UInt16 Level;
        public UInt64 s64_0;
        public List<Byte[]> bytearraylist_0;
        public UInt32 u32_0;
        public UInt16 u16_0;
        public Byte b_0;
        public Byte b_1;
    }
}
