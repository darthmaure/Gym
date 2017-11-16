using System.Threading.Tasks;
using System.Windows.Input;
using Gym.Services;
using Microcharts;
using Xamarin.Forms;

namespace Gym.ViewModels
{
    public class CounterViewModel : BaseViewModel
    {
        public CounterViewModel(
            IDataService dataService,
            IChartService chartService,
            ISettingsService settingsService)
        {
            _dataService = dataService;
            _chartService = chartService;
            _settingsService = settingsService;

            IncrementCommand = new Command(async () => await IncrementAsync());

            Task.Run(async () => await Init());
        }

        private int dailyLimit;
        private int todayCounter;
        private Chart chart;
        private readonly IDataService _dataService;
        private readonly IChartService _chartService;
        private readonly ISettingsService _settingsService;

        public ICommand IncrementCommand { get; }

        public int DailyLimit
        {
            get { return dailyLimit; }
            set { SetProperty(ref dailyLimit, value); }
        }
        public int TodayCounter
        {
            get { return todayCounter; }
            set { SetProperty(ref todayCounter, value); }
        }
        public Chart Chart
        {
            get { return chart; }
            set { SetProperty(ref chart, value); }
        }

        private async Task IncrementAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            await Task.Run(() =>
            {
                try
                {
                    var today = _dataService.GetToday();
                    today.Count = (++TodayCounter);
                    _dataService.Update(today);

                    Chart = _chartService.CreateLast7DaysChart(DailyLimit);
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
            TodayCounter = _dataService.GetToday().Count;
            DailyLimit = _settingsService.Get().DailyLimit;

            Chart = _chartService.CreateLast7DaysChart(DailyLimit);
        }
    }
}
