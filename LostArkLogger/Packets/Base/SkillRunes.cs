using System;
using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class SkillRunes
    {
        public SkillRunes(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public List<List<UInt32>> u32list_0 = new List<List<UInt32>>();
        public List<UInt32> u32_0 = new List<UInt32>();
        public UInt16 num;
    }
}
