using ElectroVamp.ApplicationCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroVamp.ApplicationCore
{
    public class AppSettings
    {
        public OpcClientConfiguration OpcClientConfiguration { get; set; }
        public SalvagniniConfiguration SalvagniniConfiguration { get; set; }
        public MidageConfiguration MariaDbConfiguration { get; set; }
        public GlobalConfiguration GlobalConfiguration { get; set; }
    }
}
