using System.Collections.Generic;
using Gym.Models;

namespace Gym.Services
{
    public interface IDataService
    {
        DailyEntry GetToday();
        IList<DailyEntry> Get();
        void Update(DailyEntry entry);
    }
}
