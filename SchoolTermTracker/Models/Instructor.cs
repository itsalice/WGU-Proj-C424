using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTermTracker.Models
{
    [Table("Instructors")]
    public class Instructor
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Indexed]
        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("email")]
        public string Email { get; set; }
    }
}
