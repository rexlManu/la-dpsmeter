using System;
using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class subPKTInitEnv8
    {
        public subPKTInitEnv8(BitReader reader)
        {
            if (Configuration.Region == Region.Steam) SteamDecode(reader);
            if (Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public List<List<UInt16>> u16list_0 = new List<List<UInt16>>();
        public List<List<UInt16>> u16list_1 = new List<List<UInt16>>();
        public List<List<UInt16>> u16list_2 = new List<List<UInt16>>();
        public UInt16 num;
    }
}
