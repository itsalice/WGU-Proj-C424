using SchoolTermTracker.Models;
using SchoolTermTracker.Services;
using System.Text.RegularExpressions;

namespace SchoolTermTracker.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class EditCoursePage : ContentPage
{
	private int _courseId;

	public EditCoursePage(int courseId)
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
            StartDate = course.Start,
            EndDate = course.End,
            StartCheckBox = course.IsStart,
            EndCheckBox = course.IsEnd,
            CourseStatus = course.Status,
            InstructorId = instructor.Id,
            InstructorName = instructor.Name,
            InstructorPhone = instructor.Phone,
            InstructorEmail = instructor.Email,
            CourseNote = course.Note
        };
    }

    private async void CourseNameTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(courseName.Text))
        {
            courseNameErrorLabel.Text = "Please enter a course name";
            courseNameErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            courseNameErrorLabel.IsVisible = false;
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

    private async void CourseStatusSelectedIndexChanged(object sender, EventArgs e)
    {
        if (courseStatus.SelectedIndex == -1)
        {
            statusErrorLabel.Text = "Must select a course status";
            statusErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            statusErrorLabel.IsVisible = false;
        }
    }

    private async void InstructorNameTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(instructorName.Text))
        {
            instructorNameErrorLabel.Text = "Please enter instructor name";
            instructorNameErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            instructorNameErrorLabel.IsVisible = false;
        }
    }

    private async void InstructorPhoneTextChanged(object sender, TextChangedEventArgs e)
    {
        var phoneRegex = new Regex(@"\(?\d{3}\)?[. -]? *\d{3}[. -]? *[. -]?\d{4}");

        if (string.IsNullOrEmpty(instructorPhone.Text))
        {
            instructorPhoneErrorLabel.Text = "Please enter instructor phone";
            instructorPhoneErrorLabel.IsVisible = true;
            return;
        }
        else if (!phoneRegex.IsMatch(instructorPhone.Text))
        {
            instructorPhoneErrorLabel.Text = "Please enter a valid phone number";
            instructorPhoneErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            instructorPhoneErrorLabel.IsVisible = false;
        }
    }

    private async void InstructorEmailTextChanged(object sender, TextChangedEventArgs e)
    {
        var emailRegex = new Regex(@"^[^@]+@[^@]+\.[^@]+$");

        if (string.IsNullOrEmpty(instructorEmail.Text))
        {
            instructorEmailErrorLabel.Text = "Please enter instructor email";
            instructorEmailErrorLabel.IsVisible = true;
            return;
        }
        else if (!emailRegex.IsMatch(instructorEmail.Text))
        {
            instructorEmailErrorLabel.Text = "Please enter a valid email address";
            instructorEmailErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            instructorEmailErrorLabel.IsVisible = false;
        }
    }

    private async void UpdateCourseClicked(object sender, EventArgs e)
    {
        var phoneRegex = new Regex(@"\(?\d{3}\)?[. -]? *\d{3}[. -]? *[. -]?\d{4}");
        var emailRegex = new Regex(@"^[^@]+@[^@]+\.[^@]+$");

        // Course
        string cName = courseName.Text;
        DateTime start = startDate.Date;
        DateTime end = endDate.Date;
        bool isStart = startCheckBox.IsChecked;
        bool isEnd = endCheckBox.IsChecked;
        string status = courseStatus.SelectedItem?.ToString();
        string? note = courseNote.Text;

        // Instructor
        Int32.TryParse(instructorId.Text, out int inId);
        string iName = instructorName.Text;
        string iPhone = instructorPhone.Text;
        string iEmail = instructorEmail.Text;



        if (string.IsNullOrEmpty(cName))
        {
            courseNameErrorLabel.Text = "Please enter a course name";
            courseNameErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            courseNameErrorLabel.IsVisible = false;
        }

        if (start.Date > end.Date)
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

        if (courseStatus.SelectedIndex == -1)
        {
            statusErrorLabel.Text = "Must select a course status";
            statusErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            statusErrorLabel.IsVisible = false;
        }

        if (string.IsNullOrEmpty(iName))
        {
            instructorNameErrorLabel.Text = "Please enter instructor name";
            instructorNameErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            instructorNameErrorLabel.IsVisible = false;
        }

        if (string.IsNullOrEmpty(iPhone))
        {
            instructorPhoneErrorLabel.Text = "Please enter instructor phone";
            instructorPhoneErrorLabel.IsVisible = true;
            return;
        }
        else if (!phoneRegex.IsMatch(iPhone))
        {
            instructorPhoneErrorLabel.Text = "Please enter a valid phone number";
            instructorPhoneErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            instructorPhoneErrorLabel.IsVisible = false;
        }

        if (string.IsNullOrEmpty(iEmail))
        {
            instructorEmailErrorLabel.Text = "Please enter instructor email";
            instructorEmailErrorLabel.IsVisible = true;
            return;
        }
        else if (!emailRegex.IsMatch(iEmail))
        {
            instructorEmailErrorLabel.Text = "Please enter a valid email address";
            instructorEmailErrorLabel.IsVisible = true;
            return;
        }
        else
        {
            instructorEmailErrorLabel.IsVisible = false;
        }

        await Course.UpdateCourse(_courseId, cName, start, end, isStart, isEnd, status, note);
        await Course.UpdateInstructor(inId, _courseId, iName, iPhone, iEmail);

        await Navigation.PopAsync(); 
    }
}