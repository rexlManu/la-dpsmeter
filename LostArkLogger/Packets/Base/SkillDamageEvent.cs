using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class SkillDamageEvent
    {
        public SkillDamageEvent(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public Int64 CurHp;
        public Int64 Damage;
        public Int64 MaxHp;
        public UInt64 TargetId;
        public Byte Modifier;
        public UInt16 u16_0;
        public Byte b_0;
        public Byte b_1;
        public Byte b_2;
    }
}
