#define TEST

using Gym.Services;
using Gym.ViewModels;
using TinyIoC;

namespace Gym
{
    public class Bootstrap
    {
        public void Init()
        {
            var container = TinyIoCContainer.Current;

            container.Register<IChartService, ChartService>();
            container.Register<IColorService, ColorService>();
            container.Register<IExportService, ExportService>();
            container.Register<IMailingService, MailingService>();

#if TEST
            container.Register<IDataService, DebugDataService>();
            container.Register<ISettingsService, DebugSettingsService>();
#else
            container.Register<IDataService, DataService>();
            container.Register<ISettingsService, SettingsService>();
#endif

            container.Register<CounterViewModel>();
            container.Register<SettingsViewModel>();
        }
    }
}
