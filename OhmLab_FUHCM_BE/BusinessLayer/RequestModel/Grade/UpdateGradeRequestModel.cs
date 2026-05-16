using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Grade
{
    public class UpdateGradeRequestModel
    {
        public int GradeId { get; set; }
        public Guid? TeacherId { get; set; }
        public int? RegistraionScheduleId { get; set; }
        public int? TeamId { get; set; }
        public double? GradeScore { get; set; }
        public string? GradeDescription { get; set; }
        public DateTime? GradeDate { get; set; }
        public string? GradeStatus { get; set; }
    }
}
