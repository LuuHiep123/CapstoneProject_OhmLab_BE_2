using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class GradeDescription
    {
        public int GradeDescriptionId { get; set; }
        public int GradeId { get; set; }
        public Guid StudentId { get; set; }
        public int ClassId { get; set; }
        public int LabId { get; set; }
        public double GradeDescriptionScore { get; set; }
        public string? GradeDescriptionDescription { get; set; }
        public string GradeDescriptionStatus { get; set; } = null!;

        public virtual Class Class { get; set; } = null!;
        public virtual Grade Grade { get; set; } = null!;
        public virtual Lab Lab { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
    }
}
