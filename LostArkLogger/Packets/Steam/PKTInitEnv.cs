using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTInitEnv
    {
        public void SteamDecode(BitReader reader)
        {
            u32_0 = reader.ReadUInt32();
            s64 = reader.ReadSimpleInt();
            u32_1 = reader.ReadUInt32();
            u64 = reader.ReadUInt64();
            b = reader.ReadByte();
            PlayerId = reader.ReadUInt64();
            blist = reader.ReadList<byte>(0);
            subPKTInitEnv5 = reader.Read<subPKTInitEnv5>(0);
        }
    }
}
