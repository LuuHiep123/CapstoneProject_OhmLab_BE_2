using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public Guid UserId { get; set; }
        public int RegistraionScheduleId { get; set; }
        public string EquipmentId { get; set; } = null!;
        public string ReportTitle { get; set; } = null!;
        public string? ReportDescription { get; set; }
        public string? ReportNote { get; set; }
        public string Url_Img { get; set; }
        public DateTime ReportCreateDate { get; set; }
        public string ReportStatus { get; set; } = null!;

        public virtual Equipment Equipment { get; set; } = null!;
        public virtual RegistraionSchedule RegistraionSchedule { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
