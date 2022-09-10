using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace LostArkLogger
{
    public static class VersionCheck
    {
        [DllImport("kernel32")] public static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] int dwFlags, [Out] StringBuilder lpExeName, ref int lpdwSize);

        public static Version SupportedSteamVersion = new Version("1.309.345.1839317");
        public static Version SupportedKoreaVersion = new Version("1.253.481.1841119");
        public static (Region, Version) GetLostArkVersion()
        {
            try
            {
                var lostArkProcesses = Process.GetProcessesByName("LOSTARK");
                foreach (var lostArkProcess in lostArkProcesses)
                {
                    var sb = new StringBuilder(1024);
                    int bufferLength = sb.Capacity + 1;
                    QueryFullProcessImageName(lostArkProcess.Handle, 0, sb, ref bufferLength);
                    var lostArkExe = sb.ToString();
                    var version = new Version(FileVersionInfo.GetVersionInfo(lostArkExe).ProductVersion.Split(' ')[0]);
                    if (version == SupportedSteamVersion) return (Region.Steam, version);
                    else if (version == SupportedKoreaVersion) return (Region.Korea, version);
                    else return (Region.Unknown, version);
                }
            }
            catch { }
            return (Region.Unknown, null);
        }
    }
}
