using System.Data;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ApplicationCore.Configuration;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Linq;
using InfluxDB.Client.Writes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.Repository;

public class StatusRepository : IStatusRepository
{
    private readonly InfluxDBClient influxDBClient;
    private readonly AppSettings _appSettings;
    public StatusRepository(AppSettings appSettings)
    {
        _appSettings = appSettings;

        influxDBClient = InfluxDBClientFactory.Create(appSettings.InfluxDb.Host, _appSettings.InfluxDb.ApiKey);
    }

    public async Task<IEnumerable<Status>> Get(string? drone, string fromDate)
    {
        if (string.IsNullOrWhiteSpace(fromDate))
        {
            fromDate = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)).ToString("O");
        }

        var dateTime = DateTime.Parse(fromDate);

        var queryApi = influxDBClient.GetQueryApiSync();
        var settings = new QueryableOptimizerSettings();
        IEnumerable<Status> result;
        try
        {
            result = InfluxDBQueryable<Status>
               .Queryable("DroneStatus", _appSettings.InfluxDb.OrgId, queryApi, settings)
               .Where(x => x.Time >= dateTime)
               .Where(x => x.DroneId == drone || string.IsNullOrWhiteSpace(drone))

               .ToList();
        }
        catch (Exception)
        {
            result = new List<Status>();
        }
        return result;
    }



    public async Task Insert(Status status)
    {
        var writeApi = influxDBClient.GetWriteApiAsync();
        await writeApi.WriteMeasurementAsync(status, WritePrecision.Ns, "DroneStatus", _appSettings.InfluxDb.OrgId);
    }


    public async Task Purge()
    {
        using var client = new HttpClient();

        var queryString = new Dictionary<string, string>()
        {
            { "bucket", "DroneStatus" },
            { "orgID", _appSettings.InfluxDb.OrgId },

        };

        var requestUri = QueryHelpers.AddQueryString($"{_appSettings.InfluxDb.Host}/api/v2/delete", queryString!);

        client.DefaultRequestHeaders.Add("Authorization", $"Token {_appSettings.InfluxDb.ApiKey}");


        var res = await client.PostAsJsonAsync(requestUri, new
        {
            start = DateTime.Parse("2020/01/01").ToString("yyyy-MM-ddThh:mm:ssZ"),
            stop = DateTime.Parse("2030/01/01").ToString("yyyy-MM-ddThh:mm:ssZ")
        });

        res.EnsureSuccessStatusCode();

    }
}