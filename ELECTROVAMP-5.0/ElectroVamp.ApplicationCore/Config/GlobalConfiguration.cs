using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroVamp.ApplicationCore.Config
{
    public class GlobalConfiguration
    {
        public string ConfigPath { get; set; }
        public string SqlitePath { get; set; }
        public string SaveDirectory { get; set; }
        public List<string> CounterReset { get; set; }
    }
}
