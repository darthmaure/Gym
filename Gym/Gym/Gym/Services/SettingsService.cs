using Gym.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Gym.Services
{
    public class SettingsService : ISettingsService
    {
        private const string DailyLimtSettingsKey = "dailyLimiSettingValue";
        private const string MonthlyLimitSettingsKey = "monthlyLimitSettingValue";
        private const string EmailKey = "emailSettingValue";
        private const int _defaultDailyLimit = 10;
        private const int _defaultMonthlyLimit = _defaultDailyLimit * 30;

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public SettingsEntry Get()
        {
            return new SettingsEntry
            {
                DailyLimit = AppSettings.GetValueOrDefault(DailyLimtSettingsKey, _defaultDailyLimit),
                MonthlyLimit = AppSettings.GetValueOrDefault(MonthlyLimitSettingsKey, _defaultMonthlyLimit),
                Email = AppSettings.GetValueOrDefault(EmailKey, default(string))
            };
        }

        public void Save(SettingsEntry entry)
        {
            AppSettings.AddOrUpdateValue(DailyLimtSettingsKey, entry.DailyLimit);
            AppSettings.AddOrUpdateValue(MonthlyLimitSettingsKey, entry.MonthlyLimit);
            AppSettings.AddOrUpdateValue(EmailKey, entry.Email);
        }
    }
}
