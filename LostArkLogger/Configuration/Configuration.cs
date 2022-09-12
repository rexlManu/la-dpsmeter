using System.Reflection;
using LostArkLogger;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace LoggerLinux.Configuration;

public class Configuration
{
    public Region Region = Region.Steam;

    public string WebHost = "host.docker.internal";
    public string PCapAddress = "192.168.178.99";
    public string PCapInterface = "";
    public int PCapPort = 1337;
    public int WebPort = 1338;
    public bool UseHttpBridge = false;

    public static byte[] ReadXorBinary(string name)
    {
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LostArkLogger.Resources." + name);
        if (stream == null)
            throw new Exception("Could not find resource " + name);
        var data = new byte[stream.Length];
        var read = stream.Read(data, 0, data.Length);
        return data;
    }
}

public class ConfigurationProvider
{
    private string configPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/config.yml";

    public Configuration Configuration;

    public ConfigurationProvider()
    {
        // check if file exists
        if (!File.Exists(configPath))
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(HyphenatedNamingConvention.Instance).Build();
            File.WriteAllText(configPath, serializer.Serialize(new Configuration()));
            Console.WriteLine("Config created.");
            Environment.Exit(-1);
            return;
        }

        // load config
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(HyphenatedNamingConvention.Instance).Build();

        this.Configuration = deserializer.Deserialize<Configuration>(File.ReadAllText(configPath));
    }
}