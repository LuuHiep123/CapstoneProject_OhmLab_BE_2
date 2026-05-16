using BusinessLayer.ResponseModel.Assignment;
using BusinessLayer.ResponseModel.BaseResponse;
using DataLayer.Entities;
using BusinessLayer.RequestModel.Assignment;
using BusinessLayer.RequestModel.Class;

namespace BusinessLayer.Service
{
    public interface IAssignmentService
    {
        // Schedule management
        //Task<BaseResponse<ScheduleResponseModel>> CreatePracticeScheduleAsync(Schedule schedule);
        Task<BaseResponse<ScheduleResponseModel>> UpdatePracticeScheduleAsync(int scheduleId, Schedule schedule);
        Task<BaseResponse<bool>> DeletePracticeScheduleAsync(int scheduleId);
        Task<DynamicResponse<ScheduleResponseModel>> GetPracticeSchedulesByClassAsync(int classId);
        Task<DynamicResponse<ScheduleResponseModel>> GetPracticeSchedulesByLecturerAsync(Guid lecturerId);
        Task<BaseResponse<bool>> AddScheduleForLecturerAsync(AddScheduleForLecturerRequestModel model);
        Task<BaseResponse<bool>> AddScheduleForClassAsync(AddScheduleForClassRequestModel model);

        // Report management
        Task<BaseResponse<ReportResponseModel>> SubmitPracticeReportAsync(Report report);
        Task<BaseResponse<ReportResponseModel>> GetReportByIdAsync(int reportId);
        Task<DynamicResponse<ReportResponseModel>> GetReportsByStudentAsync(Guid studentId);
        Task<DynamicResponse<ReportResponseModel>> GetReportsByRegistrationScheduleAsync(int registrationScheduleId);
        Task<DynamicResponse<ReportResponseModel>> GetReportsByLabAsync(int labId);


        // Statistics
        Task<BaseResponse<object>> GetLabStatisticsAsync(int labId);
        Task<BaseResponse<object>> GetStudentGradeSummaryAsync(Guid studentId);
        Task<BaseResponse<object>> GetClassPracticeSummaryAsync(int classId);

        // Utility
        Task<Schedule> GetScheduleByIdAsync(int scheduleId);
        Task<BaseResponse<object>> GetSchedulesByDateAsync(DateTime date);
        Task<BaseResponse<object>> GetSchedulesByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
} 