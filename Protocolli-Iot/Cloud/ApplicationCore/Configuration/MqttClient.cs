using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Configuration
{
    public class MqttClient
    {
        public string Host { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
