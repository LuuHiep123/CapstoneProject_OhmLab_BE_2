using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.RegistrationSchedule
{
    public class CreateRegistrationScheduleRequestModel
    {
        public string RegistraionScheduleName { get; set; } = null!;
        public Guid TeaacherId { get; set; }
        public int ClassId { get; set; }
        public int RoomId { get; set; }
        public int LabId { get; set; }
        public int SlotId { get; set; }
        public DateTime RegistraionScheduleDate { get; set; }
        public string? RegistraionScheduleDescription { get; set; }
    }
}
