using System;
using System.Windows.Input;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ShotTracker.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public Command TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
        public AboutViewModel()
        {
            Title = "About";
        }
    }
}