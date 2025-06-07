namespace StockExchangeAPI.Interfaces
{
    public interface ILoggerService
    {
        public void LogException(Exception ex);
        public void LogWarning(string warningMessage);

        public void LogInformation(string infoMessage);
    }
}
