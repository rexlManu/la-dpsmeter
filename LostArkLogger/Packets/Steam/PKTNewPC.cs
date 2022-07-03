using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTNewPC
    {
        public void SteamDecode(BitReader reader)
        {
			b_0 = reader.ReadByte();
			b_1 = reader.ReadByte();
			bool flag = b_1 == 1;
			if (flag)
			{
				subPKTNewPC33 = reader.Read<subPKTNewPC33>(0);
			}
			pCStruct = reader.Read<PCStruct>(0);
			b_2 = reader.ReadByte();
			bool flag2 = b_2 == 1;
			if (flag2)
			{
				bytearray_0 = reader.ReadBytes(20);
			}
			b_3 = reader.ReadByte();
			b_4 = reader.ReadByte();
			bool flag3 = b_4 == 1;
			if (flag3)
			{
				bytearray_1 = reader.ReadBytes(12);
			}
			b_5 = reader.ReadByte();
			bool flag4 = b_5 == 1;
			if (flag4)
			{
				u32 = reader.ReadUInt32();
			}
		}
    }
}
