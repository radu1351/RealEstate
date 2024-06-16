using SQLite;
namespace Imobiliare
{

    public partial class App : Application
    {
        private static DatabaseService databaseService;
        private static readonly string DbPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.Parent.FullName, "houses.db3");

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        public static DatabaseService Database
        {
            get
            {
                if (databaseService == null)
                {
                    try
                    {
                        databaseService = new DatabaseService(DbPath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return databaseService;
            }
        }
    }
}