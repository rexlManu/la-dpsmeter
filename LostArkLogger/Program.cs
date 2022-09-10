using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Principal;
using LostArkLogger.Utilities;

namespace LostArkLogger
{
    public class Program
    {

        public static void Main(string[] args)
        {
            // print working directory
            Console.WriteLine("Starting httpbridge");
            var server = new WebSocketServer();
            server.Start();
            Console.ReadLine();
        }
    }
}
