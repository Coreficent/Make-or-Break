namespace Coreficent.Utility
{
    using UnityEngine;

    public class DebugLogger
    {
        private static readonly string _delimiter = "::";
        private static readonly string _ender = "";

        public static void ToDo(object message)
        {
            Output("Todo", message);
        }

        public static void Log(string name, object message)
        {
            Log(name + _delimiter + message);
        }
        public static void Log(object message)
        {
            Output("Log", message);
        }
        public static void Warn(object message)
        {
            Output("Warn", message);
        }

        public static void Error(object message)
        {
            Output("Error", message);
        }

        private static void Output(string messageType, object message)
        {
            if (ApplicationMode._applicationMode.DebugMode)
            {
                Debug.Log(messageType + _delimiter + message + _ender);
            }
        }
    }
}
