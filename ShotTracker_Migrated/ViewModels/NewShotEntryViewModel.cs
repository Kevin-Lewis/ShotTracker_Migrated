using ShotTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using ShotTracker.Enums;

namespace ShotTracker.ViewModels
{
    [QueryProperty(nameof(QueryString), nameof(QueryString))]
    public class NewShotEntryViewModel : BaseViewModel
    {
        private int _makes;
        private int _misses;
        private ShotLocation _location;
        private CourtType _courtType;

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

        public string QueryString
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var parts = value.Split(',');
                    if (parts.Length == 1)
                    {
                        Location = (ShotLocation)int.Parse(parts[0]);
                        CourtType = CourtType.Unspecified;
                    }
                    else if (parts.Length == 5)
                    {
                        Location = (ShotLocation)int.Parse(parts[0]);
                        Makes = int.Parse(parts[1]);
                        Misses = int.Parse(parts[2]);
                        CourtType = (CourtType)int.Parse(parts[3]);
                        UpdateID = int.Parse(parts[4]);
                    }
                }
            }
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

        public CourtType CourtType
        {
            get => _courtType;
            set => SetProperty(ref _courtType, value);
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
                    CourtType = CourtType,
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
            entry.CourtType = CourtType;
            await DataStore.UpdateShotEntryAsync(entry);
        }

    }
}
