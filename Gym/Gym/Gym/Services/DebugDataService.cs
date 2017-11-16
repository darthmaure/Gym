using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gym.Models;

namespace Gym.Services
{
    public class DebugDataService : IDataService
    {
        public DebugDataService()
        {
            Init();
        }

        private static List<DailyEntry> entries;

        public IList<DailyEntry> Get() => entries;

        public DailyEntry GetToday() => entries.First(d => d.Day == DateTime.Today);

        public void Update(DailyEntry entry)
        {
            var tmp = entries.First(d => d.Day == entry.Day);
            tmp.Count = entry.Count;
        }

        private static void Init()
        {
            if (entries == null || !entries.Any())
            {
                var rand = new Random(DateTime.Now.GetHashCode());
                entries = Enumerable
                    .Range(0, 10)
                    .Select(d => new DailyEntry
                    {
                        Id = d + 1,
                        Count = rand.Next(1, 8),
                        Day = DateTime.Today.AddDays((-1) * d)
                    })
                    .ToList();
            }
        }
    }
}
