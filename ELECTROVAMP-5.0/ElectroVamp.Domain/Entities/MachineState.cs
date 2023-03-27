using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroVamp.Domain.Entities
{
    public class MachineState
    {
        public string MachineId { get; set; }
        public ushort counter { get; set; }
        public DateTime time_t { get; set; }
        public int buffer { get; set; }
    }
}
