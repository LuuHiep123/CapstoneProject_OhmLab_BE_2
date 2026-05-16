using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Grade
{
    public class GetGradeOfTeamByRegistrationScheduleIdAndTeamId
    {
        public int RegistrationScheduleId { get; set; }
        public int TeamId { get; set; } 
    }
}
