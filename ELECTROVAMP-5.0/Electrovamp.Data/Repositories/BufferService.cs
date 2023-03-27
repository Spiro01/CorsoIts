using Dapper;
using ElectroVamp.ApplicationCore.Config;
using ElectroVamp.ApplicationCore.Interfaces.Repositories;
using ElectroVamp.Domain.Entities;
using Serilog;
using System.Data.SQLite;

namespace ElectroVamp.Data.Repositories
{
    public class BufferService : IBufferService
    {
        private readonly ILogger _logger;
        private GlobalConfiguration _globalConfiguration;
        private SQLiteConnection _connection;
        public BufferService(ILogger logger, GlobalConfiguration globalConfiguration)
        {
            _logger = logger;
            _globalConfiguration = globalConfiguration;
            if (!Directory.Exists(_globalConfiguration.SaveDirectory))
            {
                Directory.CreateDirectory(_globalConfiguration.SaveDirectory);
            }
            InitDB();
        }

        private async void InitDB()
        {
            _connection = new SQLiteConnection("URI=file:" + _globalConfiguration.SqlitePath);
            await _connection.OpenAsync();
        }
        public async Task SaveStatus(MachineState data)
        {
            if(data.time_t == DateTime.MinValue) data.time_t = DateTime.Now;
            try
            {
                await _connection.ExecuteAsync($"DELETE FROM SAVES WHERE machine_id = '{data.MachineId}'");
                await _connection.ExecuteAsync("INSERT INTO saves (machine_id,counter,time_t) values (@MachineId,@counter,@time_t)", data);
                return;
            }
            catch (Exception ex)
            {
                _logger.Warning("Error while Saving the status -- {ex}", ex.Message);
            }
        }
        public async Task<MachineState> Reload(string machineid)
        {
            IEnumerable<MachineState> result;
            try
            {
                result = await _connection.QueryAsync<MachineState>($"select machine_id as MachineId,counter, time_t from saves where machine_id = '{machineid}'");

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error while Fetching the status -- {ex}");
            }
            if (result.Count() == 0) return new MachineState();

            return result.Single();
        }

        public async Task Reset()
        {
            await _connection.ExecuteAsync("DELETE FROM saves");
        }
    }
}

