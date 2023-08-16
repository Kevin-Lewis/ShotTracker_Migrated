using ShotTracker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShotTracker.Services
{
    public interface IDataStore
    {
        Task<bool> AddShotEntryAsync(ShotEntry item);
        Task<bool> UpdateShotEntryAsync(ShotEntry item);
        Task<bool> DeleteShotEntryAsync(ShotEntry item);
        Task<FilterSetting> GetFilterSettingAsync(int id);
        Task<bool> AddFilterSettingAsync(FilterSetting setting);
        Task<bool> UpdateFilterSettingAsync(FilterSetting setting);       
        Task<ShotEntry> GetShotEntryAsync(int id);
        Task<IEnumerable<ShotEntry>> GetShotEntriesAsync(bool forceRefresh = false);

        Task<UserData> GetUserDataAsync();
        Task<bool> AddUserDataAsync(UserData item);
        Task<bool> UpdateUserDataAsync(UserData item);
        Task<bool> DeleteUserDataAsync(UserData item);

        Task<bool> SaveOrUpdateUserDataAsync(UserData item);
    }
}
