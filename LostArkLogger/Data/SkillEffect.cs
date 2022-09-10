using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerLinux.Configuration;

namespace LostArkLogger
{
    public class SkillEffect
    {
        public static Dictionary<Int32, String> Items = (Dictionary<Int32, String>)ObjectSerialize.Deserialize(Configuration.ReadXorBinary("SkillEffect.bin"));
    }
}
