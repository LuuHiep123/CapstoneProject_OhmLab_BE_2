using BusinessLayer.RequestModel.Assignment;
using BusinessLayer.RequestModel.EquipmentType;
using BusinessLayer.RequestModel.Grade;
using BusinessLayer.ResponseModel.Assignment;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.EquipmentType;
using BusinessLayer.ResponseModel.Grade;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IGradeService
    {
        // Chấm điểm team
        Task<BaseResponse<ResponseModel.Grade.GradeResponseModel>> CreateGrade(CreateGradeRequestModel model, Guid TeacherId);
        Task<DynamicResponse<GradeResponseModel>> GetAllGrade(GetAllGradeRequestModel model);
        Task<BaseResponse<List<GradeResponseModel>>> GetGradeOfTeamByRegistrationScheduleIdAndTeamId(GetGradeOfTeamByRegistrationScheduleIdAndTeamId model);
        Task<BaseResponse<List<GradeResponseModel>>> GetGradeOfTeamByTeamId(int teamId);
        Task<BaseResponse<List<GradeResponseModel>>> GetGradeOfTeamByRegistrationScheduleId(int registrationScheduleId);
        Task<BaseResponse<GradeResponseModel>> GetGradeOfTeamById(int gradeId);
        Task<BaseResponse<GradeResponseModel>> UpdateGrade(UpdateGradeRequestModel model);
    }
}
