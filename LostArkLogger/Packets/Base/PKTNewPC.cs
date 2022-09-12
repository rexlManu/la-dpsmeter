using System.Collections.Generic;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public partial class PKTNewPC
    {
        public PKTNewPC(BitReader reader)
        {
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Steam) SteamDecode(reader);
            if (LostArkLogger.Instance.ConfigurationProvider.Configuration.Region == Region.Korea) KoreaDecode(reader);
        }
        public subPKTNewPC33 subPKTNewPC33;
        public PCStruct pCStruct;
        public Byte[] bytearray_0;
        public Byte[] bytearray_1;
        public UInt32 u32_0;
        public Byte b_0;
        public Byte b_1;
        public Byte b_2;
        public Byte b_3;
        public Byte b_4;
        public Byte b_5;
    }
}
