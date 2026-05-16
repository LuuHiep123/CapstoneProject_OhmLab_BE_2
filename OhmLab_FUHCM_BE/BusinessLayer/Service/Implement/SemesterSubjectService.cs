using AutoMapper;
using BusinessLayer.RequestModel.SemesterSubject;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.Room;
using BusinessLayer.ResponseModel.SemesterSubject;
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
    public class SemesterSubjectService : ISemesterSubjectService
    {

        private readonly ISemesterSubjectRepository _semesterSubjectRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SemesterSubjectService(IMapper mapper , ISemesterSubjectRepository semesterSubjectRepository, ISemesterRepository semesterRepository, ISubjectRepository subjectRepository )
        {
            _semesterSubjectRepository = semesterSubjectRepository;
            _subjectRepository = subjectRepository;
            _semesterRepository = semesterRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<SemesterSubjectResponseModel>> CreateSemesterSubjectAsync(CreateSemesterSubjectRequestModel model)
        {
            try
            {
                var semester = await _semesterRepository.GetByIdAsync( model.SemesterId );
                if (semester == null)
                {
                    return new BaseResponse<SemesterSubjectResponseModel>
                    {
                        Code = 404,
                        Success = true,
                        Message = "khong tim thay semester",
                        Data = null
                    };
                }
                var subject = await _subjectRepository.GetSubjectById(model.SubjectId);
                if (subject == null)
                {
                    return new BaseResponse<SemesterSubjectResponseModel>
                    {
                        Code = 404,
                        Success = true,
                        Message = "khong tim thay subject",
                        Data = null
                    };
                }

                var semesterSubject = _mapper.Map<SemesterSubject>(model);

                semesterSubject.SemesterSubject1 = "Valid";

                var result = await _semesterSubjectRepository.AddAsync(semesterSubject);


                return new BaseResponse<SemesterSubjectResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Tao SemesterSubject thanh cong",
                    Data = null
                };
            }
            catch (System.Exception ex)
            {
                return new BaseResponse<SemesterSubjectResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<SemesterSubjectResponseModel>> GetAllAsync(GetAllSemesterSubjectRequestModel model)
        {
            try
            {
                var listSemesterSubject = await _semesterSubjectRepository.GetAllAsync();
                if (!string.IsNullOrEmpty(model.status))
                {
                    listSemesterSubject = listSemesterSubject.Where(ss => ss.SemesterSubject1.ToLower().Equals(model.status.ToLower())).ToList();
                }
                var result = _mapper.Map<List<SemesterSubjectResponseModel>>(listSemesterSubject);

                // Nếu không có lỗi, thực hiện phân trang
                var pagedUsers = result// Giả sử result là danh sách người dùng
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<SemesterSubjectResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<SemesterSubjectResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pagedUsers.PageNumber,
                            Size = pagedUsers.PageSize,
                            Sort = "Ascending",
                            Order = "Name",
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
                return new DynamicResponse<SemesterSubjectResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = null,
                    Data = null,
                };
            }
        }

        public async Task<BaseResponse<SemesterSubjectResponseModel>> GetByIdAsync(int id)
        {
            try
            {
                var semesterSubject = await _semesterSubjectRepository.GetByIdAsync(id);
                if (semesterSubject.SemesterSubject1.Equals("Delete"))
                {
                    return new BaseResponse<SemesterSubjectResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "not found SemesterSubject",
                        Data = null,
                    };
                }
                var result = _mapper.Map<SemesterSubjectResponseModel>(semesterSubject);
                return new BaseResponse<SemesterSubjectResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "list SemesterSubject ",
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SemesterSubjectResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<SemesterSubjectResponseModel>> GetBySemesterIdAndSubjectIdAsync(int semesterId, int subjectId)
        {
            try
            {
                var semesterSubject = await _semesterSubjectRepository.GetBySemesterIdAngSubjectIdAsync(semesterId,subjectId);
                if (semesterSubject.SemesterSubject1.Equals("Delete"))
                {
                    return new BaseResponse<SemesterSubjectResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "not found SemesterSubject",
                        Data = null,
                    };
                }
                var result = _mapper.Map<SemesterSubjectResponseModel>(semesterSubject);
                return new BaseResponse<SemesterSubjectResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SemesterSubjectResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<List<SemesterSubjectResponseModel>>> GetBySemesterIdAsync(int semesterId)
        {
            try
            {
                var semesterSubject = await _semesterSubjectRepository.GetBySemesterIdAsync(semesterId);
                var result = _mapper.Map<List<SemesterSubjectResponseModel>>(semesterSubject);
                return new BaseResponse<List<SemesterSubjectResponseModel>>()
                {
                    Code = 200,
                    Success = false,
                    Message = "list SemesterSubject by SemesterId",
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<SemesterSubjectResponseModel>>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<List<SemesterSubjectResponseModel>>> GetBySubjectIdAsync(int subjectId)
        {
            try
            {
                var semesterSubject = await _semesterSubjectRepository.GetBySemesterIdAsync(subjectId);
                var result = _mapper.Map<List<SemesterSubjectResponseModel>>(semesterSubject);
                return new BaseResponse<List<SemesterSubjectResponseModel>>()
                {
                    Code = 200,
                    Success = false,
                    Message = "list SemesterSubject by SubjectId",
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<SemesterSubjectResponseModel>>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<SemesterSubjectResponseModel>> UpdateAsync(int id, UpdateSemesterSubjectRequestModel model)
        {
            try
            {
                var semesterSubject = await _semesterSubjectRepository.GetByIdAsync(id);
                if (semesterSubject != null && !semesterSubject.SemesterSubject1.Equals("Delete"))
                {
                    var result = _mapper.Map(model, semesterSubject);
                    await _semesterSubjectRepository.UpdateSemesterSubjectAsync(result);
                    return new BaseResponse<SemesterSubjectResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Update success!.",
                        Data = _mapper.Map<SemesterSubjectResponseModel>(result)
                    };
                }
                else
                {
                    return new BaseResponse<SemesterSubjectResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found SemesterSubject!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<SemesterSubjectResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }
    }
}
