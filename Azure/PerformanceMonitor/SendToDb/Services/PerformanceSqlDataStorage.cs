using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Domain;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Extensions.Configuration;
using SendToDb.Interfaces;

namespace SendToDb.Services;

public class PerformanceSqlDataStorage : IPerformanceDataStorage
{
    private readonly string _connString;

    PerformanceSqlDataStorage(IConfiguration configuration) => _connString = configuration.GetConnectionString("db");

    public async Task<IEnumerable<PerformanceData>> GetPerformanceData()
    {
        using var connection = new SqlConnection(_connString);


        const string query = @"
INSERT INTO [dbo].[spironelli]
           ([DeviceName]
           ,[AcquisitionDate]
           ,[CpuUsage]
           ,[RamUsage])
     VALUES
           (@DeviceName
           ,@AcquisitionDateTime
           ,@CpuUsage
           ,@RamUsage
"
        ;


        return await connection.QueryAsync<PerformanceData>(query);
    }

    public async Task InsertPerformanceData(PerformanceData data)
    {
        using var connection = new SqlConnection(_connString);
        

        const string query = @"
INSERT INTO [dbo].[spironelli]
           ([DeviceName]
           ,[AcquisitionDate]
           ,[CpuUsage]
           ,[RamUsage])
     VALUES
           (@DeviceName
           ,@AcquisitionDateTime
           ,@CpuUsage
           ,@RamUsage
";


        await connection.ExecuteAsync(query, data);

    }

    
}