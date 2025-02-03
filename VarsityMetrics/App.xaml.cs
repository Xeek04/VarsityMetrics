using SQLite;
using VarsityMetrics.DB_Models;

namespace VarsityMetrics
{
    public partial class App : Application
    {
        private readonly DBAccess _db;
        public App(DBAccess db)
        {
            InitializeComponent();

            MainPage = new LoginPage();
            _db = db;
        }

        protected override async void OnStart()
        {
            await _db.Init(); // TODO change this to Init() so that it works

            base.OnStart();
        }
    }
}
