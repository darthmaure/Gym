using System.Threading.Tasks;
using System.Windows.Input;
using Gym.Services;
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

        private readonly ISettingsService _settingsService;
        private readonly IDataService _dataService;
        private readonly IExportService _exportService;
        private readonly IMailingService _mailingService;

        private readonly string _mailTitle = "Gym App - data backup";

        private int dailyLimit;
        private string email;

        public int DailyLimit
        {
            get { return dailyLimit; }
            set { SetProperty(ref dailyLimit, value); }
        }

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
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

            var settings = _settingsService.Get();
            DailyLimit = settings.DailyLimit;
            Email = settings.Email;

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

                    _mailingService.Send(Email, _mailTitle, exported);
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }
    }
}
