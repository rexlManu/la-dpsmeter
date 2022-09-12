using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTSkillDamageNotify
    {
        public PKTSkillDamageNotify(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public UInt64 SourceId;
        public UInt32 SkillEffectId;
        public UInt32 SkillId;
        public List<SkillDamageEvent> skillDamageEvents;
        public Byte b_0;
    }
}
