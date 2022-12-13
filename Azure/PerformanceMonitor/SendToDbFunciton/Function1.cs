using System;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SendToDbFunciton
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([ServiceBusTrigger("spironelli", Connection = "")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            
        }
    }
}
