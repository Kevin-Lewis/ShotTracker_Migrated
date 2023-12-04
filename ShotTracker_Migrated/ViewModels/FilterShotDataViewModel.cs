using ShotTracker.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using ShotTracker_Migrated.Enums;

namespace ShotTracker.ViewModels
{
    public class FilterShotDataViewModel : BaseViewModel
    {
        private string _filterRange;
        private string _courtType;
        public FilterShotDataViewModel()
        {
            Title = "Filter";
        }

        public void OnAppearing()
        {
            Task.Run(async () => await LoadSetting());
        }

        public string SelectedFilterRange
        {
            get
            {
                return _filterRange;
            }
            set
            {
                _filterRange = value;
                Task.Run(async () => await UpdateSetting(FilterType.TimeInterval, value));
            }
        }

        public string SelectedCourtType
        {
            get
            {
                return _courtType;
            }
            set
            {
                _courtType = value;
                Task.Run(async () => await UpdateSetting(FilterType.CourtType, value));
            }
        }

        private async Task LoadSetting()
        {
            _filterRange = DataStore.GetFilterSettingAsync((int)FilterType.TimeInterval).Result?.Value;
            _courtType = DataStore.GetFilterSettingAsync((int)FilterType.CourtType).Result?.Value;
            if (_filterRange is null)
            {
                await DataStore.AddFilterSettingAsync(new FilterSetting() { ID = (int)FilterType.TimeInterval, Value = "All" });
                _filterRange = "All";
            }
            if (_courtType is null)
            {
                await DataStore.AddFilterSettingAsync(new FilterSetting() { ID = (int)FilterType.CourtType, Value = "All" });
                _courtType = "All";
            }
            OnPropertyChanged(nameof(SelectedFilterRange));
            OnPropertyChanged(nameof(SelectedCourtType));
        }

        private async Task UpdateSetting(FilterType filterType, string value)
        {
            int id = (int)filterType;
            await DataStore.UpdateFilterSettingAsync(new FilterSetting() { ID = id, Value = value });
        }
    }
}