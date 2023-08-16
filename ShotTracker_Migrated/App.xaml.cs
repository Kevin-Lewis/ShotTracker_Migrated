using ShotTracker.Data;
using ShotTracker.Services;
using ShotTracker.Views;
using System;
using System.IO;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using ShotTracker;

namespace ShotTracker
{
    public partial class App : Application
    {
        static ShotEntryDatabase database;

        public static ShotEntryDatabase Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        string subFolderPath = Path.Combine(folderPath, ".local", "share");
                        string dbPath = Path.Combine(subFolderPath, "ShotEntries.db3");

                        Directory.CreateDirectory(subFolderPath);

                        database = new ShotEntryDatabase(dbPath);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            DependencyService.Register<DataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
