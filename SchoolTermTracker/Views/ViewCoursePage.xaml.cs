using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using SchoolTermTracker.Models;
using SchoolTermTracker.Services;
using SchoolTermTracker.ViewModels;
using System.Diagnostics;

namespace SchoolTermTracker.Views;

public partial class ViewCoursePage : ContentPage
{
    private int _courseId;
    private List<Assessment> Assessments;

    public ViewCoursePage(int courseId)
	{
		InitializeComponent();

		_courseId = courseId; 
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var course = await Course.GetCourse(_courseId);
        var instructor = await Course.GetInstructor(_courseId);

        BindingContext = new
        {
            CourseName = course.Name,
            CourseDates = course.CombinedDates,
            CourseStatus = course.Status,
            CourseNote = course.Note,
            InstructorName = instructor.Name,
            InstructorPhone = instructor.Phone,
            InstructorEmail = instructor.Email
        };

        if (string.IsNullOrEmpty(CourseNote.Text))
        {
            ShareBtn.IsVisible = false;
        }
        else
        {
            ShareBtn.IsVisible = true;
        }

        Assessments = await Course.GetAssessmentsAsync(_courseId);

        collectionView.ItemsSource = Assessments;

        if (Assessments.Count == 2)
        {
            addAssessmentBtn.IsVisible = false;
        }
        else
        {
            addAssessmentBtn.IsVisible = true;
        }
    }

    private async void ShareCourseNote(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(CourseNote.Text))
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = CourseNote.Text,
                Title = "Share Note"
            });
        }
    }

    private async void EditCourseClicked(object sender, EventArgs e)
    {
        if (_courseId != 0)
        {
            await Navigation.PushAsync(new EditCoursePage(_courseId));
        }
    }

    private async void DeleteCourseClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Delete Course", $"Are you sure you want to delete {courseName.Text}?", "Yes", "No");

        if (answer)
        {
            await Course.RemoveCourse(_courseId);
            await Navigation.PopAsync();
        }
    }

    private async void AddAssessmentClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddAssessmentPage(_courseId));
    }

    private async void EditAssessmentClicked(object sender, EventArgs e)
    {
        var imageBtn = sender as ImageButton;
        var assessment = imageBtn?.BindingContext as Assessment;

        if (assessment != null)
        {
            await Navigation.PushAsync(new EditAssessmentPage(assessment.Id));
        }
    }

    private async Task RefreshAssessmentDataAsync()
    {
        Assessments = await Course.GetAssessmentsAsync(_courseId);

        collectionView.ItemsSource = Assessments;
    }

    private async void DeleteAssessmentClicked(object sender, EventArgs e)
    {
        var imageBtn = sender as ImageButton;
        var assessment = imageBtn?.BindingContext as Assessment;

        if (assessment != null)
        {
            bool answer = await DisplayAlert("Delete Assessment", $"Are you sure you want to delete {assessment.Type}?", "Yes", "No");

            if (answer)
            {
                await Course.RemoveAssessment(assessment.Id);
                await RefreshAssessmentDataAsync();
                addAssessmentBtn.IsVisible = true;
            }
        }
    }
}