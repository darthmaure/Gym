using Microcharts;
using System.Linq;
using TinyIoC;

namespace Gym.Services
{
    public class ChartService : IChartService
    {
        public ChartService()
        {
            _dataService = TinyIoCContainer.Current.Resolve<IDataService>();
            _colorService = TinyIoCContainer.Current.Resolve<IColorService>();
        }

        private readonly IDataService _dataService;
        private readonly IColorService _colorService;

        public Chart CreateLast7DaysChart(int limit)
        {
            var items = _dataService.Get()
                .OrderByDescending(d => d.Day)
                .Take(7)
                .Select(d => new Entry(d.Count)
                {
                    Color = _colorService.Resolve(d.Count, limit),
                    Label = $"{d.Day:yyyy-MM-dd}",
                    ValueLabel = d.Count.ToString()
                });

            return new LineChart
            {
                Entries = items
            };
        }
    }
}
