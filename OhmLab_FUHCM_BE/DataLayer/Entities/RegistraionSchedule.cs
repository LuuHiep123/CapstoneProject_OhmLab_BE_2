using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class RegistraionSchedule
    {
        public RegistraionSchedule()
        {
            Grades = new HashSet<Grade>();
            Reports = new HashSet<Report>();
        }

        public int RegistraionScheduleId { get; set; }
        public string RegistraionScheduleName { get; set; } = null!;
        public Guid TeaacherId { get; set; }
        public int ClassId { get; set; }
        public int LabId { get; set; }
        public int RoomId { get; set; }
        public int SlotId { get; set; }
        public DateTime RegistraionScheduleDate { get; set; }
        public string? RegistraionScheduleDescription { get; set; }
        public string? RegistraionScheduleNote { get; set; }
        public DateTime RegistraionScheduleCreateDate { get; set; }
        public DateTime? RegistraionScheduleCheckIn { get; set; }
        public DateTime? RegistraionScheduleCheckOut { get; set; }
        public string? RegistraionSchedule_Url_Img_Checkout { get; set; }
        public string RegistraionScheduleStatus { get; set; } = null!;

        public virtual Class Class { get; set; } = null!;
        public virtual Lab Lab { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual Slot Slot { get; set; } = null!;
        public virtual User Teaacher { get; set; } = null!;
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
