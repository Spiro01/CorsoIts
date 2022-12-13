using Domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendToDb.Interfaces;

namespace SendToDb.Functions
{
    public class TestFunction
    {
        private readonly IPerformanceDataStorage _performanceDataStorage;

        public TestFunction(IPerformanceDataStorage performanceDataStorage) => _performanceDataStorage = performanceDataStorage;


        [FunctionName("TestFunction")]
        public void Run([ServiceBusTrigger("dalpont", Connection = "cs")] PerformanceData myQueueItem, ILogger log)
        {
            log.LogInformation("C# ServiceBus queue trigger function");
            _performanceDataStorage.InsertPerformanceData(myQueueItem);

        }
    }
}
