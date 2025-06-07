using StockExchangeAPI.Interfaces;

namespace StockExchangeAPI.Services
{
    public class LoggerService : ILoggerService
    {
        public void LogException(Exception ex)
        {
            Console.WriteLine($"Exception occured: {ex.Message}");
            //Other cusotm logic
        }

        public void LogInformation(string infoMessage)
        {
            Console.WriteLine($"Information: {infoMessage}");
            //Other cusotm logic
        }

        public void LogWarning(string warningMessage)
        {
            Console.WriteLine($"Warning!: {warningMessage}");
            //Other cusotm logic
        }
    }
}
