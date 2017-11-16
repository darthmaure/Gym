using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Gym.Services;
using TinyIoC;
using Xamarin.Forms;

namespace Gym.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel(
            ISettingsService settingsService,
            IDataService dataService,
            IExportService exportService,
            IMailingService mailingService)
        {
            _settingsService = settingsService;
            _dataService = dataService;
            _exportService = exportService;
            _mailingService = mailingService;

            UpdateSettingsCommand = new Command(async () => await UpdateSettings());
            SendHistoryCommand = new Command(async () => await SendHistory());

            Task.Run(async () => await Init());
        }

        private ISettingsService _settingsService;
        private IDataService _dataService;
        private IExportService _exportService;
        private IMailingService _mailingService;

        private readonly string _mailTo = "lzo1@o2.pl";
        private readonly string _mailTitle = "Gym App - data backup";

        private int dailyLimit;

        public int DailyLimit
        {
            get { return dailyLimit; }
            set { SetProperty(ref dailyLimit, value); }
        }

        public ICommand UpdateSettingsCommand { get; }
        public ICommand SendHistoryCommand { get; }

        private async Task UpdateSettings()
        {
            if (IsBusy) return;
            IsBusy = true;

            await Task.Run(() =>
            {
               try
               {
                    _settingsService.Save(new Models.SettingsEntry { DailyLimit = DailyLimit });
               }
               finally
               {
                   IsBusy = false;
               }
            });
        }
        private async Task Init()
        {
            await Task.Delay(1);

            DailyLimit = _settingsService.Get().DailyLimit;
        }
        private async Task SendHistory()
        {
            if (IsBusy) return;
            IsBusy = true;

            await Task.Run(() =>
            {
                try
                {
                    var exported = _exportService.Export(_dataService);

                    _mailingService.Send(_mailTo, _mailTo, exported);
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }
    }
}
