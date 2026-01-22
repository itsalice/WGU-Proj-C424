using Plugin.LocalNotification;
using SchoolTermTracker.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTermTracker.Models
{
    [Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Indexed]
        [Column("term_id")]
        public int TermId { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("start")]
        public DateTime Start { get; set; }
        [Column("end")]
        public DateTime End { get; set; }
        [Column("start_notification")]
        public bool IsStart { get; set; }
        [Column("end_notification")]
        public bool IsEnd { get; set; }
        [Column("status")]
        public string Status { get; set; }
        [Column("note")]
        public string? Note { get; set; }

        [Indexed]
        [Column("instructor_id")]
        public int InstructorId { get; set; }

        public string CombinedDates => $"{Start:MM/dd/yyyy} - {End:MM/dd/yyyy}";

        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
            {
                return;
            }

            db = new DataService().GetConnection();

            await db.CreateTableAsync<Course>();
            await db.CreateTableAsync<Instructor>();
            await db.CreateTableAsync<Assessment>();
        }

        public static async Task<int> AddCourse(int termId, string courseName, DateTime start, DateTime end, bool startNotification, bool endNotification, string status, string note)
        {
            await Init();

            var course = new Course
            {
                TermId = termId,
                Name = courseName,
                Start = start,
                End = end,
                IsStart = startNotification,
                IsEnd = endNotification,
                Status = status,
                Note = string.IsNullOrWhiteSpace(note) ? null : note
            };

            if (startNotification || endNotification)
            {
                await LocalNotificationCenter.Current.AreNotificationsEnabled();
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }

            await db.InsertAsync(course);

            return course.Id;
        }

        public static async Task AddInstructor(int courseId, string name, string phone, string email)
        {
            await Init();

            var instructor = new Instructor
            {
                CourseId = courseId,
                Name = name,
                Phone = phone,
                Email = email
            };

            await db.InsertAsync(instructor);
        }

        public static async Task AddAssessment(int courseId, string type, DateTime start, DateTime end, bool startNotification, bool endNotification)
        {
            await Init();

            var assessment = new Assessment
            {
                CourseId = courseId,
                Type = type,
                Start = start,
                End = end,
                IsStart = startNotification,
                IsEnd = endNotification,
            };

            if (startNotification || endNotification)
            {
                await LocalNotificationCenter.Current.AreNotificationsEnabled();
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }

            await db.InsertAsync(assessment);
        }

        public static async Task UpdateCourse(int id, string name, DateTime start, DateTime end, bool startNotification, bool endNotification, string status, string note)
        {
            await Init();

            var course = await db.Table<Course>().FirstOrDefaultAsync(t => t.Id == id);
            if (id != null)
            {
                course.Name = name;
                course.Start = start;
                course.End = end;
                course.IsStart = startNotification;
                course.IsEnd = endNotification;
                course.Status = status;
                course.Note = string.IsNullOrWhiteSpace(note) ? null : note;
            }

            if (startNotification || endNotification)
            {
                await LocalNotificationCenter.Current.AreNotificationsEnabled();
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }

            await db.UpdateAsync(course);
        }

        public static async Task UpdateInstructor(int id, int courseId, string name, string phone, string email)
        {
            await Init();

            var instructor = await db.Table<Instructor>().FirstOrDefaultAsync(t => t.Id == id && t.CourseId == courseId);
            if (id != null)
            {
                instructor.CourseId = courseId;
                instructor.Name = name;
                instructor.Phone = phone;
                instructor.Email = email;
            }

            await db.UpdateAsync(instructor);
        }

        public static async Task UpdateAssessment(int id, string type, DateTime start, DateTime end, bool startNotification, bool endNotification)
        {
            await Init();

            var assessment = await db.Table<Assessment>().FirstOrDefaultAsync(i => i.Id == id);
            if (id != null)
            {
                assessment.Type = type;
                assessment.Start = start;
                assessment.End = end;
                assessment.IsStart = startNotification;
                assessment.IsEnd = endNotification;
            }

            await db.UpdateAsync(assessment);
        }

        public static async Task RemoveCourse(int id)
        {
            await Init();

            await db.DeleteAsync<Course>(id);
        }

        public static async Task RemoveAssessment(int id)
        {
            await Init();

            await db.DeleteAsync<Assessment>(id);
        }

        public static async Task<Course> GetCourse(int id)
        {
            await Init();

            return await db.Table<Course>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public static async Task<Instructor> GetInstructor(int courseId)
        {
            await Init();

            return await db.Table<Instructor>().Where(i => i.CourseId == courseId).FirstOrDefaultAsync();
        }

        public static async Task<Assessment> GetAssessment(int id)
        {
            await Init();

            return await db.Table<Assessment>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public static async Task<List<Course>> GetCoursesAsync(int termId)
        {
            await Init();

            return await db.Table<Course>().Where(i => i.TermId == termId).OrderBy(x => x.Start).ToListAsync();
        }

        public static async Task<List<Assessment>> GetAssessmentsAsync(int courseId)
        {
            await Init();

            return await db.Table<Assessment>().Where(i => i.CourseId == courseId).OrderBy(x => x.Start).ToListAsync();

        }

        public static async Task<List<Course>> GetSearchAsync(int termId, string text)
        {
            await Init();

            return await db.Table<Course>().Where(i => i.TermId == termId && i.Name.ToLower().Contains(text.ToLower())).OrderBy(x => x.Start).ToListAsync();
        }
    }
}
