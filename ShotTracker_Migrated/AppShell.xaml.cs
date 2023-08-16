using ShotTracker.ViewModels;
using ShotTracker.Views;
using System;
using System.Collections.Generic;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ShotTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ShotEntriesPage), typeof(ShotEntriesPage));
            Routing.RegisterRoute(nameof(ShotEntryDetailPage), typeof(ShotEntryDetailPage));
            Routing.RegisterRoute(nameof(NewShotEntryPage), typeof(NewShotEntryPage));
            Routing.RegisterRoute(nameof(FilterShotDataPage), typeof(FilterShotDataPage));
        }

    }
}
