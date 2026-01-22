using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTermTracker.Models
{
    [Table("Assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Indexed]
        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("type")]
        public string Type { get; set; }
        [Column("start")]
        public DateTime Start { get; set; }
        [Column("end")]
        public DateTime End { get; set; }
        [Column("start_notification")]
        public bool IsStart { get; set; }
        [Column("end_notification")]
        public bool IsEnd { get; set; }

        public string CombinedDates => $"{Start:MM/dd/yyyy} - {End:MM/dd/yyyy}";
    }
}
