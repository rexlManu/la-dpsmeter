using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTNewNpcSummon
    {
        public void SteamDecode(BitReader reader)
        {
            reader.Skip(31);
            OwnerId = reader.ReadUInt64();
            b_0 = reader.ReadByte();
            bytearray_1 = reader.ReadBytes(13);
            npcStruct = reader.Read<NpcStruct>();
        }
    }
}
