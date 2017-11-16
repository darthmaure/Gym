namespace Gym.Services
{
    public interface IMailingService
    {
        void Send(string to, string title, string content);
    }
}
