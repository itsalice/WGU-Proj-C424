using SchoolTermTracker.Models;
using SchoolTermTracker.Services;

namespace SchoolTermTracker.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class EditAssessmentPage : ContentPage
{
	private int _assessmentId;

	public EditAssessmentPage(int assessmentId)
	{
		InitializeComponent();

		_assessmentId = assessmentId; 
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var assessment = await Course.GetAssessment(_assessmentId);

        BindingContext = new
        {
            AssessmentType = assessment.Type,
            StartDate = assessment.Start,
            EndDate = assessment.End,
            StartCheckBox = assessment.IsStart,
            EndCheckBox = assessment.IsEnd
        };
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

    private async void UpdateAssessmentBtnClicked(object sender, EventArgs e)
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

        var assessment = await Course.GetAssessment(_assessmentId);

        string type = assessment.Type;
        DateTime start = startDate.Date;
        DateTime end = endDate.Date;
        bool isStart = startCheckBox.IsChecked;
        bool isEnd = endCheckBox.IsChecked;

        await Course.UpdateAssessment(_assessmentId, type, start, end, isStart, isEnd);

        await Navigation.PopAsync();
    }
}