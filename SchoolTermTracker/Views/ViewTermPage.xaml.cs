using Microsoft.Maui.Controls;
using SchoolTermTracker.Models;
using SchoolTermTracker.Services;
using SchoolTermTracker.ViewModels;
using System.Diagnostics;

namespace SchoolTermTracker.Views;

public partial class ViewTermPage : ContentPage
{
    private int _termId;
    private List<Course> Courses;
    private List<Course> Results;

	public ViewTermPage(int termId)
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

    private async void EditTermBtnClicked(object sender, EventArgs e)
    {
        if (_termId != 0)
        {
            await Navigation.PushAsync(new EditTermPage(_termId));
        }
    }

    private void AddCourseBtnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddCoursePage(_termId)); 
    }

    private async void ViewCourseBtnClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var course = button?.BindingContext as Course;

        if (course != null)
        {
            await Navigation.PushAsync(new ViewCoursePage(course.Id));
        }
    }

    private async void SearchBarOnTextChanged(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        Results = await Course.GetSearchAsync(_termId, searchBar.Text);

        if (string.IsNullOrWhiteSpace(searchBar.Text))
        {
            searchResults.ItemsSource = null;
        }
        else
        {
            searchResults.ItemsSource = Results;
        }
    }

    private void GenerateReportBtnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ViewReportPage(_termId));
    }
}