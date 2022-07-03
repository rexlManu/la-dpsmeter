using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class StatusEffectData
    {
        public void SteamDecode(BitReader reader)
        {
			u32_0 = reader.ReadUInt32();
			InstanceId = reader.ReadUInt64();
			SourceId = reader.ReadUInt64();
			bytearraylist = reader.ReadList<byte[]>(7);
			b_0 = reader.ReadByte();
			u32_1 = reader.ReadUInt32();
			b_1 = reader.ReadByte();
			hasValue = reader.ReadByte();
			bool flag = hasValue == 1;
			if (flag)
			{
				Value = reader.ReadBytes(16);
			}
			s64 = reader.ReadSimpleInt();
			b_2 = reader.ReadByte();
			bool flag2 = b_2 == 1;
			if (flag2)
			{
				u64 = reader.ReadUInt64();
			}
			BuffId = reader.ReadUInt32();
		}
    }
}
