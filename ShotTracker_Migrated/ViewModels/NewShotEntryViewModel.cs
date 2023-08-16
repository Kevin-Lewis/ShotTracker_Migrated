using ShotTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ShotTracker.ViewModels
{
    [QueryProperty(nameof(LocationQueryString), nameof(LocationQueryString))]
    [QueryProperty(nameof(Makes), nameof(Makes))]
    [QueryProperty(nameof(Misses), nameof(Misses))]
    [QueryProperty(nameof(UpdateID), nameof(UpdateID))]
    public class NewShotEntryViewModel : BaseViewModel
    {
        private int _makes;
        private int _misses;
        private ShotLocation _location;
        private string _locationQueryString;

        private int _updateID = -1;

        public NewShotEntryViewModel()
        {          
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return true;
        }

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
        
        public int UpdateID
        {
            get => _updateID;
            set => SetProperty(ref _updateID, value);
        }

        public ShotLocation Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public string LocationQueryString
        {
            get => _locationQueryString;
            set
            {
                if (_locationQueryString != value)
                {
                    _locationQueryString = value;
                    _location = (ShotLocation)int.Parse(_locationQueryString);
                }
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            if (UpdateID == -1)
            {
                ShotEntry newItem = new ShotEntry()
                {
                    Makes = Makes,
                    Misses = Misses,
                    Location = Location,
                    Date = DateTime.Now
                };
                await DataStore.AddShotEntryAsync(newItem);
            }
            else
            {
                await Task.Run(() => UpdateShotEntry());
            }               
            await Shell.Current.GoToAsync("..");
        }

        private async void UpdateShotEntry()
        {
            ShotEntry entry = DataStore.GetShotEntryAsync(UpdateID).Result;
            entry.Makes = Makes;
            entry.Misses = Misses;
            await DataStore.UpdateShotEntryAsync(entry);
        }

    }
}
