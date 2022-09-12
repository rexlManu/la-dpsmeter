using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTSkillStageNotify
    {
        public PKTSkillStageNotify(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public UInt64 SourceId;
        public UInt32 SkillId;
        public Byte Stage;
        public Byte[] bytearray_0;
        public Byte[] bytearray_1;
        public Byte[] bytearray_2;
        public Byte[] bytearray_3;
    }
}
