using SchoolTermTracker.Models;

namespace SchoolTermTracker.ViewModels
{
    public class CourseViewModel
    {
        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
        public Assessment Assessment { get; set; }

    }
}
