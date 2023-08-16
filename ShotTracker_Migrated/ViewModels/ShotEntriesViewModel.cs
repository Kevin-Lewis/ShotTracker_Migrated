using ShotTracker.Models;
using ShotTracker.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Plugin.Maui.AppRating;
using ShotTracker.Services;

namespace ShotTracker.ViewModels
{
    [QueryProperty(nameof(Location), nameof(Location))]
    public class ShotEntriesViewModel : BaseViewModel
    {
        private ObservableCollection<ShotEntry> _shotEntries;
        private ShotEntry _selectedShotEntry;
        private string _location;
        private int _activeEntriesCount;
        private bool _shotAdd = false;

        private readonly IAppRating _appRating;
        private readonly IDispatcherService _dispatcherService;

        public ShotEntriesViewModel(IAppRating appRating, IDispatcherService dispatcherService)
        {
            _appRating = appRating;
            _dispatcherService = dispatcherService;

            AddShotEntryCommand = new Command(OnAddShotEntry);
            ShotEntries = new ObservableCollection<ShotEntry>();
        }

        public void OnAppearing()
        {
            Title = "Shot Entry";
            IsBusy = true;
            SelectedShotEntry = null;
            _shotAdd = false;
        }
        public void OnDisappearing()
        {
            Task.Run(async () => await CheckAndPromptForReviewAsync());
        }

        public ObservableCollection<ShotEntry> ShotEntries
        {
            get => _shotEntries;
            set
            {
                _shotEntries = value;
                OnPropertyChanged(nameof(ShotEntries));
                OnPropertyChanged(nameof(OverallPercentage));
            }
        }
        public string Location
        {
            get => _location;
            set
            {               
                if (_location != value)
                {
                    _location = value;                                    
                }               
            }
        }

        public string OverallPercentage
        {
            get
            {
                if (ShotEntries.Count == 0)
                {
                    return string.Empty;
                }
                return $"{ShotEntries.Sum(item => item.Makes)} / {ShotEntries.Sum(item => item.Makes + item.Misses)}";
            }
        }

        public Command AddShotEntryCommand { get; set; }
        
        public ShotEntry SelectedShotEntry
        {
            get => _selectedShotEntry;
            set
            {
                _selectedShotEntry = value;
                OnShotEntrySelected(value);
                OnPropertyChanged(nameof(SelectedShotEntry));
                if (_selectedShotEntry is null)
                {
                    Task.Run(() => ShotEntries = new ObservableCollection<ShotEntry>(DataStore.GetShotEntriesAsync(true).Result.Where(o => o.Location == (ShotLocation)int.Parse(_location))));
                }                
            }
        }

        private async void OnAddShotEntry(object obj)
        {
            _shotAdd = true;
            await Shell.Current.GoToAsync($"{nameof(NewShotEntryPage)}?{nameof(NewShotEntryViewModel.LocationQueryString)}={Location}");
        }

        private async void OnShotEntrySelected(ShotEntry entry)
        {
            if (entry == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ShotEntryDetailPage)}?{nameof(ShotEntryDetailViewModel.ID)}={entry.ID}");
        }

        private async Task CheckAndPromptForReviewAsync()
        {
            var userData = await DataStore.GetUserDataAsync();
            if (userData.ReviewShown || _shotAdd)
                return;

            _activeEntriesCount = (await DataStore.GetShotEntriesAsync()).ToList().Count;

            if (_activeEntriesCount >= 5)
            {
                userData.ReviewShown = true;
                await DataStore.SaveOrUpdateUserDataAsync(userData);
                await RateAppAsync();               
            }
        }

        private async Task RateAppAsync()
        {
            await _dispatcherService.DispatchAsync(async () =>
            {
                await _appRating.PerformInAppRateAsync();
            });
        }
    }
}