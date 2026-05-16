using System;

namespace BusinessLayer.RequestModel.Report
{
    public class CreateReportRequestModel
    {
        public int RegistraionScheduleId { get; set; }
        public string EquipmentId { get; set; }
        public string Url_Img { get; set; }
        public string ReportTitle { get; set; }
        public string? ReportDescription { get; set; }

    }

    public class GetAllReportRequestModel
    {
        public int pageNum { get; set; } = 1;
        public int pageSize { get; set; } = 1;
        public string? status { get; set; }

    }

    public class UpdateReportStatusRequestModel
    {
        public Guid UserId { get; set; }
        public int RegistraionScheduleId { get; set; }
        public string EquipmentId { get; set; } = null!;
        public string ReportTitle { get; set; } = null!;
        public string? ReportDescription { get; set; }
        public string? ReportNote { get; set; }
        public string ReportStatus { get; set; } = null!;
    }

    public class AcceptReportRequestModel
    {
        public int ReportId { get; set; }
        public string? ReportNote { get; set; }
    }
} 