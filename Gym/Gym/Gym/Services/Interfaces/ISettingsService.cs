using Gym.Models;

namespace Gym.Services
{
    public interface ISettingsService
    {
        SettingsEntry Get();
        void Save(SettingsEntry entry);
    }
}
