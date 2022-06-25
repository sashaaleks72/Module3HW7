
namespace Module3HW7.MyLogger
{
    public enum LogType
    {
        Info,
        Warning,
        Error
    }

    public class Logger
    {
        public event Action<int> NotifyWhenDoBackup;

        public Logger(int n, Action<int> handler)
        {
            Logs = new List<string>();
            N = n;
            NotifyWhenDoBackup += handler;
        }

        public int N { get; set; }
        private List<string> Logs { get; set; }

        private readonly string logsPath = "../../../Logs/logs.txt";
        private readonly string backupsPath = "../../../Backups";

        public void DeleteLogsFromDirectory()
        {
            if (File.Exists(logsPath))
            {
                File.Delete(logsPath);
            }
        }

        public void DeleteBackupsFromDirectory()
        {
            var filesNames = Directory.GetFiles(backupsPath);

            for (int i = 0; i < filesNames.Length; i++)
            {
                if (File.Exists(filesNames[i]))
                {
                    File.Delete(filesNames[i]);
                }
            }
        }

        public async Task WriteLogIntoFileAsync(string log, LogType logType)
        {
            if (NotifyWhenDoBackup != null) NotifyWhenDoBackup(N - Logs.Count % N - 1);

            log = $"{DateTime.Now}: {logType.ToString()}: {log}";
            Logs.Add(log);

            using (var fileWriter = new StreamWriter(logsPath, true))
            {
                await fileWriter.WriteLineAsync(log);
            }

            if (Logs.Count % N == 0)
            {
                using (var fileWriter = new StreamWriter($"{backupsPath}/{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")}.txt", false))
                {
                    await fileWriter.WriteAsync(String.Join('\n', Logs));
                }
                Console.WriteLine("Backup created!");
            }
        }
    }
}
