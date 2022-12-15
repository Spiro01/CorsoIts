namespace Domain;

public class PerformanceData
{
    public PerformanceData(string deviceName,DateTime acquisitionDateTime, float cpuUsage, float ramUsage)
    {
        AcquisitionDateTime = acquisitionDateTime;
        CpuUsage = cpuUsage;
        RamUsage = ramUsage;
        DeviceName = deviceName;
    }

    public int? Id;
    public string DeviceName { get; set; }
    public DateTime AcquisitionDateTime { get; set; }
    public float CpuUsage { get; set; }
    public float RamUsage { get; set; }
}