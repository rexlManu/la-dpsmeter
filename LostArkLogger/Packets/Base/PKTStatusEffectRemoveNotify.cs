using System;
using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTStatusEffectRemoveNotify
    {
        public PKTStatusEffectRemoveNotify(BitReader reader)
        {
            if (Configuration.Region == Region.Steam) SteamDecode(reader);
            if (Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public List<UInt32> InstanceIds;
        public UInt64 ObjectId;
        public Byte Reason;
    }
}
