using Microsoft.Maui.Controls;
using SchoolTermTracker.Models;
using System.Linq;

namespace SchoolTermTracker.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ViewReportPage : ContentPage
{
    private int _termId;
    private List<Course> Courses;

    public ViewReportPage(int termId)
	{
		InitializeComponent();

        _termId = termId;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var term = await Term.GetTerm(_termId);

        BindingContext = term;

        Courses = await Course.GetCoursesAsync(_termId);

        collectionView.ItemsSource = Courses;
    }
}