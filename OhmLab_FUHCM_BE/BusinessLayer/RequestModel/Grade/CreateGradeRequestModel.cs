using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Grade
{
    public class CreateGradeRequestModel
    {
        public int RegistraionScheduleId { get; set; }
        public int TeamId { get; set; }
        public double GradeScore { get; set; }
        public string? GradeDescription { get; set; }
    }
}
