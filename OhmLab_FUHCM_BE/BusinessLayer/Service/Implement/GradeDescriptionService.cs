using AutoMapper;
using BusinessLayer.RequestModel.GradeDesciption;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.Grade;
using BusinessLayer.ResponseModel.GradeDescription;
using DataLayer.Entities;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList.Extensions;

namespace BusinessLayer.Service.Implement
{
    public class GradeDescriptionService : IGradeDescriptionService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IGradeDescriptionRepository _gradeDescriptionRepository;
        private readonly IRegistrationScheduleRepository _registrationScheduleRepository;
        private readonly ITeamUserRepository _teamUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public GradeDescriptionService(
            IGradeRepository gradeRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IGradeDescriptionRepository gradeDescriptionRepository,
            ITeamUserRepository teamUserRepository,
            IRegistrationScheduleRepository registrationScheduleRepository,
            IClassRepository classRepository
            )
        {
            _classRepository = classRepository;
            _teamUserRepository = teamUserRepository;
            _userRepository = userRepository;
            _gradeRepository = gradeRepository;
            _mapper = mapper;
            _gradeDescriptionRepository = gradeDescriptionRepository;
            _registrationScheduleRepository = registrationScheduleRepository;
        }

        public async Task<DynamicResponse<GradeDesciprionResponseModel>> GetAllGradeDescription(GetAllGradeDescriptionRequestModel model)
        {
            try
            {
                var listGradeDes = await _gradeDescriptionRepository.GetAllGradeDescription();


                if (!string.IsNullOrEmpty(model.status))
                {
                    listGradeDes = listGradeDes.Where(g => g.GradeDescriptionStatus.ToLower().Equals(model.status)).ToList();
                }
                var result = _mapper.Map<List<GradeDesciprionResponseModel>>(listGradeDes);

                // Nếu không có lỗi, thực hiện phân trang
                var pagedUsers = result// Giả sử result là danh sách người dùng
                    .OrderBy(g => g.GradeId) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<GradeDesciprionResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<GradeDesciprionResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pagedUsers.PageNumber,
                            Size = pagedUsers.PageSize,
                            Sort = "Ascending",
                            Order = null,
                            TotalPage = pagedUsers.PageCount,
                            TotalItem = pagedUsers.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = null,
                            role = null,
                            status = model.status,
                        },
                        PageData = pagedUsers.ToList(),
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<GradeDesciprionResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = null,
                    Data = null,
                };
            }
        }

        public async Task<BaseResponse<List<GradeDesciprionResponseModel>>> GetGradeDescriptionByGradeId(int gradeId)
        {
             try
            {
                var gradeDes = await _gradeDescriptionRepository.GetAllGradeDescription();
                gradeDes = gradeDes.Where(g => g.GradeId == gradeId).ToList();
                return new BaseResponse<List<GradeDesciprionResponseModel>>()
                {
                    Code = 200,
                    Success = true,
                    Message = "list gradeDescription by GradeId!",
                    Data = _mapper.Map<List<GradeDesciprionResponseModel>>(gradeDes)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<GradeDesciprionResponseModel>>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<GradeDesciprionResponseModel>> GetGradeDescriptionById(int gradeDescriptionId)
        {
            try
            {
                var gradeDes = await _gradeDescriptionRepository.GetGradeDescriptionById(gradeDescriptionId);
                if (gradeDes != null)
                {
                    var result = _mapper.Map<GradeDesciprionResponseModel>(gradeDes);
                    return new BaseResponse<GradeDesciprionResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = null,
                        Data = result
                    };
                }
                else
                {
                    return new BaseResponse<GradeDesciprionResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found grade!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<GradeDesciprionResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<List<GradeDesciprionResponseModel>>> GetGradeDescriptionByUserid(Guid userId)
        {
            try
            {
                var gradeDes = await _gradeDescriptionRepository.GetAllGradeDescription();
                gradeDes = gradeDes.Where(g => g.StudentId == userId).ToList();
                return new BaseResponse<List<GradeDesciprionResponseModel>>()
                {
                    Code = 200,
                    Success = true,
                    Message = "list gradeDescription by StudentId!",
                    Data = _mapper.Map<List<GradeDesciprionResponseModel>>(gradeDes)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<GradeDesciprionResponseModel>>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<GradeDesciprionResponseModel>> UpdateGradeDescription(UpdateGradeDescriptionRequestModel model)
        {
            try
            {
                if (model.GradeDescriptionScore < 0)
                {
                    return new BaseResponse<GradeDesciprionResponseModel>()
                    {
                        Code = 401,
                        Success = false,
                        Message = "Do not enter GradeDesciprion negative numbers!",
                        Data = null
                    };
                }
                var gradeDes = await _gradeDescriptionRepository.GetGradeDescriptionById(model.GradeDescriptionId);
                if (gradeDes != null)
                {
                    var result = _mapper.Map(model, gradeDes);
                    await _gradeDescriptionRepository.UpdateGradeDescription(result);
                    return new BaseResponse<GradeDesciprionResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Update success!.",
                        Data = null
                    };
                }
                else
                {
                    return new BaseResponse<GradeDesciprionResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found GradeDesciprion!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<GradeDesciprionResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }
    }
}
