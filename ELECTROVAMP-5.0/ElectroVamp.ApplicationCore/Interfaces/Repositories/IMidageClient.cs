using ElectroVamp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroVamp.ApplicationCore.Interfaces.Repositories
{
    public interface IMidageClient
    {
        public Task<IEnumerable<MariadbData>> GetWorkingMachines();
        public void DeleteEndedProductions();
    }
}
