using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTDeathNotify
    {
        public void SteamDecode(BitReader reader)
        {
			b_0 = reader.ReadByte();
			bool flag = b_0 == 1;
			if (flag)
			{
				b_1 = reader.ReadByte();
			}
			b_2 = reader.ReadByte();
			u64 = reader.ReadUInt64();
			b_3 = reader.ReadByte();
			bool flag2 = b_3 == 1;
			if (flag2)
			{
				b_4 = reader.ReadByte();
			}
			SourceId = reader.ReadUInt64();
			TargetId = reader.ReadUInt64();
			b_5 = reader.ReadByte();
			bool flag3 = b_5 == 1;
			if (flag3)
			{
				b_6 = reader.ReadByte();
			}
			u32 = reader.ReadUInt32();
			u16 = reader.ReadUInt16();
		}
    }
}
