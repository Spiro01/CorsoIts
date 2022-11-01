using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureChecker_MAP
{
    public class Sensor
    {
        
        public Sensor(string id, string value, string timeStamp)
        {
            Id = id;
            if (int.TryParse(value, out int ivalue))
            {
                Value = ivalue;
            }
            if (DateTime.TryParse(timeStamp, out DateTime timestamp))
            {
                TimeStamp = timestamp;
            }
        }

        public string Id { get; set; }
        public int Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
