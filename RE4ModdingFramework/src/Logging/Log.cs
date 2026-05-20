using System.Runtime.CompilerServices;


namespace RE4ModdingFramework.src.Logging
{
    public static class Log
    {
        private enum Level
        {
            INFO,
            WARNING,
            ERROR,
            DEBUG
        }

        public static void Info(string msg, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Write(msg, Level.INFO, ConsoleColor.Cyan, lineNumber, caller);
        }

        public static void Error(string msg, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Write(msg, Level.ERROR, ConsoleColor.Red, lineNumber, caller);
        }

        public static void Warning(string msg, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Write(msg, Level.WARNING, ConsoleColor.Yellow, lineNumber, caller);
        }

        public static void Debug(string msg, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Write(msg, Level.DEBUG, ConsoleColor.Green, lineNumber, caller);
        }

        private static void Write(string msg, Level level, ConsoleColor levelcolor, int lineNumber, string caller)
        {
            string time = Time.GetTime();
            LogFile.CreateDirectory();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("[" + time + "]");
            Console.ForegroundColor = levelcolor;
            Console.Write($"[{level}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("<");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{caller}:{lineNumber}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">");
            Console.WriteLine(" " + msg);
            Console.ResetColor();
            LogFile.WriteToFile($"[{time}][{level}]<{caller}:{lineNumber}> {msg}");
        }
    }
}
