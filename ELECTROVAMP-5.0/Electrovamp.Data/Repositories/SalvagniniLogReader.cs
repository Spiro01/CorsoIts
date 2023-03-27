using ElectroVamp.ApplicationCore.Config;
using ElectroVamp.ApplicationCore.Interfaces.Repositories;
using ElectroVamp.Domain.Entities;
using Serilog;

namespace ElectroVamp.Data.Repositories
{
    public class SalvagniniLogReader : ISalvagniniLogReader
    {
        private readonly SalvagniniConfiguration _salvagniniconfig;
        public event EventHandler onLogCreated;
        private ILogger _logger;

        public SalvagniniLogReader(SalvagniniConfiguration salvagniniConfiguration, ILogger logger)
        {
            _salvagniniconfig = salvagniniConfiguration;
            _logger = logger;
            _logger.Information("Service SalvagniniLogReader loaded successfully");
        }
        public void Start()
        {
            if (!Directory.Exists(_salvagniniconfig.DirectoryPath)) return;

            Thread thread = new Thread(async () =>
            {
                using var watcher = new FileSystemWatcher(_salvagniniconfig.DirectoryPath);

                watcher.NotifyFilter = NotifyFilters.Attributes
                                     | NotifyFilters.CreationTime
                                     | NotifyFilters.DirectoryName
                                     | NotifyFilters.FileName
                                     | NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.Security
                                     | NotifyFilters.Size;

                watcher.Created += OnCreated;
                watcher.Error += OnError;

                watcher.Filter = "*.syn";
                watcher.IncludeSubdirectories = true;
                watcher.EnableRaisingEvents = true;

                await Task.Delay(-1);
            });
            thread.Start();
        }

        private bool TxtFileIsReady(FileSystemEventArgs e)
        {
            return File.Exists(e.FullPath.Replace(".syn", ".txt")) && new FileInfo(e.FullPath.Replace(".syn", ".txt")).Length > 0;
        }

        private async void OnCreated(object sender, FileSystemEventArgs e)
        {
            if (TxtFileIsReady(e))
            {
                try
                {
                    string line = await File.ReadAllTextAsync(e.FullPath.Replace(".syn", ".txt"));
                    string[] lines = line.Split("|");
                    var send = new LogLine(lines[0], lines[2], lines[3], lines[4], lines[7], Convert.ToInt32(lines[14]), Convert.ToInt32(lines[15]));

                    onLogCreated(send, e);
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error while parsing the log -- {ex}");
                }
            }

        }
        private void OnError(object sender, ErrorEventArgs e)
        {
            _logger.Error($"Error while watching the filesys -- {e.GetException().Message}");
        }
    }
}
