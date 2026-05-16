
using BusinessLayer.RequestModel.Semester;
using BusinessLayer.RequestModel.SemesterSubject;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.Semester;
using BusinessLayer.ResponseModel.SemesterSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface ISemesterSubjectService
    {
        Task<BaseResponse<SemesterSubjectResponseModel>> CreateSemesterSubjectAsync(CreateSemesterSubjectRequestModel model);
        Task<BaseResponse<List<SemesterSubjectResponseModel>>> GetBySemesterIdAsync(int semesterId);
        Task<BaseResponse<List<SemesterSubjectResponseModel>>> GetBySubjectIdAsync(int subjectId);
        Task<BaseResponse<SemesterSubjectResponseModel>> GetByIdAsync(int id);
        Task<BaseResponse<SemesterSubjectResponseModel>> GetBySemesterIdAndSubjectIdAsync(int semesterId,int subjectId);
        Task<DynamicResponse<SemesterSubjectResponseModel>> GetAllAsync(GetAllSemesterSubjectRequestModel model);
        Task<BaseResponse<SemesterSubjectResponseModel>> UpdateAsync(int id, UpdateSemesterSubjectRequestModel model);
    }
}
