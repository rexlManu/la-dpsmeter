using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTPartyStatusEffectAddNotify
    {
        public void SteamDecode(BitReader reader)
        {
            PlayerIdOnRefresh = reader.ReadUInt64();
            PartyId = reader.ReadUInt64();
            statusEffectDatas = reader.ReadList<StatusEffectData>();
            b_0 = reader.ReadByte();
            u64_0 = reader.ReadUInt64();
        }
    }
}
