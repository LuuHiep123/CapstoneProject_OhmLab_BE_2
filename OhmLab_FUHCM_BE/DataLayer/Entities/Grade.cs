using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public Guid TeacherId { get; set; }
        public int RegistraionScheduleId { get; set; }
        public int TeamId { get; set; }
        public double GradeScore { get; set; }
        public string? GradeDescription { get; set; }
        public DateTime GradeDate { get; set; }
        public string GradeStatus { get; set; } = null!;

        public virtual RegistraionSchedule RegistraionSchedule { get; set; } = null!;
        public virtual ICollection<GradeDescription> GradeDescriptions { get; set; }
        public virtual User Teacher { get; set; } = null!;
        public virtual Team Team { get; set; } = null!;
    }
}
