using Plugin.LocalNotification;
using SchoolTermTracker.Models;
using SchoolTermTracker.Services;
using SchoolTermTracker.Views;

namespace SchoolTermTracker.Views;

public partial class TermListPage : ContentPage
{
    private List<Term> Terms;
    private List<Course> Courses;
    private List<Assessment> Assessments;
    private List<Term> Results;

    public TermListPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Terms = await Term.GetTermsAsync();

        collectionView.ItemsSource = Terms;

        if (Terms != null)
        {
            foreach (Term term in Terms)
            {
                Courses = await Course.GetCoursesAsync(term.Id);
            }
        }

        if (Courses != null)
        {
            foreach (var course in Courses)
            {
                Assessments = await Course.GetAssessmentsAsync(course.Id);

                if (course.IsStart && course.Start.Date == DateTime.Today)
                {
                    var courseStartNotificationRequest = new NotificationRequest
                    {
                        NotificationId = course.Id,
                        Title = "Reminder",
                        Description = course.Name + " starts today!"
                    };

                    await LocalNotificationCenter.Current.Show(courseStartNotificationRequest);
                }

                if (course.IsEnd && course.End.Date == DateTime.Today)
                {
                    var courseEndNotificationRequest = new NotificationRequest
                    {
                        NotificationId = course.Id,
                        Title = "Reminder",
                        Description = course.Name + " ends today!"
                    };

                    await LocalNotificationCenter.Current.Show(courseEndNotificationRequest);
                }

                if (Assessments != null)
                {
                    foreach (var assessment in Assessments)
                    {
                        if (assessment.IsStart && assessment.Start.Date == DateTime.Today)
                        {
                            var assessmentStartNotificationRequest = new NotificationRequest
                            {
                                NotificationId = assessment.Id,
                                Title = "Reminder",
                                Description = assessment.Type + " starts today!"
                            };

                            await LocalNotificationCenter.Current.Show(assessmentStartNotificationRequest);
                        }

                        if (assessment.IsEnd && assessment.End.Date == DateTime.Today)
                        {
                            var assessmentEndNotificationRequest = new NotificationRequest
                            {
                                NotificationId = assessment.Id,
                                Title = "Reminder",
                                Description = assessment.Type + " ends today!"
                            };

                            await LocalNotificationCenter.Current.Show(assessmentEndNotificationRequest);
                        }
                    }
                }
            }
        }
    }

    private void AddTermBtnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddTermPage());
    }

    private async void ViewTermBtnClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var term = button?.BindingContext as Term;

        if (term != null)
        {
            await Navigation.PushAsync(new ViewTermPage(term.Id));
        }
    }

    private async void SearchBarOnTextChanged(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        Results = await Term.GetSearchAsync(searchBar.Text);

        if (string.IsNullOrWhiteSpace(searchBar.Text))
        {
            searchResults.ItemsSource = null;
        }
        else
        {
            searchResults.ItemsSource = Results;
        }
    }
}