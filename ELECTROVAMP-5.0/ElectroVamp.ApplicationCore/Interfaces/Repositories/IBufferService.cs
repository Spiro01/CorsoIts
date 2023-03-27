using ElectroVamp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroVamp.ApplicationCore.Interfaces.Repositories
{
    public interface IBufferService
    {
        public Task SaveStatus(MachineState data);
        public Task<MachineState> Reload(string machineid);
        public Task Reset();
    }
}
