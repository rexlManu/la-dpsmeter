using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostArkLogger.Utilities
{
    public static class Logger
    {
        public static string documentsPath = "/mnt/raid1/apps/";
        public static string logsPath = Path.Combine(documentsPath, "Lost Ark Logs");

        public static bool debugLog = false;

        static BinaryWriter logger;
        static FileStream debugStream;
        private static StreamWriter logStream = null;

        private static readonly object LogFileLock = new object();
        private static readonly object DebugFileLock = new object();
        public static string fileName = logsPath + "/LostArk_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".log";
        public static DateTime fileDate = DateTime.Now;
        private static Thread thread = null;
        private static bool stopThread = false;

        static Logger()
        {
            if (!Directory.Exists(logsPath)) Directory.CreateDirectory(logsPath);
            thread = new Thread(Run);
            thread.Start();
        }

        public static void UpdateLogPath(string customLogPath = default)
        {
            if (!String.IsNullOrEmpty(customLogPath))
            {
                logsPath = customLogPath;
            }

            if (!Directory.Exists(logsPath)) Directory.CreateDirectory(logsPath);
        }

        public static void StartNewLogFile()
        {
            if(logStream != null) logStream.Close();
            fileName = logsPath + "/LostArk_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".log";
            logStream = new StreamWriter(fileName, true);
            logStream.AutoFlush = true;
            fileDate = DateTime.Now;
        }
        public static event Action<string> onLogAppend;
        static bool InittedLog = false;
        public static ConcurrentQueue<string> logLines = new ConcurrentQueue<string>();
        public static void AppendLog(int id, params string[] elements)
        {
            // check if current day is different then from fileDate
            if (fileDate.Day != DateTime.Now.Day || logStream == null)
            {
                StartNewLogFile();
            }
            if (InittedLog == false)
            {
                InittedLog = true;
                AppendLog(253, System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
            }
            var log = id + "|" + DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") + "|" + String.Join("|", elements);
            var logHash = string.Concat(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(log)).Select(x => x.ToString("x2")));

            Task.Run(() =>
            {
               
                onLogAppend?.Invoke(log + "\n");
                logLines.Enqueue(log + "|" + logHash + "\n");
            });
        }
        public static void DoDebugLog(byte[] bytes)
        {
            if (debugLog)
            {
                Task.Run(() =>
                {
                    var log = BitConverter.GetBytes(DateTime.Now.ToBinary())
                        .Concat(BitConverter.GetBytes(bytes.Length)).Concat(bytes).ToArray();

                    lock (DebugFileLock)
                    {
                        if (logger == null)
                        {
                            debugStream = new FileStream(fileName.Replace(".log", ".bin"), FileMode.Create);
                            logger = new BinaryWriter(debugStream);
                        }

                        logger.Write(log);
                    }
                });
            }
        }
        public static void Run()
        {
            while (!stopThread)
            {
                if (logStream != null && logLines.TryDequeue(out var msg)) {
                    logStream.Write(msg);
                } else
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
