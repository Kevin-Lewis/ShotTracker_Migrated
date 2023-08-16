using ShotTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShotTracker.Services
{
    public class MockDataStore : IDataStore
    {
        readonly List<ShotEntry> shotEntries;
        readonly FilterSetting setting;

        public MockDataStore()
        {
            shotEntries = new List<ShotEntry>()
            {
                new ShotEntry { Makes = 0, Misses = 0, Location = (ShotLocation)0, Date=DateTime.Now },
                new ShotEntry { Makes = 10, Misses = 5, Location = (ShotLocation)1, Date=DateTime.Now },
                new ShotEntry { Makes = 42, Misses = 32, Location = (ShotLocation)2, Date=DateTime.Now },
                new ShotEntry { Makes = 3, Misses = 7, Location = (ShotLocation)3, Date=DateTime.Now },
                new ShotEntry { Makes = 1, Misses = 0, Location = (ShotLocation)4, Date=DateTime.Now },
                new ShotEntry { Makes = 0, Misses = 3, Location = (ShotLocation)5, Date=DateTime.Now },
                new ShotEntry { Makes = 300, Misses = 333, Location = (ShotLocation)6, Date=DateTime.Now },
                new ShotEntry { Makes = 97, Misses = 100, Location = (ShotLocation)7, Date=DateTime.Now },
                new ShotEntry { Makes = 245, Misses = 300, Location = (ShotLocation)8, Date=DateTime.Now },
                new ShotEntry { Makes = 5, Misses = 2, Location = (ShotLocation)9, Date=DateTime.Now },
                new ShotEntry { Makes = 40, Misses = 42, Location = (ShotLocation)10, Date=DateTime.Now }
            };

            setting = new FilterSetting() { ID = 1, Value = "Today" };
        }

        public async Task<bool> AddShotEntryAsync(ShotEntry item)
        {
            shotEntries.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateShotEntryAsync(ShotEntry item)
        {
            var oldItem = shotEntries.Where((ShotEntry arg) => arg.ID == item.ID).FirstOrDefault();
            shotEntries.Remove(oldItem);
            shotEntries.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteShotEntryAsync(ShotEntry item)
        {
            shotEntries.Remove(item);
            return await Task.FromResult(true);
        }

        public async Task<ShotEntry> GetShotEntryAsync(int id)
        {
            return await Task.FromResult(shotEntries.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<ShotEntry>> GetShotEntriesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(shotEntries);
        }

        public async Task<bool> AddFilterSettingAsync(FilterSetting item)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateFilterSettingAsync(FilterSetting item)
        {
            return await Task.FromResult(true);
        }

        public async Task<FilterSetting> GetFilterSettingAsync(int id)
        {
            return await Task.FromResult(setting);
        }

        public Task<UserData> GetUserDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserDataAsync(UserData item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserDataAsync(UserData item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserDataAsync(UserData item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveOrUpdateUserDataAsync(UserData item)
        {
            throw new NotImplementedException();
        }
    }
}