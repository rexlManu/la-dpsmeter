using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTInitPC
    {
        public void SteamDecode(BitReader reader)
        {
			b_0 = reader.ReadByte();
			b_1 = reader.ReadByte();
			b_7 = reader.ReadByte();
			b_12 = reader.ReadByte();
			ClassId = reader.ReadUInt16();
			b_18 = reader.ReadByte();
			u64_1 = reader.ReadUInt64();
			u32_11 = reader.ReadUInt32();
			Name = reader.ReadString();
			u64_2 = reader.ReadUInt64();
			b_2 = reader.ReadByte();
			b_3 = reader.ReadByte();
			b_4 = reader.ReadByte();
			u32_0 = reader.ReadUInt32();
			u16_0 = reader.ReadUInt16();
			u16_1 = reader.ReadUInt16();
			u16_2 = reader.ReadUInt16();
			b_5 = reader.ReadByte();
			b_6 = reader.ReadByte();
			u32_1 = reader.ReadUInt32();
			u32_2 = reader.ReadUInt32();
			statPair = reader.Read<StatPair>(0);
			GearLevel = reader.ReadUInt32();
			subPKTInitPC29s = reader.ReadList<subPKTInitPC29>(0);
			u32_3 = reader.ReadUInt32();
			b_8 = reader.ReadByte();
			b_9 = reader.ReadByte();
			b_10 = reader.ReadByte();
			PlayerId = reader.ReadUInt64();
			b_11 = reader.ReadByte();
			bool flag = b_11 == 1;
			if (flag)
			{
				u32_4 = reader.ReadUInt32();
			}
			b_13 = reader.ReadByte();
			u32_5 = reader.ReadUInt32();
			u16_3 = reader.ReadUInt16();
			bytearray_2 = reader.ReadBytes(35);
			u64_0 = reader.ReadUInt64();
			b_14 = reader.ReadByte();
			blist = reader.ReadList<byte>(0);
			bytearray_3 = reader.ReadBytes(25);
			u32_6 = reader.ReadUInt32();
			u32_7 = reader.ReadUInt32();
			b_15 = reader.ReadByte();
			b_16 = reader.ReadByte();
			bytearraylist = reader.ReadList<byte[]>(30);
			u16list = reader.ReadList<ushort>(0);
			b_17 = reader.ReadByte();
			u16_4 = reader.ReadUInt16();
			statusEffectDatas = reader.ReadList<StatusEffectData>(0);
			u32_8 = reader.ReadUInt32();
			u32_9 = reader.ReadUInt32();
			u32_10 = reader.ReadUInt32();
			b_19 = reader.ReadByte();
			bytearray_1 = reader.ReadBytes(50);
			Level = reader.ReadUInt16();
			bytearray_0 = reader.ReadBytes(60);
		}
    }
}
