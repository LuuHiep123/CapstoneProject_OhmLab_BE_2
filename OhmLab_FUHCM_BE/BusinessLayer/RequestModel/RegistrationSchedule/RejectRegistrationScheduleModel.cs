using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.RegistrationSchedule
{
    public class RejectRegistrationScheduleModel
    {
        public int RegistrationScheduleId { get; set; }
        public string? RegistrationScheduleNote { get; set; }
    }
}
