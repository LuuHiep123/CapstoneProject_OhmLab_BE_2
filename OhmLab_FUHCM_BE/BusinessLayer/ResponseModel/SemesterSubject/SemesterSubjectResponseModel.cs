using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.SemesterSubject
{
    public class SemesterSubjectResponseModel
    {
        public int SemesterSubjectId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
        public string SemesterSubjectStatus { get; set; } = null!;
    }
}
