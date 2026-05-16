using BusinessLayer.RequestModel.Grade;
using BusinessLayer.RequestModel.GradeDesciption;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.Grade;
using BusinessLayer.ResponseModel.GradeDescription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IGradeDescriptionService
    {
        Task<DynamicResponse<GradeDesciprionResponseModel>> GetAllGradeDescription(GetAllGradeDescriptionRequestModel model);
        Task<BaseResponse<GradeDesciprionResponseModel>> GetGradeDescriptionById(int gradeDescriptionId);
        Task<BaseResponse<List<GradeDesciprionResponseModel>>> GetGradeDescriptionByGradeId(int gradeId);
        Task<BaseResponse<List<GradeDesciprionResponseModel>>> GetGradeDescriptionByUserid(Guid userId);
        Task<BaseResponse<GradeDesciprionResponseModel>> UpdateGradeDescription(UpdateGradeDescriptionRequestModel model);
    }
}
