namespace Omnivus.Helpers
{
    public class MockMailService : IMailService
    {
        private readonly ILogger<MockMailService> _logger;

        public MockMailService(ILogger<MockMailService> logger)
        {
            _logger = logger;
        }

        public void SendMail(string email, string subject, string message)
        {
            _logger.LogInformation($"{email}{Environment.NewLine}{subject}{Environment.NewLine}{message}");
        }
    }
}
