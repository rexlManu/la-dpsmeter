using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class PKTSkillStageNotify
    {
        public void SteamDecode(BitReader reader)
        {
            reader.Skip(8);
            Stage = reader.ReadByte();
            reader.Skip(28);
            SourceId = reader.ReadUInt64();
            reader.Skip(4);
            SkillId = reader.ReadUInt32();
        }
    }
}
