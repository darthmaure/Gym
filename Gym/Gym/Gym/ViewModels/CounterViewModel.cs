using System.Threading.Tasks;
using System.Windows.Input;
using Gym.Services;
using Microcharts;
using TinyIoC;
using Xamarin.Forms;

namespace Gym.ViewModels
{
    public class CounterViewModel : BaseViewModel
    {
        public CounterViewModel()
        {
            _dataService = TinyIoCContainer.Current.Resolve<IDataService>();
            _chartService = TinyIoCContainer.Current.Resolve<IChartService>();
            _settingsService = TinyIoCContainer.Current.Resolve<ISettingsService>();

            IncrementCommand = new Command(async () => await IncrementAsync());

            Task.Run(async () => await Init());
        }

        private int dailyLimit;
        private int todayCounter;
        private Chart chart;
        private IDataService _dataService;
        private IChartService _chartService;
        private ISettingsService _settingsService;

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
