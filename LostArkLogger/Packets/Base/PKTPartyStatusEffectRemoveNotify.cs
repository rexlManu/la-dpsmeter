using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTPartyStatusEffectRemoveNotify
    {
        public PKTPartyStatusEffectRemoveNotify(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public List<UInt32> StatusEffectIds;
        public UInt64 PartyId;
        public UInt64 u64_0;
        public Byte b_0;
    }
}
