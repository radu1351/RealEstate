using SQLite;
namespace Imobiliare
{

    public partial class App : Application
    {
        private static DatabaseService databaseService;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        public static DatabaseService Database
        {
            get
            {
                if (databaseService == null)
                {
                    string dbPath;
                    if(DeviceInfo.Current.Platform == DevicePlatform.Android)
                        dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "houses.db3");
                    else 
                        dbPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.Parent.FullName, "houses.db3");
                    databaseService = new DatabaseService(dbPath);
                }
                return databaseService;
            }
        }
    }
}