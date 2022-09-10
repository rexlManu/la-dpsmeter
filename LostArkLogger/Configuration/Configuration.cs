using System.Reflection;
using LostArkLogger;

namespace LoggerLinux.Configuration;

public class Configuration
{
    public static Region Region = Region.Steam;
    
    public static string PCapAddress = "192.168.178.99";
    public static string PCapInterface = "\\Device\\NPF_{D55DEB80-AD97-4004-9F0C-FD5661D9FC45}";
    public static int PCapPort = 1337;
    
    public static byte[] ReadXorBinary(string name)
    {
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LostArkLogger.Resources." + name);
        if(stream == null)
            throw new Exception("Could not find resource " + name);
        var data = new byte[stream.Length];
        var read = stream.Read(data, 0, data.Length);
        return data;
    }

}