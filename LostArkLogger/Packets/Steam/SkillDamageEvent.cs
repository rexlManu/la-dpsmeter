using System;
using System.Collections.Generic;
namespace LostArkLogger
{
    public partial class SkillDamageEvent
    {
        public void SteamDecode(BitReader reader)
        {
            Modifier = reader.ReadByte();
            b_1 = reader.ReadByte();
            if (b_1 == 1)
                b_2 = reader.ReadByte();
            u16_0 = reader.ReadUInt16();
            CurHp = reader.ReadPackedInt();
            MaxHp = reader.ReadPackedInt();
            TargetId = reader.ReadUInt64();
            Damage = reader.ReadPackedInt();
            b_0 = reader.ReadByte();
        }
    }
}
