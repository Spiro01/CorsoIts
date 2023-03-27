using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroVamp.Domain.Entities
{
    public class MariadbData
    {
        public string MACHINE_ID { get; set; }
        public int PARTS_DONE { get; set; }
        public int PARTS_TO_DO { get; set; }
        public int TIME { get; set; }
        public string PROGRAM_NAME { get; set; }
    }
}
