using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroVamp.ApplicationCore.Config
{
    public class MidageConfiguration
    {
        public string ConnString { get; set; }
        public int RefreshTime { get; set; }
        public List<string> DeleteEndedProductions { get; set; }
    }
}
