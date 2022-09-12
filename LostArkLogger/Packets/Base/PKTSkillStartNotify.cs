using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTSkillStartNotify
    {
        public PKTSkillStartNotify(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public UInt64 SourceId;
        public UInt32 SkillId;
        public List<Object> packed_0;
        public UInt64 u64_0;
        public UInt64 u64_1;
        public UInt64 u64_2;
        public UInt32 u32_0;
        public UInt32 u32_1;
        public UInt16 u16_0;
        public UInt16 u16_1;
        public UInt16 u16_2;
        public Byte b_0;
        public Byte b_1;
        public Byte b_2;
        public Byte b_3;
    }
}
