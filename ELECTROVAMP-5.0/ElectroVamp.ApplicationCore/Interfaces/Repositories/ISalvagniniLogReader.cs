using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroVamp.ApplicationCore.Interfaces.Repositories
{
    public interface ISalvagniniLogReader
    {
        public event EventHandler onLogCreated;
        public void Start();
    }
}
