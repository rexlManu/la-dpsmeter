using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTSkillStartNotify
    {
        public void SteamDecode(BitReader reader)
        {
			u64_0 = reader.ReadUInt64();
			b_0 = reader.ReadByte();
			bool flag = b_0 == 1;
			if (flag)
			{
				u16_0 = reader.ReadUInt16();
			}
			b_2 = reader.ReadByte();
			bool flag2 = b_2 == 1;
			if (flag2)
			{
				u32_1 = reader.ReadUInt32();
			}
			u16_1 = reader.ReadUInt16();
			u64_2 = reader.ReadUInt64();
			packed = reader.ReadPackedValues(new int[] { 1, 1, 4, 4, 4, 3, 6 });
			SkillId = reader.ReadUInt32();
			u16_2 = reader.ReadUInt16();
			SourceId = reader.ReadUInt64();
			b_3 = reader.ReadByte();
			b_1 = reader.ReadByte();
			bool flag3 = b_1 == 1;
			if (flag3)
			{
				u32_0 = reader.ReadUInt32();
			}
			u64_1 = reader.ReadUInt64();
		}
    }
}
