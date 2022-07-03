using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTNewNpc
    {
        public void SteamDecode(BitReader reader)
        {
			b_0 = reader.ReadByte();
			bool flag = b_0 == 1;
			if (flag)
			{
				u64 = reader.ReadUInt64();
			}
			b_1 = reader.ReadByte();
			bool flag2 = b_1 == 1;
			if (flag2)
			{
				b_2 = reader.ReadByte();
			}
			b_3 = reader.ReadByte();
			npcStruct = reader.Read<NpcStruct>(0);
		}
    }
}
