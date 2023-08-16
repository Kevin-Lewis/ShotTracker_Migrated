using ShotTracker.Models;
using ShotTracker.ViewModels;
using ShotTracker.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Plugin.Maui.AppRating;
using ShotTracker.Services;

namespace ShotTracker.Views
{
    public partial class ShotEntriesPage : ContentPage
    {
        private ShotEntriesViewModel _viewModel;

        public ShotEntriesPage(IAppRating appRating, IDispatcherService dispatcherService)
        {
            InitializeComponent();
            BindingContext = _viewModel = new ShotEntriesViewModel(appRating, dispatcherService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.OnDisappearing();
        }
    }
}