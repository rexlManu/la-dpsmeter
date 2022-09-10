using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Principal;
using LoggerLinux.Configuration;
using LostArkLogger.Utilities;

namespace LostArkLogger
{
    public class Program
    {

        public static void Main(string[] args)
        {
            // Shutdown hook
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) =>
            {
                LostArkLogger.Instance.onExit();
            };
            
            LostArkLogger.Instance.onLaunch();
            // Hold program open
            while (true)
            {
                Console.ReadLine();
            }
        }
    }

    public class LostArkLogger
    {
        private static LostArkLogger? _instance = null;
        
        public static LostArkLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LostArkLogger();
                }
                return _instance;
            }
        }

        public ConfigurationProvider ConfigurationProvider;
        private WebSocketServer Server;
        public void onLaunch()
        {
            this.ConfigurationProvider = new ConfigurationProvider();
            this.Server = new WebSocketServer();
            
            this.Server.Start();
        }
        
        public void onExit()
        {
            this.Server.close();
        }
    }
}
