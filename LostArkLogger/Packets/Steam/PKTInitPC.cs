using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTInitPC
    {
        public void SteamDecode(BitReader reader)
        {
            bytearraylist_3 = reader.ReadList<Byte[]>(1);
            statusEffectDatas = reader.ReadList<StatusEffectData>();
            u32_0 = reader.ReadUInt32();
            b_0 = reader.ReadByte();
            b_1 = reader.ReadByte();
            u32_2 = reader.ReadUInt32();
            b_3 = reader.ReadByte();
            u16_0 = reader.ReadUInt16();
            b_4 = reader.ReadByte();
            u64_0 = reader.ReadUInt64();
            u32_3 = reader.ReadUInt32();
            u32_4 = reader.ReadUInt32();
            bytearraylist_0 = reader.ReadList<Byte[]>(30);
            u32_11 = reader.ReadUInt32();
            u32_12 = reader.ReadUInt32();   
            b_20 = reader.ReadByte();
            bytearraylist_1 = reader.ReadList<Byte[]>(17);
            b_5 = reader.ReadByte();
            u64_1 = reader.ReadUInt64();
            bytearray_3 = reader.ReadBytes(25);
            PlayerId = reader.ReadUInt64();
            u32_5 = reader.ReadUInt32();
            bytearray_0 = reader.ReadBytes(112);
            ClassId = reader.ReadUInt16();
            b_6 = reader.ReadByte();
            u32_6 = reader.ReadUInt32();
            b_2 = reader.ReadByte();
            if (b_2 == 1)
                u32_1 = reader.ReadUInt32();
            b_7 = reader.ReadByte();
            u32_7 = reader.ReadUInt32();
            Name = reader.ReadString();
            u32_8 = reader.ReadUInt32();
            b_8 = reader.ReadByte();
            bytearraylist_2 = reader.ReadList<Byte[]>(2);
            b_9 = reader.ReadByte();
            b_10 = reader.ReadByte();
            u32_9 = reader.ReadUInt32();
            reader.Skip(32);
            u16_1 = reader.ReadUInt16();
            reader.Skip(1);
            u16_2 = reader.ReadUInt16();
            u64_2 = reader.ReadUInt64();
            b_11 = reader.ReadByte();
            u64_3 = reader.ReadUInt64();
            subPKTInitPC29s = reader.ReadList<subPKTInitPC29>();
            b_12 = reader.ReadByte();
            b_13 = reader.ReadByte();
            u16_3 = reader.ReadUInt16();
            statPair = reader.Read<StatPair>();
            b_14 = reader.ReadByte();
            b_15 = reader.ReadByte();
            GearLevel = reader.ReadUInt32();
            b_16 = reader.ReadByte();
            b_17 = reader.ReadByte();
            b_18 = reader.ReadByte();
            b_19 = reader.ReadByte();
            u16_4 = reader.ReadUInt16();
            u32_10 = reader.ReadUInt32();
            u16_5 = reader.ReadUInt16();
            u64_4 = reader.ReadUInt64();
        }
    }
}
