using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class User
    {
        public User()
        {
            ClassUsers = new HashSet<ClassUser>();
            Classes = new HashSet<Class>();
            GradeDescriptions = new HashSet<GradeDescription>();
            Grades = new HashSet<Grade>();
            RegistraionSchedules = new HashSet<RegistraionSchedule>();
            Reports = new HashSet<Report>();
            TeamUsers = new HashSet<TeamUser>();
        }

        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = null!;
        public string UserRollNumber { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserRoleName { get; set; } = null!;
        public string UserNumberCode { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual ICollection<ClassUser> ClassUsers { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<GradeDescription> GradeDescriptions { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<RegistraionSchedule> RegistraionSchedules { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<TeamUser> TeamUsers { get; set; }
    }
}
