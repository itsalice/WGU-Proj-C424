using SchoolTermTracker.Models;
using SchoolTermTracker.Services;

namespace SchoolTermTracker.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddAssessmentPage : ContentPage
{
    private int _courseId;
    private List<Assessment> Assessments;

    public AddAssessmentPage(int courseId)
	{
		InitializeComponent();

        _courseId = courseId; 
	}

    private async void AssessmentTypeSelectedIndexChanged(object sender, EventArgs e)
    {
        if (assessmentType.SelectedIndex == -1)
        {
            assessmentTypeErrorLabel.Text = "Must select an assessment type";
            assessmentTypeErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            assessmentTypeErrorLabel.IsVisible = false;
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

    private async void AddAssessmentBtnClicked(object sender, EventArgs e)
    {
        Assessments = await Course.GetAssessmentsAsync(_courseId);

        if (assessmentType.SelectedIndex == -1)
        {
            assessmentTypeErrorLabel.Text = "Please select an assessment type";
            assessmentTypeErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            assessmentTypeErrorLabel.IsVisible = false;
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

        foreach (var assessment in Assessments)
        {
            if (assessment.Type == assessmentType.SelectedItem.ToString())
            {
                assessmentTypeErrorLabel.Text = "Assessment already exists. Please select another assessment type";
                assessmentTypeErrorLabel.IsVisible = true;
                return;
            }
            else
            {
                assessmentTypeErrorLabel.IsVisible = false;
            }
        }

        string type = assessmentType.SelectedItem?.ToString();
        DateTime start = startDate.Date;
        DateTime end = endDate.Date;
        bool isStart = startCheckBox.IsChecked;
        bool isEnd = endCheckBox.IsChecked;

        await Course.AddAssessment(_courseId, type, start, end, isStart, isEnd);

        await Navigation.PopAsync();
    }
}