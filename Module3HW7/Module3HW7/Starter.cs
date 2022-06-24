using Module3HW7.Configs;
using Module3HW7.MyLogger;
using Newtonsoft.Json;

namespace Module3HW7
{
    public class Starter
    {
        public async Task Run()
        {
            string backupConfigFile = File.ReadAllText("../../../Configs/backup-config.json");
            var backupConfig = JsonConvert.DeserializeObject<BackupConfig>(backupConfigFile);

            var logger = new Logger(backupConfig!.N, NotifyHandler);

            logger.DeleteLogsFromDirectory();
            logger.DeleteBackupsFromDirectory();

            ConsoleKeyInfo key = new();

            var logs1 = new List<string>();
            var logs2 = new List<string>();

            while (logs1.Count != 50)
            {
                Console.Write("Press some button: ");
                key = Console.ReadKey();
                Console.WriteLine();

                Console.WriteLine($"You pressed {key.Key} button");

                logs1.Add($"You pressed {key.Key} button");
                logs2.Add($"You pressed {key.Key} button");
            }

            await AddFiftyRowsAsync1(logger, logs1);
            await AddFiftyRowsAsync2(logger, logs2);
        }

        public void NotifyHandler(int remainingLogs)
        {
            if (remainingLogs > 0)
                Console.WriteLine($"{remainingLogs} logs left to do backup!");
        }

        public async Task AddFiftyRowsAsync1(Logger logger, List<string> logs)
        {
            for (int i = 0; i < logs.Count; i++)
            {
                await logger.WriteLogIntoFileAsync($"{logs[i]}", LogType.Info);
            }
        }

        public async Task AddFiftyRowsAsync2(Logger logger, List<string> logs)
        {
            for (int i = 0; i < logs.Count; i++)
            {
                await logger.WriteLogIntoFileAsync($"{logs[i]}", LogType.Info);
            }
        }
    }
}

