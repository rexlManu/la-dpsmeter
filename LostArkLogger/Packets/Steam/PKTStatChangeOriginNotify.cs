using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTStatChangeOriginNotify
    {
        public void SteamDecode(BitReader reader)
        {
            b_0 = reader.ReadByte();
            if (b_0 == 1)
                u32_0 = reader.ReadUInt32();
            StatPairList = reader.Read<StatPair>();
            ObjectId = reader.ReadUInt64();
            b_1 = reader.ReadByte();
            StatPairChangedList = reader.Read<StatPair>();
        }
    }
}
