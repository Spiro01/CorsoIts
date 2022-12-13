using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace SendToDb.Interfaces;

public interface IPerformanceDataStorage
{
    public Task<IEnumerable<PerformanceData>> GetPerformanceData();
    public Task InsertPerformanceData(PerformanceData data);
}