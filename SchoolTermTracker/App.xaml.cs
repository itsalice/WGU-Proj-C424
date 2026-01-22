using SchoolTermTracker.Models;
using SchoolTermTracker.Views;

namespace SchoolTermTracker
{
    public partial class App : Application
    {
        private List<User> Users;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            OnResume();
        }
    }
}
