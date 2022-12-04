using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LostArkLogger
{
    public partial class PCStruct
    {
        public void SteamDecode(BitReader reader)
        {
            b_1 = reader.ReadByte();
            b_2 = reader.ReadByte();
            b_3 = reader.ReadByte();
            u32_0 = reader.ReadUInt32();
            u32_1 = reader.ReadUInt32();
            b_12 = reader.ReadByte();
            statusEffectDatas = reader.ReadList<StatusEffectData>();
            PlayerId = reader.ReadUInt64();
            Level = reader.ReadUInt16();
            u32_2 = reader.ReadUInt32();
            b_4 = reader.ReadByte();
            ClassId = reader.ReadUInt16();
            b_5 = reader.ReadByte();
            u16_0 = reader.ReadUInt16();
            bytearray_2 = reader.ReadBytes(5);
            statPair = reader.Read<StatPair>();
            u32_3 = reader.ReadUInt32();
            EquippedItems = reader.ReadList<ItemInfo>();
            skillRunes = reader.Read<SkillRunes>();
            subPKTNewNpc5 = reader.Read<subPKTNewNpc5>();
            b_6 = reader.ReadByte();
            u32_4 = reader.ReadUInt32();
            b_7 = reader.ReadByte();
            itemInfos = reader.ReadList<ItemInfo>();
            u32_5 = reader.ReadUInt32();
            bytearray_0 = reader.ReadBytes(25);
            u32list_0 = reader.ReadList<UInt32>(4);
            u32_6 = reader.ReadUInt32();
            u16_1 = reader.ReadUInt16();
            PartyId = reader.ReadUInt64();
            u16_2 = reader.ReadUInt16();
            GearLevel = reader.ReadUInt32();
            u16_3 = reader.ReadUInt16();
            Name = reader.ReadString();
            b_0 = reader.ReadByte();
            if (b_0 == 1)
                bytearray_1 = reader.ReadBytes(12);
            b_8 = reader.ReadByte();
            str_0 = reader.ReadString();
            u32_7 = reader.ReadUInt32();
            u32_8 = reader.ReadUInt32();
            b_9 = reader.ReadByte();
            subPKTInitPC29s = reader.ReadList<subPKTInitPC29>();
            u64_0 = reader.ReadUInt64();
            b_10 = reader.ReadByte();
            b_11 = reader.ReadByte();
            
            try {
                var nonASCII = @"[^\x00-\x7F]+";
                var rgx = new Regex(nonASCII);
                if (rgx.IsMatch(Name))
                    Name = Npc.GetPcClass(ClassId);
            } catch (Exception e) {
                Console.WriteLine("Failed matching PC name:\n" + e);
                Name = "@BAD_NAME@" + new Random().Next(1000, 9999);
            }
        }
    }
}
