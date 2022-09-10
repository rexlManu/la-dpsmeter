using System;
using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTNewNpc
    {
        public PKTNewNpc(BitReader reader)
        {
            if (Configuration.Region == Region.Steam) SteamDecode(reader);
            if (Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public NpcStruct npcStruct;
        public UInt64 u64_0;
        public Byte b_0;
        public Byte b_1;
        public Byte b_2;
        public Byte b_3;
    }
}
