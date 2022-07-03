using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class NpcStruct
    {
        public void SteamDecode(BitReader reader)
        {
			b_0 = reader.ReadByte();
			bool flag = b_0 == 1;
			if (flag)
			{
				bytearraylist = reader.ReadList<byte[]>(12);
			}
			b_1 = reader.ReadByte();
			b_10 = reader.ReadByte();
			b_22 = reader.ReadByte();
			bool flag2 = b_22 == 1;
			if (flag2)
			{
				b_23 = reader.ReadByte();
			}
			statPair = reader.Read<StatPair>(0);
			b_25 = reader.ReadByte();
			bool flag3 = b_25 == 1;
			if (flag3)
			{
				u16_3 = reader.ReadUInt16();
			}
			b_26 = reader.ReadByte();
			bool flag4 = b_26 == 1;
			if (flag4)
			{
				u32_3 = reader.ReadUInt32();
			}
			b_27 = reader.ReadByte();
			bool flag5 = b_27 == 1;
			if (flag5)
			{
				u32_4 = reader.ReadUInt32();
			}
			b_28 = reader.ReadByte();
			bool flag6 = b_28 == 1;
			if (flag6)
			{
				b_29 = reader.ReadByte();
			}
			b_30 = reader.ReadByte();
			bool flag7 = b_30 == 1;
			if (flag7)
			{
				u32_5 = reader.ReadUInt32();
			}
			u64_0 = reader.ReadUInt64();
			u16_0 = reader.ReadUInt16();
			u16_1 = reader.ReadUInt16();
			b_2 = reader.ReadByte();
			bool flag8 = b_2 == 1;
			if (flag8)
			{
				b_3 = reader.ReadByte();
			}
			b_4 = reader.ReadByte();
			bool flag9 = b_4 == 1;
			if (flag9)
			{
				b_5 = reader.ReadByte();
			}
			b_6 = reader.ReadByte();
			bool flag10 = b_6 == 1;
			if (flag10)
			{
				b_7 = reader.ReadByte();
			}
			b_8 = reader.ReadByte();
			b_9 = reader.ReadByte();
			NpcType = reader.ReadUInt32();
			statusEffectDatas = reader.ReadList<StatusEffectData>(0);
			b_11 = reader.ReadByte();
			b_12 = reader.ReadByte();
			bool flag11 = b_12 == 1;
			if (flag11)
			{
				b_13 = reader.ReadByte();
			}
			b_14 = reader.ReadByte();
			NpcId = reader.ReadUInt64();
			b_15 = reader.ReadByte();
			bool flag12 = b_15 == 1;
			if (flag12)
			{
				u16_2 = reader.ReadUInt16();
			}
			b_16 = reader.ReadByte();
			bool flag13 = b_16 == 1;
			if (flag13)
			{
				subPKTNewNpc66 = reader.Read<subPKTNewNpc66>(0);
			}
			b_17 = reader.ReadByte();
			bool flag14 = b_17 == 1;
			if (flag14)
			{
				u64list = reader.ReadList<ulong>(0);
			}
			b_18 = reader.ReadByte();
			bool flag15 = b_18 == 1;
			if (flag15)
			{
				u64_1 = reader.ReadUInt64();
			}
			b_19 = reader.ReadByte();
			bool flag16 = b_19 == 1;
			if (flag16)
			{
				u32_0 = reader.ReadUInt32();
			}
			b_20 = reader.ReadByte();
			bool flag17 = b_20 == 1;
			if (flag17)
			{
				b_21 = reader.ReadByte();
			}
			b_24 = reader.ReadByte();
			bool flag18 = b_24 == 1;
			if (flag18)
			{
				u32_1 = reader.ReadUInt32();
			}
			u32_2 = reader.ReadUInt32();
			subPKTInitPC29s = reader.ReadList<subPKTInitPC29>(0);
		}
    }
}
