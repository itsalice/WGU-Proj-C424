using SchoolTermTracker.Models;
using SchoolTermTracker.Services;

namespace SchoolTermTracker.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class EditTermPage : ContentPage
{
    private int _termId;

    public EditTermPage(int termId)
	{
		InitializeComponent();

        _termId = termId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var term = await Term.GetTerm(_termId);
        BindingContext = term;

        termName.Text = term.Name;
        startDate.Date = term.Start;
        endDate.Date = term.End;
    }

    private async void TermNameTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(termName.Text))
        {
            termNameErrorLabel.Text = "Please enter a term name";
            termNameErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            termNameErrorLabel.IsVisible = false;
        }
    }

    private async void StartDateSelected(object sender, DateChangedEventArgs e)
    {
        if (startDate.Date > endDate.Date)
        {
            dateErrorLabel.Text = "The start date must be before the end date";
            dateErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            dateErrorLabel.IsVisible = false;
        }

        if (endDate == null || startDate == null)
        {
            dateErrorLabel.Text = "Must enter a start date and an end date";
            dateErrorLabel.IsVisible = true;
            return;
        }
        else if (endDate != null && startDate == null)

        {
            dateErrorLabel.Text = "The start date must be entered before the end date";
            dateErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            dateErrorLabel.IsVisible = false;
        }
    }

    private async void EndDateSelected(object sender, DateChangedEventArgs e)
    {
        if (endDate == null || startDate == null)
        {
            dateErrorLabel.Text = "Must enter a start date and an end date";
            dateErrorLabel.IsVisible = true;
            return;
        }
        else if (endDate != null && startDate == null)

        {
            dateErrorLabel.Text = "The start date must be entered before the end date";
            dateErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            dateErrorLabel.IsVisible = false;
        }
    }

    private async void EditTermSubmitClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(termName.Text))
        {
            termNameErrorLabel.Text = "Please enter a term name";
            termNameErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            termNameErrorLabel.IsVisible = false;
        }

        if (startDate.Date > endDate.Date)
        {
            dateErrorLabel.Text = "The start date must be before the end date";
            dateErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            dateErrorLabel.IsVisible = false;
        }

        if (endDate == null || startDate == null)
        {
            dateErrorLabel.Text = "Must enter a start date and an end date";
            dateErrorLabel.IsVisible = true;
            return;
        }
        else if (endDate != null && startDate == null)

        {
            dateErrorLabel.Text = "The start date must be entered before the end date";
            dateErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            dateErrorLabel.IsVisible = false;
        }

        string name = termName.Text;
        DateTime start = startDate.Date;
        DateTime end = endDate.Date;

        await Term.UpdateTerm(_termId, name, start, end);
        await Navigation.PopAsync();
    }

    private async void DeleteTermClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Delete Term", $"Are you sure you want to delete {termName.Text}?", "Yes", "No");

        if (answer)
        {
            await Term.RemoveTerm(_termId);
            await Navigation.PushAsync(new TermListPage());
        }
    }
}