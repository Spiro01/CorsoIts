using ElectroVamp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroVamp.ApplicationCore.Config
{

    public class OpcClientConfiguration
    {
        public KepServer KepServerConfiguration { get; set; }
        public List<FirstGenConfiguration> FirstGenConfiguration { get; set; }
    }
    public class FirstGenConfiguration
    {
        public string MachineId { get; set; }
        public string Url { get; set; }
        public List<Node> Nodes { get; set; }
    }

    public class KepServer
    {
        public string Url { get; set; }
        public List<FirstGenKep> Machines { get; set; }
    }


    public class FirstGenKep
    {
        public string MachineId { get; set; }
        public List<Node> Nodes { get; set; }
    }
   



}
