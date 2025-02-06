using System;
using System.Collections.Generic;
using System.IO;

namespace Utilities
{
    public static class Logger
    {
        private static List<string> logMessages = new List<string>();
        private static readonly string logFilePath = "log.txt";

        /// <summary>
        /// Registra uma mensagem no log e salva em um arquivo.
        /// </summary>
        public static void Log(string message)
        {
            string timestampedMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            logMessages.Add(timestampedMessage);
            SaveLogToFile(timestampedMessage);
        }

        /// <summary>
        /// Obtém todas as mensagens do log.
        /// </summary>
        public static IEnumerable<string> GetLogMessages() => logMessages;

        /// <summary>
        /// Limpa o log na memória e no arquivo.
        /// </summary>
        public static void ClearLog()
        {
            logMessages.Clear();
            File.WriteAllText(logFilePath, string.Empty);
        }

        /// <summary>
        /// Salva uma mensagem no arquivo de log.
        /// </summary>
        private static void SaveLogToFile(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar log no arquivo: {ex.Message}");
            }
        }
    }
}
