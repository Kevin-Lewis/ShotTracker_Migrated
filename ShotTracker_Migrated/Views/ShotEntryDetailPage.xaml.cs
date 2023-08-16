using ShotTracker.ViewModels;
using System.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ShotTracker.Views
{
    public partial class ShotEntryDetailPage : ContentPage
    {
        ShotEntryDetailViewModel _viewModel;
        public ShotEntryDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ShotEntryDetailViewModel(this);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}