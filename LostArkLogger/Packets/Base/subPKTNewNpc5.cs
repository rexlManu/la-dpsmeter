using System;
using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class subPKTNewNpc5
    {
        public subPKTNewNpc5(BitReader reader)
        {
            if (Configuration.Region == Region.Steam) SteamDecode(reader);
            if (Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public UInt32 num;
        public List<Byte> b_0 = new List<Byte>();
    }
}
