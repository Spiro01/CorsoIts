using Dapper;
using ElectroVamp.ApplicationCore.Config;
using ElectroVamp.ApplicationCore.Interfaces.Repositories;
using ElectroVamp.Domain.Entities;
using MySqlConnector;
using System.Data.Common;

namespace ElectroVamp.Data.Repositories
{
    public class MariadbClient : IMidageClient
    {
        private readonly MidageConfiguration _mariadbconfiguration;

        public MariadbClient(MidageConfiguration mariaDbConfiguration)
        {
            _mariadbconfiguration = mariaDbConfiguration;

        }
        protected DbConnection GetConnection()
        {
            return new MySqlConnection(_mariadbconfiguration.ConnString);
        }
        public async Task<IEnumerable<MariadbData>> GetWorkingMachines()
        {
            DbConnection conn = GetConnection();
            conn.Open();
            var res = await conn.QueryAsync<MariadbData>(@$"SELECT MACHINE_ID, PARTS_DONE, PARTS_TO_DO, TIME, PROGRAM_NAME " +
                                                            $"FROM `WORKS_AND_PROGRAMS_VIEW` WHERE PARTS_TO_DO > PARTS_DONE;");
            await conn.CloseAsync();
            return res;
        }

        public async void DeleteEndedProductions()
        {
            DbConnection conn = GetConnection();
            conn.Open();
            await conn.ExecuteAsync($"DELETE p,w FROM `PROGRAMS` p LEFT JOIN `WORKS` w ON (w.PROGRAM_ID = p.ID)  WHERE PARTS_TO_DO <= PARTS_DONE;");

            await conn.CloseAsync();
        }
    }

}