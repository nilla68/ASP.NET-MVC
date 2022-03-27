namespace Omnivus.Helpers
{
    public interface IMailService
    {
        void SendMail(string email, string subject, string message);
    }
}