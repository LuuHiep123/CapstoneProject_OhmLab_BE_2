using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.SemesterSubject
{
    public class CreateSemesterSubjectRequestModel
    {
        public int SubjectId { get; set; }
        public int SemesterId { get; set; }
    }
}
