using SQLite;
using VarsityMetrics.DB_Models;

namespace VarsityMetrics
{
    public partial class App : Application
    {
        public static DBAccess db;
        public App(DBAccess database)
        {
            InitializeComponent();

            MainPage = new LoginPage();
            db = database;

        }
    }
}
