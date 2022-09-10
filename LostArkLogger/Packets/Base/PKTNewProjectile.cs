using System;
using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTNewProjectile
    {
        public PKTNewProjectile(BitReader reader)
        {
            if (Configuration.Region == Region.Steam) SteamDecode(reader);
            if (Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public ProjectileInfo projectileInfo;
    }
}
