using ShotTracker.Models;
using ShotTracker_Migrated.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShotTracker.Data
{
    public class ShotEntryDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ShotEntryDatabase(string dbPath)
        {
            try
            {
                database = new SQLiteAsyncConnection(dbPath);
                database.CreateTableAsync<ShotEntry>().Wait();
                database.CreateTableAsync<FilterSetting>().Wait();
                database.CreateTableAsync<UserData>().Wait();
                database.CreateTableAsync<SoloChallenge>().Wait();
            }
            catch(Exception ex) 
            {

            }           
        }

        public Task<List<ShotEntry>> GetShotEntriesAsync()
        {
            return database.Table<ShotEntry>().ToListAsync();
        }

        public Task<ShotEntry> GetShotEntryAsync(int id)
        {
            return database.Table<ShotEntry>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        public Task<FilterSetting> GetFilterSettingAsync(int id)
        {
            return database.Table<FilterSetting>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        public Task<int> SaveShotDataFilter(FilterSetting setting)
        {
            return database.InsertAsync(setting);
        }

        public Task<int> UpdateShotDataFilter(FilterSetting setting)
        {
            return database.UpdateAsync(setting);
        }

        public Task<int> SaveShotEntryAsync(ShotEntry entry)
        {
            return database.InsertAsync(entry);
        }

        public Task<int> UpdateShotEntryAsync(ShotEntry entry)
        {
            return database.UpdateAsync(entry);
        }

        public Task<int> DeleteShotEntryAsync(ShotEntry entry)
        {
            return database.DeleteAsync(entry);
        }

        public async Task<UserData> GetUserDataAsync()
        {
            var userData = await database.Table<UserData>().FirstOrDefaultAsync();

            if (userData == null)
            {
                userData = new UserData();
                await database.InsertAsync(userData);
            }

            return userData;
        }


        public Task<int> SaveUserDataAsync(UserData entry)
        {
            return database.InsertAsync(entry);
        }

        public Task<int> UpdateUserDataAsync(UserData entry)
        {
            return database.UpdateAsync(entry);
        }

        public Task<int> DeleteUserDataAsync(UserData entry)
        {
            return database.DeleteAsync(entry);
        }

        public async Task<int> SaveOrUpdateUserDataAsync(UserData entry)
        {
            var existingEntry = await GetUserDataAsync();

            if (existingEntry != null)
            {
                entry.ID = existingEntry.ID;
                return await UpdateUserDataAsync(entry);
            }
            else
            {
                return await SaveUserDataAsync(entry);
            }
        }
    }
}