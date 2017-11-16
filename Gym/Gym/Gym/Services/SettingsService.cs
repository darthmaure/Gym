using System;
using System.Collections.Generic;
using System.Text;
using Gym.Models;
using Plugin.Settings;

namespace Gym.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsEntry Get()
        {
            throw new NotImplementedException();
        }

        public void Save(SettingsEntry entry)
        {
            throw new NotImplementedException();
            //AppSettings.AddOrUpdateValue(DailyLimtSettingsKey, value);
            //AppSettings.AddOrUpdateValue(MonthlyLimitSettingsKey, value);
        }
    }
}
