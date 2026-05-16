using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Class
    {
        public Class()
        {
            ClassUsers = new HashSet<ClassUser>();
            GradeDescriptions = new HashSet<GradeDescription>();
            RegistraionSchedules = new HashSet<RegistraionSchedule>();
            Schedules = new HashSet<Schedule>();
            Teams = new HashSet<Team>();
        }

        public int ClassId { get; set; }
        public int SemesterSubjectId { get; set; }
        public Guid? LecturerId { get; set; }
        public int? ScheduleTypeId { get; set; }
        public string ClassName { get; set; } = null!;
        public string? ClassDescription { get; set; }
        public string ClassStatus { get; set; } = null!;

        public virtual User? Lecturer { get; set; }
        public virtual ScheduleType? ScheduleType { get; set; }
        public virtual SemesterSubject SemesterSubject { get; set; } = null!;
        public virtual ICollection<ClassUser> ClassUsers { get; set; }
        public virtual ICollection<GradeDescription> GradeDescriptions { get; set; }
        public virtual ICollection<RegistraionSchedule> RegistraionSchedules { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
