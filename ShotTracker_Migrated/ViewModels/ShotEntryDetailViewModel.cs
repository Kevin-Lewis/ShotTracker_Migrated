using ShotTracker.Models;
using ShotTracker.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ShotTracker.ViewModels
{
    [QueryProperty(nameof(ID), nameof(ID))]
    public class ShotEntryDetailViewModel : BaseViewModel
    {
        private int _shotEntryId;
        private int _makes;
        private int _misses;
        private ShotLocation _location;
        private DateTime _date;

        private ShotEntryDetailPage _parent;

        public ShotEntryDetailViewModel(ShotEntryDetailPage parent)
        {
            _parent = parent;

            EditShotEntryCommand = new Command(OnEditShotEntry);
            DeleteShotEntryCommand = new Command(OnDeleteShotEntry);
        }

        public void OnAppearing()
        {
            LoadItemId(ID);
        }

        public Command EditShotEntryCommand { get; set; }
        public Command DeleteShotEntryCommand { get; set; }

        public int Makes
        {
            get => _makes;
            set => SetProperty(ref _makes, value);
        }

        public int Misses
        {
            get => _misses;
            set => SetProperty(ref _misses, value);
        }

        public string TextResult
        {
            get
            {
                return $"{Makes}/{Makes + Misses}";
            }
        }

        public ShotLocation Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public int ID
        {
            get
            {
                return _shotEntryId;
            }
            set
            {
                if (_shotEntryId == value)
                {
                    return;
                }
                LoadItemId(value);
                _shotEntryId = value;
            }
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetShotEntryAsync(itemId);
                ID = item.ID;
                Makes = item.Makes;
                Misses = item.Misses;
                Location = item.Location;
                Date = item.Date;
                OnPropertyChanged(nameof(TextResult));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnEditShotEntry(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewShotEntryPage)}?{nameof(NewShotEntryViewModel.LocationQueryString)}={(int)Location}&&{nameof(NewShotEntryViewModel.Makes)}={Makes}&&{nameof(NewShotEntryViewModel.Misses)}={Misses}&&{nameof(NewShotEntryViewModel.UpdateID)}={ID}");
        }

        private async void OnDeleteShotEntry(object obj)
        {
            bool deleteEntry = await _parent.DisplayAlert("Delete Entry", "Are you sure you want to permanently delete this entry?", "Yes", "No");

            if (deleteEntry)
            {
                await Task.Run(() => DeleteShotEntry());
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void DeleteShotEntry()
        {
            ShotEntry entry = DataStore.GetShotEntryAsync(ID).Result;
            await DataStore.DeleteShotEntryAsync(entry);
        }
    }
}
