using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.GradeDescription
{
    public class GradeDesciprionResponseModel
    {
        public int GradeDescriptionId { get; set; }
        public int GradeId { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName{ get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int RegistrationScheduleId { get; set; }
        public string RegistrationScheduleName { get; set; }
        public int SlotId { get; set; }
        public string SlotName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int LabId { get; set; }
        public string LabName { get; set; }
        public float GradeDescriptionScore { get; set; }
        public string? GradeDescriptionDescription { get; set; }
        public string GradeDescriptionStatus { get; set; } = null!;
    }
}
