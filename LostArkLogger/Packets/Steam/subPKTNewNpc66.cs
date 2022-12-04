using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class subPKTNewNpc66
    {
        public void SteamDecode(BitReader reader)
        {
            u64_0 = reader.ReadUInt64();
            u16_0 = reader.ReadUInt16();
            str_0 = reader.ReadString();
            b_1 = reader.ReadByte();
            b_2 = reader.ReadByte();
            subPKTNewNpc5 = reader.Read<subPKTNewNpc5>();
            itemInfos = reader.ReadList<ItemInfo>();
            b_0 = reader.ReadByte();
        }
    }
}
