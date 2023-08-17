using System;
using System.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Plugin.Maui.AppRating;
using ShotTracker.ViewModels;

namespace ShotTracker.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel _viewModel;
        public AboutPage()
        {           
            InitializeComponent();
            BindingContext = _viewModel = new AboutViewModel();
        }
    }
}