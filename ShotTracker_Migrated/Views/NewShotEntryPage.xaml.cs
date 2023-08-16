using ShotTracker.Models;
using ShotTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ShotTracker.Views
{
    public partial class NewShotEntryPage : ContentPage
    {
        public NewShotEntryPage()
        {
            InitializeComponent();
            BindingContext = new NewShotEntryViewModel();
        }
    }
}