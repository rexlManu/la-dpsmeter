using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTNewNpc
    {
        public void SteamDecode(BitReader reader)
        {
            b_1 = reader.ReadByte();
            if (b_1 == 1)
                u64_0 = reader.ReadUInt64();
            npcStruct = reader.Read<NpcStruct>();
            b_0 = reader.ReadByte();
            b_2 = reader.ReadByte();
            if (b_2 == 1)
                b_3 = reader.ReadByte();
        }
    }
}
