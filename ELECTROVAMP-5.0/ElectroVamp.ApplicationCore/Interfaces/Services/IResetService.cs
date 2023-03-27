namespace ElectroVamp.ApplicationCore.Interfaces.Services
{
    public interface IResetService
    {
        public Task<bool> CheckResetNow();
        public event EventHandler ResetTimeOccurred;
    }
}
