using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.Grade
{
    public class GradeResponseModel
    {
        public int GradeId { get; set; }
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int RegistraionScheduleId { get; set; }
        public string RegistraionScheduleName { get; set; }
        public DateTime RegistraionScheduleDate { get; set; }
        public string SlotName { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public float GradeScore { get; set; }
        public string GradeDescription { get; set; }
        public DateTime GradeDate { get; set; }
        public string GradeStatus { get; set; }
    }
}
