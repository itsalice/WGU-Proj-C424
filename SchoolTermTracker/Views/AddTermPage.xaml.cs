using SchoolTermTracker.Models;
using SchoolTermTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolTermTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTermPage : ContentPage
	{
		public AddTermPage()
		{
			InitializeComponent();
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

        private async void AddTermSubmitClicked(object sender, EventArgs e)
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

			await Term.AddTerm(name, start, end);
			await Navigation.PopAsync();
		}
    }
}