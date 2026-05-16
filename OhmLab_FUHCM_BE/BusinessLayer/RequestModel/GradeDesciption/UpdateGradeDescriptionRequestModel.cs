using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.GradeDesciption
{
    public class UpdateGradeDescriptionRequestModel
    {
        public int GradeDescriptionId { get; set; }
        public int GradeId { get; set; }
        public Guid StudentId { get; set; }
        public int ClassId { get; set; }
        public int LabId { get; set; }
        public double GradeDescriptionScore { get; set; }
        public string? GradeDescriptionDescription { get; set; }
        public string GradeDescriptionStatus { get; set; }
    }
}
