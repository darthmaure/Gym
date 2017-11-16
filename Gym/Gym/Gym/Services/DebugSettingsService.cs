using Gym.Models;

namespace Gym.Services
{
    public class DebugSettingsService : ISettingsService
    {
        private static SettingsEntry _entry = new SettingsEntry { DailyLimit = 10, MonthlyLimit = 300 };

        public SettingsEntry Get()
        {
            return _entry;
        }

        public void Save(SettingsEntry entry)
        {
            _entry = entry;
        }
    }
}
