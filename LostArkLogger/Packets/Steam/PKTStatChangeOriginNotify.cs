using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTStatChangeOriginNotify
    {
        public void SteamDecode(BitReader reader)
        {
			ObjectId = reader.ReadUInt64();
			b_0 = reader.ReadByte();
			bool flag = b_0 == 1;
			if (flag)
			{
				u32 = reader.ReadUInt32();
			}
			StatPairChangedList = reader.Read<StatPair>(0);
			b_1 = reader.ReadByte();
			StatPairList = reader.Read<StatPair>(0);
		}
    }
}
