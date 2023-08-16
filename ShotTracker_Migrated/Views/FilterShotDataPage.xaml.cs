using ShotTracker.ViewModels;
using System;
using System.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ShotTracker.Views
{
    public partial class FilterShotDataPage : ContentPage
    {
        FilterShotDataViewModel _viewModel;
        public FilterShotDataPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FilterShotDataViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}