using ShotTracker.Data;
using ShotTracker.Services;

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
                        var dbPath = Path.Combine(subFolderPath, "ShotEntries.db3");

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
