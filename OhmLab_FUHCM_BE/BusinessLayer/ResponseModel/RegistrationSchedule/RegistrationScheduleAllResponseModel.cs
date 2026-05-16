using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.RegistrationSchedule
{
    public class RegistrationScheduleAllResponseModel
    {
        public int RegistraionScheduleId { get; set; }
        public string RegistraionScheduleName { get; set; } = null!;
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherRollNumber { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int LabId { get; set; }
        public string LabName { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int SlotId { get; set; }
        public string SlotName { get; set; }
        public string SlotStartTime { get; set; }
        public string SlotEndTime { get; set; }
        public DateTime RegistraionScheduleDate { get; set; }
        public DateTime RegistraionScheduleCreateDate { get; set; }
        public DateTime RegistraionScheduleCheckIn { get; set; }
        public DateTime RegistraionScheduleCheckOut { get; set; }
        public string? RegistraionScheduleDescription { get; set; }
        public string? RegistraionSchedule_Url_Img_Checkout { get; set; }
        public string? RegistraionScheduleNote { get; set; }
        public int? Registration_Total_Practice { get; set; }
        public int? Registration_Number_Practice { get; set; }
        public string RegistraionScheduleStatus { get; set; } = null!;
        public string? Note { get; set; }
    }
}
