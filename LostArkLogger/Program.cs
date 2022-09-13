using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Principal;
using LoggerLinux.Configuration;
using LostArkLogger.Event;
using LostArkLogger.State;
using LostArkLogger.Utilities;

namespace LostArkLogger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine(" DPS Meter - https://github.com/rexlManu/dps-meter");
            Console.WriteLine(
                " If you have any issues, please report them on the github page or on our discord server.");
            Console.WriteLine(" Discord: discord.gg/bm8ntsjveb");
            Console.WriteLine("");
            Console.WriteLine(" Credits to the following people:");
            Console.WriteLine(
                " https://github.com/shalzuth/LostArkLogger - For the big work of reverse engineering the Lost Ark network protocol and providing a open source solution.");
            Console.WriteLine(
                " https://github.com/karaeren/loa-details - For a really nice UI and UX for displaying damage meter.");
            Console.WriteLine(" Ofcourse a big thanks you to all contributors of the projects too.");
            Console.WriteLine("");
            // Shutdown hook
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => { LostArkLogger.Instance.onExit(); };

            LostArkLogger.Instance.onLaunch(args);
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
        public EventManager EventManager;
        public StateManager StateManager;
        private HttpBridge? _httpBridge;
        private ApplicationServer? Server;

        public void onLaunch(string[] args)
        {
            this.ConfigurationProvider = new ConfigurationProvider();
            this.EventManager = new EventManager();

            if (this.ConfigurationProvider.Configuration.UseHttpBridge)
            {
                Console.WriteLine("useHttpBridge is true, starting http bridge");
                this._httpBridge = new HttpBridge() {args = args};
                return;
            }

            this.StateManager = new StateManager();
            this.Server = new ApplicationServer();

            this.Server.Start();
        }

        public void onExit()
        {
            if (this._httpBridge != null)
            {
                this._httpBridge.Stop();
            }

            if (Server != null)
            {
                this.Server.close();
            }
        }
    }
}