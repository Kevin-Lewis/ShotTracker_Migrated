using ShotTracker.Models;
using ShotTracker.Views;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ShotTracker.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<ShotEntry> ShotEntries { get; }
        public Command ZonePressedCommand { get; }
        public ShotLocation Location { get; }
        public Command FilterShotDataCommand { get; set; }
        public string Paint { get; set; } = "-";
        public string ShortLeft { get; set; } = "-";
        public string LeftElbow { get; set; } = "-";
        public string FreeThrow { get; set; } = "-";
        public string RightElbow { get; set; } = "-";
        public string ShortRight { get; set; } = "-";
        public string LeftCorner { get; set; } = "-";
        public string LeftWing { get; set; } = "-";
        public string TopKey { get; set; } = "-";
        public string RightWing { get; set; } = "-";
        public string RightCorner { get; set; } = "-";

        public Color PaintBackground { get { return GetBackgroundColor(Paint); } }
        public Color ShortLeftBackground { get { return GetBackgroundColor(ShortLeft); } }
        public Color LeftElbowBackground { get { return GetBackgroundColor(LeftElbow); } }
        public Color FreeThrowBackground { get { return GetBackgroundColor(FreeThrow); } }
        public Color RightElbowBackground { get { return GetBackgroundColor(RightElbow); } }
        public Color ShortRightBackground { get { return GetBackgroundColor(ShortRight); } }
        public Color LeftCornerBackground { get { return GetBackgroundColor(LeftCorner); } }
        public Color LeftWingBackground { get { return GetBackgroundColor(LeftWing); } }
        public Color TopKeyBackground { get { return GetBackgroundColor(TopKey); } }
        public Color RightWingBackground { get { return GetBackgroundColor(RightWing); } }
        public Color RightCornerBackground { get { return GetBackgroundColor(RightCorner); } }

        public HomeViewModel()
        {
            Title = "Home";
            ShotEntries = new ObservableCollection<ShotEntry>();
            FilterShotDataCommand = new Command(OnFilterShotData);
            ZonePressedCommand = new Command<ShotLocation>(OnZoneSelected);
        }

        private async Task LoadItems()
        {
            try
            {
                ShotEntries.Clear();
                FilterSetting setting = await DataStore.GetFilterSettingAsync(1);
                if (setting is null) { setting = new FilterSetting() { ID = 1, Value = "All" };}
                var items = (await DataStore.GetShotEntriesAsync(true)).Where(x => x.Date >= ReturnFilterDate(setting));
                foreach (var item in items)
                {
                    ShotEntries.Add(item);
                }
                SetZoneText();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private DateTime ReturnFilterDate(FilterSetting setting)
        {
            DateTime result = new DateTime();

            switch (setting.Value)
            {
                case "All":
                    result = DateTime.MinValue;
                    break;
                case "Last 90 Days":
                    result = DateTime.Today.AddDays(-89);
                    break;
                case "Last 30 Days":
                    result = DateTime.Today.AddDays(-29);
                    break;
                case "Last 7 Days":
                    result = DateTime.Today.AddDays(-6);
                    break;
                case "This Year":
                    result = DateTime.Parse($"1-1-{DateTime.Today.Year}");
                    break;
                case "This Month":
                    result = DateTime.Parse($"{DateTime.Today.Month}-1-{DateTime.Today.Year}");
                    break;
                case "This Week":
                    result = DateTime.Parse($"{DateTime.Today.Month}-{DateTime.Today.Day + (0 - DateTime.Today.DayOfWeek)}-{DateTime.Today.Year}");
                    break;
                case "Today":
                    result = DateTime.Today;
                    break;
            }

            return result;
        } 

        public void OnAppearing()
        {
            IsBusy = true;
            Task.Run(async () => await LoadItems());
        }

        private void SetZoneText()
        {
            Paint = ShotEntries.Where(o => o.Location == ShotLocation.Paint).ToList().Count > 0 ? $"{Math.Round((double)ShotEntries.Where(o => o.Location == ShotLocation.Paint).Sum(item => item.Makes) / (double)ShotEntries.Where(o => o.Location == ShotLocation.Paint).Sum(item => item.Makes + item.Misses) * 100)}%" : "-";
            ShortLeft = ShotEntries.Where(o => o.Location == ShotLocation.ShortLeft).ToList().Count > 0 ? $"{Math.Round((double)ShotEntries.Where(o => o.Location == ShotLocation.ShortLeft).Sum(item => item.Makes) / (double)ShotEntries.Where(o => o.Location == ShotLocation.ShortLeft).Sum(item => item.Makes + item.Misses) * 100)}%" : "-";
            LeftElbow = ShotEntries.Where(o => o.Location == ShotLocation.LeftElbow).ToList().Count > 0 ? $"{Math.Round((double)ShotEntries.Where(o => o.Location == ShotLocation.LeftElbow).Sum(item => item.Makes) / (double)ShotEntries.Where(o => o.Location == ShotLocation.LeftElbow).Sum(item => item.Makes + item.Misses) * 100)}%" : "-";
            FreeThrow = ShotEntries.Where(o => o.Location == ShotLocation.FreeThrow).ToList().Count > 0 ? $"{Math.Round((double)ShotEntries.Where(o => o.Location == ShotLocation.FreeThrow).Sum(item => item.Makes) / (double)ShotEntries.Where(o => o.Location == ShotLocation.FreeThrow).Sum(item => item.Makes + item.Misses) * 100)}%" : "-";
            RightElbow = ShotEntries.Where(o => o.Location == ShotLocation.RightElbow).ToList().Count > 0 ? $"{Math.Round((double)ShotEntries.Where(o => o.Location == ShotLocation.RightElbow).Sum(item => item.Makes) / (double)ShotEntries.Where(o => o.Location == ShotLocation.RightElbow).Sum(item => item.Makes + item.Misses) * 100)}%" : "-";
            ShortRight = ShotEntries.Where(o => o.Location == ShotLocation.ShortRight).ToList().Count > 0 ? $"{Math.Round((double)ShotEntries.Where(o => o.Location == ShotLocation.ShortRight).Sum(item => item.Makes) / (double)ShotEntries.Where(o => o.Location == ShotLocation.ShortRight).Sum(item => item.Makes + item.Misses) * 100)}%" : "-";
            LeftCorner = ShotEntries.Where(o => o.Location == ShotLocation.LeftCorner).ToList().Count > 0 ? $"{Math.Round((double)ShotEntries.Where(o => o.Location == ShotLocation.LeftCorner).Sum(item => item.Makes) / (double)ShotEntries.Where(o => o.Location == ShotLocation.LeftCorner).Sum(item => item.Makes + item.Misses) * 100)}%" : "-";
            LeftWing = ShotEntries.Where(o => o.Location == ShotLocation.LeftWing).ToList().Count > 0 ? $"{Math.Round((double)ShotEntries.Where(o => o.Location == ShotLocation.LeftWing).Sum(item => item.Makes) / (double)ShotEntries.Where(o => o.Location == ShotLocation.LeftWing).Sum(item => item.Makes + item.Misses) * 100)}%" : "-";
            TopKey = ShotEntries.Where(o => o.Location == ShotLocation.TopKey).ToList().Count > 0 ? $"{Math.Round((double)ShotEntries.Where(o => o.Location == ShotLocation.TopKey).Sum(item => item.Makes) / (double)ShotEntries.Where(o => o.Location == ShotLocation.TopKey).Sum(item => item.Makes + item.Misses) * 100)}%" : "-";
            RightWing = ShotEntries.Where(o => o.Location == ShotLocation.RightWing).ToList().Count > 0 ? $"{Math.Round((double)ShotEntries.Where(o => o.Location == ShotLocation.RightWing).Sum(item => item.Makes) / (double)ShotEntries.Where(o => o.Location == ShotLocation.RightWing).Sum(item => item.Makes + item.Misses) * 100)}%" : "-";
            RightCorner = ShotEntries.Where(o => o.Location == ShotLocation.RightCorner).ToList().Count > 0 ? $"{Math.Round((double)ShotEntries.Where(o => o.Location == ShotLocation.RightCorner).Sum(item => item.Makes) / (double)ShotEntries.Where(o => o.Location == ShotLocation.RightCorner).Sum(item => item.Makes + item.Misses) * 100)}%" : "-";

            OnPropertyChanged(nameof(Paint));
            OnPropertyChanged(nameof(ShortLeft));
            OnPropertyChanged(nameof(LeftElbow));
            OnPropertyChanged(nameof(FreeThrow));
            OnPropertyChanged(nameof(RightElbow));
            OnPropertyChanged(nameof(ShortRight));
            OnPropertyChanged(nameof(LeftCorner));
            OnPropertyChanged(nameof(LeftWing));
            OnPropertyChanged(nameof(TopKey));
            OnPropertyChanged(nameof(RightWing));
            OnPropertyChanged(nameof(RightCorner));

            OnPropertyChanged(nameof(PaintBackground));
            OnPropertyChanged(nameof(ShortLeftBackground));
            OnPropertyChanged(nameof(LeftElbowBackground));
            OnPropertyChanged(nameof(FreeThrowBackground));
            OnPropertyChanged(nameof(RightElbowBackground));
            OnPropertyChanged(nameof(ShortRightBackground));
            OnPropertyChanged(nameof(LeftCornerBackground));
            OnPropertyChanged(nameof(LeftWingBackground));
            OnPropertyChanged(nameof(TopKeyBackground));
            OnPropertyChanged(nameof(RightWingBackground));
            OnPropertyChanged(nameof(RightCornerBackground));
        }

        private Color GetBackgroundColor(string text)
        {
            if (text != "-")
            {
                double percentage = double.Parse(text.Trim('%'));
                switch (percentage)
                {
                    case var expression when percentage == 100:
                        return Color.FromHex("#00FF00");
                    case var expression when percentage > 95:
                        return Color.FromHex("#12FF00");
                    case var expression when percentage > 90:
                        return Color.FromHex("#24FF00");
                    case var expression when percentage > 85:
                        return Color.FromHex("#47FF00");
                    case var expression when percentage > 80:
                        return Color.FromHex("#6AFF00");
                    case var expression when percentage > 75:
                        return Color.FromHex("#7CFF00");
                    case var expression when percentage > 70:
                        return Color.FromHex("#9FFF00");
                    case var expression when percentage > 65:
                        return Color.FromHex("#B0FF00");
                    case var expression when percentage > 60:
                        return Color.FromHex("#D4FF00");
                    case var expression when percentage > 55:
                        return Color.FromHex("#E5FF00");
                    case var expression when percentage > 50:
                        return Color.FromHex("#F7FF00");
                    case var expression when percentage > 45:
                        return Color.FromHex("#FFE400");
                    case var expression when percentage > 40:
                        return Color.FromHex("#FFC100");
                    case var expression when percentage > 35:
                        return Color.FromHex("#FFAF00");
                    case var expression when percentage > 30:
                        return Color.FromHex("#FF8C00");
                    case var expression when percentage > 25:
                        return Color.FromHex("#FF7B00");
                    case var expression when percentage > 20:
                        return Color.FromHex("#FF5700");
                    case var expression when percentage > 15:
                        return Color.FromHex("#FF4600");
                    case var expression when percentage > 10:
                        return Color.FromHex("#FF2300");
                    case var expression when percentage > 5:
                        return Color.FromHex("#FF1100");
                    default:
                        return Color.FromHex("#FF0000");
                }
            }
            else
            {
                return Color.FromHex("#D3D3D3");
            }
        }

        private async void OnFilterShotData(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(FilterShotDataPage)}");
        }

        async void OnZoneSelected(ShotLocation location)
        {
            await Shell.Current.GoToAsync($"{nameof(ShotEntriesPage)}?{nameof(ShotEntriesViewModel.Location)}={(int)location}");
        }
    }
}