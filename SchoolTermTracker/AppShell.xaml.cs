using SchoolTermTracker.Views;

namespace SchoolTermTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddTermPage), typeof(AddTermPage));
            Routing.RegisterRoute(nameof(ViewTermPage), typeof(ViewTermPage));
            Routing.RegisterRoute(nameof(EditTermPage), typeof(EditTermPage));
            Routing.RegisterRoute(nameof(AddCoursePage), typeof(AddCoursePage));
            Routing.RegisterRoute(nameof(ViewCoursePage), typeof(ViewCoursePage));
            Routing.RegisterRoute(nameof(EditCoursePage), typeof(EditCoursePage));
            Routing.RegisterRoute(nameof(AddAssessmentPage), typeof(AddAssessmentPage));
            Routing.RegisterRoute(nameof(EditAssessmentPage), typeof(EditAssessmentPage));
            Routing.RegisterRoute(nameof(ViewReportPage), typeof(ViewReportPage));
            Routing.RegisterRoute(nameof(UserLoginPage), typeof(UserLoginPage));
        }
    }
}
