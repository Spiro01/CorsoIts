using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendToDb.Interfaces;

namespace SendToDb.Functions
{
    public class TestFunction
    {
        private IPerformanceDataStorage _performanceDataStorage;

        public TestFunction(IPerformanceDataStorage performanceDataStorage)
        {
            _performanceDataStorage = performanceDataStorage;
        }


        [FunctionName("TestFunction")]
        public async Task Run([ServiceBusTrigger("dalpont", Connection = "cs")] string myQueueItem, ILogger log)
        {
            log.LogInformation("C# ServiceBus queue trigger function");
            try
            {
                var item = JsonConvert.DeserializeObject<PerformanceData>(myQueueItem);
                await _performanceDataStorage.InsertPerformanceData(item);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
        }
    }
}
