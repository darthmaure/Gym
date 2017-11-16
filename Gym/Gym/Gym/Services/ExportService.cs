using System.Text;
using Gym.Models;

namespace Gym.Services
{
    public class ExportService : IExportService
    {
        public string Export(IDataService dataService)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{nameof(DailyEntry.Id)};{nameof(DailyEntry.Day)};{nameof(DailyEntry.Count)}");

            foreach (var item in dataService.Get())
            {
                builder.AppendLine($"{item.Id};{item.Day:yyyy-MM-dd};{item.Count}");
            }

            return builder.ToString();
        }
    }
}
