using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gym.Models;
using SQLite;

namespace Gym.Services
{
    public class DataService : IDataService
    {
        private readonly string _dbName = "gym_data.db3";
        private readonly SQLiteConnection _database;

        public DataService()
        {
            var path = GetLocation();

            _database = new SQLiteConnection(path);
            _database.CreateTable<DailyEntry>();
        }

        public IList<DailyEntry> Get()
        {
            return _database.Query<DailyEntry>("SELECT * FROM DailyEntry").ToList();
        }

        public DailyEntry GetToday()
        {
            return GetTodayItem() ?? CreateNew();
        }

        public void Update(DailyEntry entry)
        {
            var item = GetTodayItem();
            if (item == null)
            {
                item = CreateNew();
                item.Count = entry.Count;
                _database.Insert(item);
            }
            else
            {
                item.Count = entry.Count;
                _database.Update(item);
            }
        }

        private DailyEntry CreateNew() => new DailyEntry { Day = DateTime.Today, Count = 0 };
        private DailyEntry GetTodayItem()
        {
            var today = DateTime.Today;
            var tomorrow = DateTime.Today.AddDays(1);

            return _database
                .Table<DailyEntry>()
                .FirstOrDefault(d => d.Day >= today && d.Day < tomorrow);
        }
        private string GetLocation()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbName);
        }
    }
}
