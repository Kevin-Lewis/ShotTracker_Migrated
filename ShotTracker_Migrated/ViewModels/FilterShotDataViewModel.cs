using ShotTracker.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ShotTracker.ViewModels
{
    public class FilterShotDataViewModel : BaseViewModel
    {
        private string _filterRange;
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
                Task.Run(async () => await UpdateSetting(value));
            }
        }

        private async Task LoadSetting()
        {
            _filterRange = DataStore.GetFilterSettingAsync(1).Result?.Value;
            if (_filterRange is null)
            {
                await DataStore.AddFilterSettingAsync(new FilterSetting() { ID = 1, Value = "All" });
                _filterRange = "All";
            }
            OnPropertyChanged(nameof(SelectedFilterRange));
        }

        private async Task UpdateSetting(string value)
        {
            await DataStore.UpdateFilterSettingAsync(new FilterSetting() { ID = 1, Value = value });
        }
    }
}