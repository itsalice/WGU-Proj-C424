using SchoolTermTracker.Models;

namespace SchoolTermTracker.Views;

public partial class UserLoginPage : ContentPage
{
	public UserLoginPage()
	{
		InitializeComponent();
	}

    private async void userLoginBtnClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(userName.Text))
        {
            userName.Margin = new Thickness(0, 0, 0, 0);
            userNameErrorLabel.Margin = new Thickness(0, 0, 0, 30);
            userNameErrorLabel.Text = "Please enter a user name";
            userNameErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            userName.Margin = new Thickness(0, 0, 0, 30);
            userNameErrorLabel.IsVisible = false;
        }

        if (string.IsNullOrWhiteSpace(password.Text))
        {
            password.Margin = new Thickness(0, 0, 0, 0);
            passwordErrorLabel.Margin = new Thickness(0, 0, 0, 30);
            passwordErrorLabel.Text = "Please enter a password";
            passwordErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            password.Margin = new Thickness(0, 0, 0, 30);
            passwordErrorLabel.IsVisible = false;
        }

        var isUser = await User.CheckUser(userName.Text, password.Text, true);

        if (isUser)
        {
            await Navigation.PushAsync(new TermListPage());
            loginErrorLabel.IsVisible = false;
        }
        else
        {
            loginErrorLabel.Text = "User name and password do not match";
            loginErrorLabel.IsVisible = true;
            return;
        }
    }
}