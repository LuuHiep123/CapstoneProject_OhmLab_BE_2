using AutoMapper;
using BusinessLayer.RequestModel.Grade;
using BusinessLayer.ResponseModel.Assignment;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.EquipmentType;
using BusinessLayer.ResponseModel.Grade;
using BusinessLayer.ResponseModel.KitAccessory;
using DataLayer.DBContext;
using DataLayer.Entities;
using DataLayer.Repository;
using DataLayer.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList.Extensions;

namespace BusinessLayer.Service.Implement
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IGradeDescriptionRepository _gradeDescriptionRepository;
        private readonly IRegistrationScheduleRepository _registrationScheduleRepository;
        private readonly ITeamUserRepository _teamUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public GradeService(
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

        public async Task<BaseResponse<ResponseModel.Grade.GradeResponseModel>> CreateGrade(CreateGradeRequestModel model, Guid TeacherId)
        {
            try
            {
                if (model.GradeScore < 0)
                {
                    return new BaseResponse<GradeResponseModel>()
                    {
                        Code = 401,
                        Success = false,
                        Message = "Do not enter grade negative numbers!",
                        Data = null
                    };
                }

                var registrationSchedule = await _registrationScheduleRepository.GetRegistrationScheduleById(model.RegistraionScheduleId);
                if (registrationSchedule == null)
                {
                    return new BaseResponse<ResponseModel.Grade.GradeResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "not found registrationSchedule",
                        Data = null
                    };
                }

                var teacher = await _userRepository.GetUserById(TeacherId);
                if (teacher == null)
                {
                    return new BaseResponse<ResponseModel.Grade.GradeResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "not found teacher",
                        Data = null
                    };
                }

                var team = await _teamUserRepository.GetByIdAsync(model.TeamId);
                if (team == null)
                {
                    return new BaseResponse<ResponseModel.Grade.GradeResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "not found team",
                        Data = null
                    };
                }



                var grade = _mapper.Map<Grade>(model);
                grade.GradeDate = DateTime.Now;
                grade.TeacherId = TeacherId;
                grade.GradeStatus = "Valid";
                await _gradeRepository.CreateAsync(grade);

                List<TeamUser> listTeamUser = await _teamUserRepository.GetByTeamIdAsync(model.TeamId); 
                foreach(var teamuser in listTeamUser)
                {
                    var gradeDescription = new GradeDescription()
                    {
                        GradeId = grade.GradeId,
                        StudentId = teamuser.UserId,
                        ClassId = registrationSchedule.ClassId,
                        LabId = registrationSchedule.LabId,
                        GradeDescriptionScore = grade.GradeScore,
                        GradeDescriptionDescription = null,
                        GradeDescriptionStatus = "Valid"
                    };
                    await _gradeDescriptionRepository.CreateGradeDescription(gradeDescription);
                }

                return new BaseResponse<ResponseModel.Grade.GradeResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Create Grade success!",
                    Data = _mapper.Map<ResponseModel.Grade.GradeResponseModel>(grade)
                };      

            }
            catch (Exception ex)
            {
                return new BaseResponse<ResponseModel.Grade.GradeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<DynamicResponse<GradeResponseModel>> GetAllGrade(GetAllGradeRequestModel model)
        {
            try
            {
                var listGrade = await _gradeRepository.GetAllAsync();

                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    listGrade = listGrade.Where(g => g.Team.TeamName.ToLower().Contains(model.keyWord.ToLower())).ToList();

                }

                if (!string.IsNullOrEmpty(model.status))
                {
                    listGrade = listGrade.Where(g => g.GradeStatus.ToLower().Equals(model.status)).ToList();
                }
                var result = _mapper.Map<List<GradeResponseModel>>(listGrade);

                // Nếu không có lỗi, thực hiện phân trang
                var pagedUsers = result// Giả sử result là danh sách người dùng
                    .OrderBy(g => g.GradeId) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<GradeResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<GradeResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pagedUsers.PageNumber,
                            Size = pagedUsers.PageSize,
                            Sort = "Ascending",
                            Order = "Team Name",
                            TotalPage = pagedUsers.PageCount,
                            TotalItem = pagedUsers.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.status,
                        },
                        PageData = pagedUsers.ToList(),
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<GradeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = null,
                    Data = null,
                };
            }
        }

        public async Task<BaseResponse<GradeResponseModel>> GetGradeOfTeamById(int gradeId)
        {
            try
            {
                var grade = await _gradeRepository.GetByIdAsync(gradeId);
                if (grade != null || !grade.GradeStatus.Equals("Delete"))
                {
                    var result = _mapper.Map<GradeResponseModel>(grade);
                    return new BaseResponse<GradeResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = null,
                        Data = result
                    };
                }
                else
                {
                    return new BaseResponse<GradeResponseModel>()
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
                return new BaseResponse<GradeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<List<GradeResponseModel>>> GetGradeOfTeamByRegistrationScheduleId(int registrationScheduleId)
        {
            try
            {
                var grade = await _gradeRepository.GetAllAsync();
                grade = grade.Where(g => g.RegistraionScheduleId == registrationScheduleId).ToList();
                return new BaseResponse<List<GradeResponseModel>>()
                {
                    Code = 200,
                    Success = true,
                    Message = "list grade by RegisterScheduleId!",
                    Data = _mapper.Map<List<GradeResponseModel>>(grade)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<GradeResponseModel>>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<List<GradeResponseModel>>> GetGradeOfTeamByRegistrationScheduleIdAndTeamId(GetGradeOfTeamByRegistrationScheduleIdAndTeamId model)
        {
            try
            {
                var grade = await _gradeRepository.GetAllAsync();
                grade = grade.Where(g => g.RegistraionScheduleId == model.RegistrationScheduleId && g.TeamId == model.TeamId).ToList();
                return new BaseResponse<List<GradeResponseModel>>()
                {
                    Code = 200,
                    Success = true,
                    Message = "list grade by RegisterScheduleId and TeamId!",
                    Data = _mapper.Map<List<GradeResponseModel>>(grade)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<GradeResponseModel>>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<List<GradeResponseModel>>> GetGradeOfTeamByTeamId(int teamId)
        {
            try
            {
                var grade = await _gradeRepository.GetAllAsync();
                grade = grade.Where(g => g.TeamId == teamId).ToList();
                return new BaseResponse<List<GradeResponseModel>>()
                {
                    Code = 200,
                    Success = true,
                    Message = "list grade by TeamId!",
                    Data = _mapper.Map<List<GradeResponseModel>>(grade)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<GradeResponseModel>>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<GradeResponseModel>> UpdateGrade(UpdateGradeRequestModel model)
        {
            try
            {
                if (model.GradeScore < 0)
                {
                    return new BaseResponse<GradeResponseModel>()
                    {
                        Code = 401,
                        Success = false,
                        Message = "Do not enter grade negative numbers!",
                        Data = null
                    };
                }
                var grade = await _gradeRepository.GetByIdAsync(model.GradeId);
                if (grade != null)
                {
                    var result = _mapper.Map(model, grade);
                    await _gradeRepository.UpdateAsync(result);

                    var gradeDesciption = await _gradeDescriptionRepository.GetAllGradeDescription();
                    gradeDesciption = gradeDesciption.Where(gd => gd.GradeId == grade.GradeId).ToList();

                    foreach(var gardedes in gradeDesciption)
                    {
                        gardedes.GradeDescriptionScore = grade.GradeScore;
                        await _gradeDescriptionRepository.UpdateGradeDescription(gardedes);
                    }
                    return new BaseResponse<GradeResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Update success!.",
                        Data = null
                    };
                }
                else
                {
                    return new BaseResponse<GradeResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Grade!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<GradeResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }
    }
}
