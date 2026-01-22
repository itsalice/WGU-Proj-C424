using SchoolTermTracker.Models;
using SchoolTermTracker.Views;

namespace SchoolTermTracker
{
    public partial class MainPage : ContentPage
    {
        private List<User> Users;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Users = await User.GetUsersAsync();
            var isLoggedIn = await User.IsUserLoggedIn();

            if (isLoggedIn)
            {
                await Shell.Current.GoToAsync(nameof(TermListPage));
            }
            else
            {
                if (Users.Count == 0)
                {
                    await User.AddUserLogin("admin", "password", false);
                }
                else
                {
                    await Shell.Current.GoToAsync(nameof(UserLoginPage));
                }
            }
        }
    }
}
