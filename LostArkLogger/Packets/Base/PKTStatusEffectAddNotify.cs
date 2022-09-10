using System;
using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTStatusEffectAddNotify
    {
        public PKTStatusEffectAddNotify(BitReader reader)
        {
            if (Configuration.Region == Region.Steam) SteamDecode(reader);
            if (Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public UInt64 ObjectId;
        public Byte New;
        public StatusEffectData statusEffectData;
        public UInt64 u64_0;
        public UInt64 u64_1;
        public Byte b_0;
    }
}
