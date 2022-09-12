using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class SkillDamageMoveEvent
    {
        public SkillDamageMoveEvent(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public SkillDamageEvent skillDamageEvent;
        public UInt64 flag_0;
        public UInt64 flag_1;
        public UInt64 flag_2;
        public UInt64 flag_3;
        public UInt16 u16_0;
        public UInt16 u16_1;
        public UInt16 u16_2;
        public Byte b_0;
    }
}
