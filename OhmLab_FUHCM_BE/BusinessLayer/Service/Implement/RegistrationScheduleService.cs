using AutoMapper;
using BusinessLayer.RequestModel.RegistrationSchedule;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.RegistrationSchedule;
using BusinessLayer.ResponseModel.Schedule;
using BusinessLayer.ResponseModel.Slot;
using BusinessLayer.ResponseModel.TeamKit;
using BusinessLayer.ResponseModel.User;
using DataLayer.Entities;
using DataLayer.Repository;
using DataLayer.Repository.Implement;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using X.PagedList.Extensions;

namespace BusinessLayer.Service.Implement
{
    public class RegistrationScheduleService : IRegistrationScheduleService
    {

        private readonly IRegistrationScheduleRepository _registrationScheduleRepository;
        private readonly IClassRepository _classRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ISlotRepository _slotRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ILabRepository _labRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClassUserRepository _classUserRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public RegistrationScheduleService(IRoomRepository roomRepository ,IClassUserRepository classUserRepository, IScheduleRepository scheduleRepository, ILabRepository labRepository, IUserRepository userRepository, ISlotRepository slotRepository, IClassRepository classRepository, IRegistrationScheduleRepository registrationScheduleRepository, IConfiguration configuration, IMapper mapper, IMemoryCache memoryCache)
        {
            _roomRepository = roomRepository;
            _registrationScheduleRepository = registrationScheduleRepository;
            _labRepository = labRepository;
            _scheduleRepository = scheduleRepository;
            _classUserRepository = classUserRepository;
            _userRepository = userRepository;
            _slotRepository = slotRepository;
            _classRepository = classRepository;
            _configuration = configuration;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<BaseResponse<RegistrationScheduleAllResponseModel>> AcceptRegistrtionSchedule(AcceptRejectRegistrationScheduleRequestModel model)
        {
            try
            {
                var registrationSchedule = await _registrationScheduleRepository.GetRegistrationScheduleById(model.RegistrationScheduleId);
                if (registrationSchedule == null)
                {
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found RegistrationSchedule!"

                    };
                }
                else
                {
                    if (!registrationSchedule.RegistraionScheduleStatus.ToLower().Equals("pending"))
                    {
                        return new BaseResponse<RegistrationScheduleAllResponseModel>()
                        {
                            Code = 401,
                            Success = false,
                            Message = "RegistrationSchedule has been processed!"

                        };
                    }

                    if (registrationSchedule.RegistraionScheduleStatus.ToLower().Equals("accept"))
                    {
                        return new BaseResponse<RegistrationScheduleAllResponseModel>()
                        {
                            Code = 401,
                            Success = false,
                            Message = "RegistrationSchedule has accept!"

                        };
                    }
                    registrationSchedule.RegistraionScheduleStatus = "Accept";
                    await SendMailAccept(registrationSchedule);
                    await _registrationScheduleRepository.UpdateRegistrationSchedule(registrationSchedule);

                    var listRegistrationSchedule = await _registrationScheduleRepository.GetAllRegistrationSchedule();
                    listRegistrationSchedule.Where(rs =>
                        (rs.RegistraionScheduleDate.Date == registrationSchedule.RegistraionScheduleDate.Date && rs.SlotId == registrationSchedule.SlotId)
                        &&
                        (rs.RoomId == registrationSchedule.RoomId && rs.RegistraionScheduleStatus.ToLower().Equals("pending"))
                    );
                    foreach (var rs in listRegistrationSchedule)
                    {
                        var reject = new RejectRegistrationScheduleModel()
                        {
                            RegistrationScheduleId = rs.RegistraionScheduleId,
                            RegistrationScheduleNote = "Another schedule has been approved."
                        };
                        await RejectRegistrtionSchedule(reject);
                    }

                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Accept RegistrationSchedule success!",
                        Data = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<RegistrationScheduleAllResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse> SendMailAccept(RegistraionSchedule registrationSchedule)
        {
            try
            {
                var user = await _userRepository.GetUserById(registrationSchedule.TeaacherId);
                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("ohmlabsystem@gmail.com", "rdpj tier eipp epmd");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("ohmlabsystem@gmail.com");
                mailMessage.To.Add(user.UserEmail);
                mailMessage.Subject = "Thông báo trạng thái đăng ký lịch thực hành";

                mailMessage.Body = @"
<html>
<head>
  <style>
    body {
      font-family: Arial, sans-serif;
      line-height: 1.6;
      background-color: #f8f9fa;
    }
    .container {
      padding: 20px;
      background-color: #ffffff;
      border: 1px solid #ddd;
      border-radius: 10px;
      max-width: 600px;
      margin: 40px auto;
      box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    }
    .header {
      font-size: 20px;
      font-weight: bold;
      text-align: center;
      color: #007BFF;
      margin-bottom: 20px;
    }
    .content {
      font-size: 16px;
      color: #333;
      background-color: #f8f9ff;
      padding: 15px;
      border-radius: 8px;
    }
    .footer {
      font-size: 12px;
      color: #888;
      text-align: center;
      margin-top: 25px;
    }
    .highlight {
      color: #007BFF;
      font-weight: bold;
    }
    ul {
      list-style-type: none;
      padding-left: 0;
    }
    li {
      margin-bottom: 6px;
    }
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>Thông tin lịch thực hành</div>
    <div class='content'>
      <p><strong>Thông tin lịch thực hành:</strong></p>
      <ul>
        <li> <strong>Lớp:</strong> " + registrationSchedule.Class.ClassName + @"</li>
        <li> <strong>Bài lab thực hành:</strong> " + registrationSchedule.Lab.LabName + @"</li>
        <li> <strong>Ngày đăng ký:</strong> " + registrationSchedule.RegistraionScheduleDate.ToString("dd/MM/yyyy") + @"</li>
        <li> <strong>Phòng học:</strong> " + registrationSchedule.Room.RoomName + @"</li>
        <li> <strong>Slot:</strong> " + registrationSchedule.Slot.SlotName + @"</li>
        <li> <strong>Giờ học:</strong> " + registrationSchedule.Slot.SlotStartTime + " - " + registrationSchedule.Slot.SlotEndTime + @"</li>
        <li> <strong>Trạng thái:</strong> <span class='highlight'>Accept</span></li>
      </ul>

      <p style='margin-top:15px;'>Lịch thực hành của bạn đã được duyệt. Vui lòng kiểm tra lại thông tin trên hệ thống trước khi đến phòng lab.</p>
    </div>
    <div class='footer'>
      &copy; 2024 OHM Lab System. All rights reserved.
    </div>
  </div>
</body>
</html>";

                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);

                return new BaseResponse
                {
                    Code = 200,
                    Message = "Send succeed."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = 400,
                    Message = "An error occurred: " + ex.Message
                };
            }
        }


        public async Task<BaseResponse> SendMailReject(RegistraionSchedule registrationSchedule)
        {
            try
            {
                var user = await _userRepository.GetUserById(registrationSchedule.TeaacherId);
                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("ohmlabsystem@gmail.com", "rdpj tier eipp epmd");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("ohmlabsystem@gmail.com");
                mailMessage.To.Add(user.UserEmail);
                mailMessage.Subject = "Thông báo trạng thái đăng ký lịch thực hành";

                mailMessage.Body = @"
<html>
<head>
  <style>
    body {
      font-family: Arial, sans-serif;
      line-height: 1.6;
      background-color: #f8f9fa;
    }
    .container {
      padding: 20px;
      background-color: #ffffff;
      border: 1px solid #ddd;
      border-radius: 10px;
      max-width: 600px;
      margin: 40px auto;
      box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    }
    .header {
      font-size: 20px;
      font-weight: bold;
      text-align: center;
      color: #007BFF;
      margin-bottom: 20px;
    }
    .content {
      font-size: 16px;
      color: #333;
      background-color: #f8f9ff;
      padding: 15px;
      border-radius: 8px;
    }
    .footer {
      font-size: 12px;
      color: #888;
      text-align: center;
      margin-top: 25px;
    }
    .highlight {
      color: #007BFF;
      font-weight: bold;
    }
    ul {
      list-style-type: none;
      padding-left: 0;
    }
    li {
      margin-bottom: 6px;
    }
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>Thông tin lịch thực hành</div>
    <div class='content'>
      <p><strong>Thông tin lịch thực hành:</strong></p>
      <ul>
        <li>- <strong>Lớp:</strong> " + registrationSchedule.Class.ClassName + @"</li>
        <li>- <strong>Bài lab thực hành:</strong> " + registrationSchedule.Lab.LabName + @"</li>
        <li>- <strong>Ngày đăng ký:</strong> " + registrationSchedule.RegistraionScheduleDate.ToString("dd/MM/yyyy") + @"</li>
        <li>- <strong>Phòng học:</strong> " + registrationSchedule.Room.RoomName + @"</li>
        <li>- <strong>Slot:</strong> " + registrationSchedule.Slot.SlotName + @"</li>
        <li>- <strong>Giờ học:</strong> " + registrationSchedule.Slot.SlotStartTime + " - " + registrationSchedule.Slot.SlotEndTime + @"</li>
        <li>- <strong>Description:</strong> " + registrationSchedule.RegistraionScheduleNote + @"</li>
        <li>- <strong>Trạng thái:</strong> <span class='highlight'>Reject</span></li>
      </ul>

      <p style='margin-top:15px;'>Lịch thực hành của bạn bị từ chối. Vui lòng kiểm tra lại thông tin trên hệ thống.</p>
    </div>
    <div class='footer'>
      &copy; 2024 OHM Lab System. All rights reserved.
    </div>
  </div>
</body>
</html>";

                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);

                return new BaseResponse
                {
                    Code = 200,
                    Message = "Send succeed."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = 400,
                    Message = "An error occurred: " + ex.Message
                };
            }
        }

        public async Task<BaseResponse> SendMailCancel(RegistraionSchedule registrationSchedule)
        {
            try
            {
                var user = await _userRepository.GetUserById(registrationSchedule.TeaacherId);
                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("ohmlabsystem@gmail.com", "rdpj tier eipp epmd");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("ohmlabsystem@gmail.com");
                mailMessage.To.Add(user.UserEmail);
                mailMessage.Subject = "Thông báo trạng thái đăng ký lịch thực hành";

                mailMessage.Body = @"
<html>
<head>
  <style>
    body {
      font-family: Arial, sans-serif;
      line-height: 1.6;
      background-color: #f8f9fa;
    }
    .container {
      padding: 20px;
      background-color: #ffffff;
      border: 1px solid #ddd;
      border-radius: 10px;
      max-width: 600px;
      margin: 40px auto;
      box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    }
    .header {
      font-size: 20px;
      font-weight: bold;
      text-align: center;
      color: #007BFF;
      margin-bottom: 20px;
    }
    .content {
      font-size: 16px;
      color: #333;
      background-color: #f8f9ff;
      padding: 15px;
      border-radius: 8px;
    }
    .footer {
      font-size: 12px;
      color: #888;
      text-align: center;
      margin-top: 25px;
    }
    .highlight {
      color: #007BFF;
      font-weight: bold;
    }
    ul {
      list-style-type: none;
      padding-left: 0;
    }
    li {
      margin-bottom: 6px;
    }
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>Thông tin lịch thực hành</div>
    <div class='content'>
      <p><strong>Thông tin lịch thực hành:</strong></p>
      <ul>
        <li>- <strong>Lớp:</strong> " + registrationSchedule.Class.ClassName + @"</li>
        <li>- <strong>Bài lab thực hành:</strong> " + registrationSchedule.Lab.LabName + @"</li>
        <li>- <strong>Ngày đăng ký:</strong> " + registrationSchedule.RegistraionScheduleDate.ToString("dd/MM/yyyy") + @"</li>
        <li>- <strong>Phòng học:</strong> " + registrationSchedule.Room.RoomName + @"</li>
        <li>- <strong>Slot:</strong> " + registrationSchedule.Slot.SlotName + @"</li>
        <li>- <strong>Giờ học:</strong> " + registrationSchedule.Slot.SlotStartTime + " - " + registrationSchedule.Slot.SlotEndTime + @"</li>
        <li>- <strong>Description:</strong> " + registrationSchedule.RegistraionScheduleNote + @"</li>
        <li>- <strong>Trạng thái:</strong> <span class='highlight'>" + registrationSchedule.RegistraionScheduleStatus + @"</span></li>
      </ul>

      <p style='margin-top:15px;'>Bạn đã hủy lịch thực hành. Vui lòng kiểm tra lại thông tin trên hệ thống.</p>
    </div>
    <div class='footer'>
      &copy; 2024 OHM Lab System. All rights reserved.
    </div>
  </div>
</body>
</html>";

                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);

                return new BaseResponse
                {
                    Code = 200,
                    Message = "Send succeed."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = 400,
                    Message = "An error occurred: " + ex.Message
                };
            }
        }

        //public async Task<BaseResponse<RegistrationScheduleAllResponseModel>> CheckDupplicateRegistrtionSchedule(CheckDupplicateRegitrationScheduleRequestModel model)
        //{
        //    try
        //    {
        //        var listRegistrationSchedule = await _registrationScheduleRepository.GetAllRegistrationSchedule();
        //        var listCheck = listRegistrationSchedule.Where(rs => (rs.RegistraionScheduleDate == model.RegistrationScheduleDate && rs.SlotId == model.SlotId) && (!rs.RegistraionScheduleStatus.ToLower().Equals("reject")));

        //        if (listCheck.Any())
        //        {
        //            return new BaseResponse<RegistrationScheduleAllResponseModel>()
        //            {
        //                Code = 200,
        //                Success = false,
        //                Message = "Dupplicate RegistrationSchedule!"

        //            };
        //        }
        //        else
        //        {
        //            return new BaseResponse<RegistrationScheduleAllResponseModel>()
        //            {
        //                Code = 200,
        //                Success = true,
        //                Message = "Not dupplicate RegistrationSchedule!",
        //                Data = null
        //            };
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<RegistrationScheduleAllResponseModel>()
        //        {
        //            Code = 500,
        //            Success = false,
        //            Message = "Server Error!"

        //        };
        //    }
        //}

        public async Task<BaseResponse<RegistrationScheduleAllResponseModel>> CreateRegistrationSchedule(CreateRegistrationScheduleRequestModel model)
        {
            try
            {
                var Class = await _classRepository.GetByIdAsync(model.ClassId);
                if (Class == null)
                {
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Class!"

                    };
                }


                var lab = await _labRepository.GetLabById(model.LabId);
                if (lab == null)
                {
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found lab!"

                    };
                }

                var room = await _roomRepository.GetRoomById(model.RoomId);
                if (room == null)
                {
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found room!"

                    };
                }

                var slot = await _slotRepository.GetByIdAsync(model.SlotId);
                if (slot == null)
                {
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found slot!"

                    };
                }

                var teacher = await _userRepository.GetUserById(model.TeaacherId);
                if (teacher == null)
                {
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Teacher!"

                    };
                }

                var registrationSchedule = _mapper.Map<RegistraionSchedule>(model);
                registrationSchedule.RegistraionSchedule_Url_Img_Checkout = null;
                registrationSchedule.RegistraionScheduleStatus = "Pending";
                registrationSchedule.RegistraionScheduleCreateDate = DateTime.Now;

                await _registrationScheduleRepository.CreateRegistrationSchedule(registrationSchedule);
                return new BaseResponse<RegistrationScheduleAllResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Create RegistrationSchedule success!",
                    Data = null
                };


            }
            catch (Exception ex)
            {
                return new BaseResponse<RegistrationScheduleAllResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<RegistrationScheduleAllResponseModel>> DeleteRegistrationSchedule(int id)
        {
            try
            {
                var registrationSchedule = await _registrationScheduleRepository.GetRegistrationScheduleById(id);
                if(registrationSchedule == null)
                {
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Teacher!"

                    };
                }
                else
                {
                    registrationSchedule.RegistraionScheduleStatus = "Delete";
                    await _registrationScheduleRepository.UpdateRegistrationSchedule(registrationSchedule);
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Delete RegistrationSchedule success!",
                        Data = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<RegistrationScheduleAllResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<DynamicResponse<RegistrationScheduleAllResponseModel>> GetAllRegistrationSchedule(GetAllRegistrationScheduleRequestModel model)
        {
            try
            {
                var listReschedule = await _registrationScheduleRepository.GetAllRegistrationSchedule();
                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<RegistraionSchedule> listRescheduleByName = listReschedule.Where(rs => rs.RegistraionScheduleName.ToLower().Contains(model.keyWord.ToLower())).ToList();

                }
                if (!string.IsNullOrEmpty(model.status))
                {
                    listReschedule = listReschedule.Where(rs => rs.RegistraionScheduleStatus.ToLower().Contains(model.status.ToLower())).ToList();
                }
                if (model.Date.HasValue)
                {
                    listReschedule = listReschedule.Where(rs => rs.RegistraionScheduleDate.Date == model.Date.Value.Date).ToList();
                }
                if (model.SlotId != 0)
                {
                    listReschedule = listReschedule.Where(rs => rs.SlotId == model.SlotId).ToList();
                }
                var result = _mapper.Map<List<RegistrationScheduleAllResponseModel>>(listReschedule);

                var listRegistrationschedulePractice = new List<RegistrationScheduleAllResponseModel>();
                var listRegistrationscheduleOther = new List<RegistrationScheduleAllResponseModel>();
                var listRegistrationschedulePracticeFull = new List<RegistrationScheduleAllResponseModel>();


                foreach (var rs in result)
                {
                    if (rs.RegistraionScheduleDescription.Equals("0"))
                    {
                        var check = await CheckNumberOfPractice(rs);
                        if(check.check)
                        {
                            rs.Note = "Not full practice";
                            rs.Registration_Total_Practice = check.total;
                            rs.Registration_Number_Practice = check.count;
                            listRegistrationschedulePractice.Add(rs);
                        }
                        else
                        {
                            rs.Note = "Full practice";
                            rs.Registration_Total_Practice = check.total;
                            rs.Registration_Number_Practice = check.count;
                            listRegistrationschedulePracticeFull.Add(rs);
                        }
                    }
                    else
                    {
                        listRegistrationscheduleOther.Add(rs);
                    }
                }

                listRegistrationschedulePractice = listRegistrationschedulePractice
                    .OrderBy(x => x.RegistraionScheduleCreateDate)
                    .ToList();

                listRegistrationscheduleOther = listRegistrationscheduleOther
                    .OrderBy(x => x.RegistraionScheduleCreateDate)
                    .ToList();

                listRegistrationschedulePracticeFull = listRegistrationschedulePracticeFull
                    .OrderBy(x => x.RegistraionScheduleCreateDate)
                    .ToList();

                var finalList = listRegistrationschedulePractice
                    .Concat(listRegistrationschedulePracticeFull)
                    .Concat(listRegistrationscheduleOther)
                    .ToList();

                // Nếu không có lỗi, thực hiện phân trang
                var pagedUsers = finalList// Giả sử result là danh sách người dùng
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<RegistrationScheduleAllResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<RegistrationScheduleAllResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pagedUsers.PageNumber,
                            Size = pagedUsers.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
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
                return new DynamicResponse<RegistrationScheduleAllResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = null,
                    Data = null,
                };
            }
        }

        public async Task<CountRegisterScheduleRequestModel> CheckNumberOfPractice(RegistrationScheduleAllResponseModel model)
        {
            try
            {
                var lab = await _labRepository.GetLabById(model.LabId);
                var registrationSchedulee = await _registrationScheduleRepository.GetAllRegistrationSchedule();
                registrationSchedulee = registrationSchedulee
                    .Where(rs => rs.RegistraionScheduleStatus.ToLower() == "accept"
                              || rs.RegistraionScheduleStatus.ToLower() == "inprogress"
                              || rs.RegistraionScheduleStatus.ToLower() == "complete")
                    .ToList();
                registrationSchedulee = registrationSchedulee.Where(rs => rs.LabId == model.LabId && rs.ClassId == model.ClassId).ToList();
                int i = registrationSchedulee.Count();
                if (i >= lab.LabNumberOfPractice)
                {
                    return new CountRegisterScheduleRequestModel() 
                    {
                        check = false,
                        count = i,
                        total = lab.LabNumberOfPractice
                    };
                }
                else
                {
                    return new CountRegisterScheduleRequestModel()
                    {
                        check = true,
                        count = i,
                        total = lab.LabNumberOfPractice
                    };
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BaseResponse<List<RegistrationScheduleAllResponseModel>>> GetAllRegistrationScheduleByTeacherId(Guid teacherId)
        {
            try
            {
                var listReschedule = await _registrationScheduleRepository.GetAllRegistrationSchedule();

                var listRescheduleTeacher = listReschedule.Where(rs => rs.TeaacherId.Equals(teacherId)).ToList();
                var result = _mapper.Map<List<RegistrationScheduleAllResponseModel>>(listRescheduleTeacher);

                return new BaseResponse<List<RegistrationScheduleAllResponseModel>>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<RegistrationScheduleAllResponseModel>>()
                {
                    Code = 500,
                    Success = false,
                    Message = null,
                    Data = null,
                };
            }
        }

        public async Task<BaseResponse<RegistrationScheduleAllResponseModel>> GetRegistrationScheduleById(int id)
        {
            try
            {
                var reschedule = await _registrationScheduleRepository.GetRegistrationScheduleById(id);
                if(reschedule == null)
                {
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found RegistrationSchedule!"

                    };
                }
                var result = _mapper.Map<RegistrationScheduleAllResponseModel>(reschedule);

                return new BaseResponse<RegistrationScheduleAllResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<RegistrationScheduleAllResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = null,
                    Data = null,
                };
            }
        }

        public async Task<BaseResponse<List<RegistrationScheduleAllResponseModel>>> GetRegistrationScheduleByStudentId(Guid studentId)
        {
            try
            {
                var listRegistrationSchedule = await _registrationScheduleRepository.GetAllRegistrationSchedule();
                var listClassUser = await _classUserRepository.GetByUserIdAsync(studentId);

                var filteredRegistrationSchedules = listRegistrationSchedule
                    .Where(s => listClassUser.Any(cu => cu.ClassId == s.ClassId))
                    .ToList();

                var result = _mapper.Map<List<RegistrationScheduleAllResponseModel>>(filteredRegistrationSchedules);
                return new BaseResponse<List<RegistrationScheduleAllResponseModel>>()
                {
                    Code = 200,
                    Success = true,
                    Message = "List RegisterSchedule by StudentId",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<RegistrationScheduleAllResponseModel>>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        //public async Task<BaseResponse<List<SlotResponseModel>>> GetSlotEmptyByDate(DateTime date)
        //{

        //    try
        //    {
        //        var listSchedule = await _scheduleRepository.GetAllAsync();
        //        var listScheduleFilter = listSchedule.Where(s => s.ScheduleDate == date);

        //        var listRegistrationSchedule = await _registrationScheduleRepository.GetAllRegistrationSchedule();
        //        var listRegistrationScheduleFilter = listRegistrationSchedule.Where(rs => rs.RegistraionScheduleDate == date && rs.RegistraionScheduleStatus.ToLower().Equals("accept"));

        //        var listSlot = await _slotRepository.GetAllAsync();
        //        var listSlotDupplicate = new List<Slot>();
        //        if (listScheduleFilter.Any())
        //        {
        //            foreach (var schedule in listScheduleFilter)
        //            {
        //                listSlotDupplicate.Add(schedule.Class.ScheduleType.Slot);
        //            }
        //        }
        //        if (listRegistrationScheduleFilter.Any())
        //        {
        //            foreach (var registrationSchedule in listRegistrationScheduleFilter)
        //            {
        //                listSlotDupplicate.Add(registrationSchedule.Slot);
        //            }
        //        }
        //        if(listSlotDupplicate.Count() == listSlot.Count())
        //        {
        //            return new BaseResponse<List<SlotResponseModel>>()
        //            {
        //                Code = 200,
        //                Success = true,
        //                Message = "Hết slot cho ngày đã chọn",
        //                Data = null,

        //            };
        //        }
        //        if (listSlotDupplicate.Any())
        //        {
        //            var result = listSlot.Except(listSlotDupplicate).ToList();
        //            var resultMap = _mapper.Map<List<SlotResponseModel>>(result);
        //            return new BaseResponse<List<SlotResponseModel>>()
        //            {
        //                Code = 200,
        //                Success = true,
        //                Message = null,
        //                Data = resultMap,

        //            };
        //        }
        //        else
        //        {
        //            var resultMap = _mapper.Map<List<SlotResponseModel>>(listSlot);
        //            return new BaseResponse<List<SlotResponseModel>>()
        //            {
        //                Code = 200,
        //                Success = true,
        //                Message = null,
        //                Data = resultMap,

        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<List<SlotResponseModel>>()
        //        {
        //            Code = 500,
        //            Success = false,
        //            Message = "Server Error!"

        //        };
        //    }
        //}

        public async Task<BaseResponse<RegistrationScheduleAllResponseModel>> RejectRegistrtionSchedule(RejectRegistrationScheduleModel model)
        {
            try
            {
                var registrationSchedule = await _registrationScheduleRepository.GetRegistrationScheduleById(model.RegistrationScheduleId);
                if (registrationSchedule == null)
                {
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found RegistrationSchedule!"

                    };
                }
                else
                {
                    if (!registrationSchedule.RegistraionScheduleStatus.ToLower().Equals("pending"))
                    {
                        return new BaseResponse<RegistrationScheduleAllResponseModel>()
                        {
                            Code = 401,
                            Success = false,
                            Message = "RegistrationSchedule has been processed!"

                        };
                    }
                    if (registrationSchedule.RegistraionScheduleStatus.ToLower().Equals("reject"))
                    {
                        return new BaseResponse<RegistrationScheduleAllResponseModel>()
                        {
                            Code = 401,
                            Success = false,
                            Message = "RegistrationSchedule has reject!"

                        };
                    }
                    registrationSchedule.RegistraionScheduleStatus = "Reject";
                    if (model.RegistrationScheduleNote != null)
                    {
                        registrationSchedule.RegistraionScheduleNote = model.RegistrationScheduleNote;
                    }
                    await SendMailReject(registrationSchedule);
                    await _registrationScheduleRepository.UpdateRegistrationSchedule(registrationSchedule);
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Reject RegistrationSchedule success!",
                        Data = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<RegistrationScheduleAllResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<RegistrationScheduleAllResponseModel>> UpdateRegistrationSchedule(int id, UpdateRegistrationScheduleRequestModel model)
        {
            try
            {
                var registrationSchedule = await _registrationScheduleRepository.GetRegistrationScheduleById(id);
                if (registrationSchedule == null)
                {
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found RegistrationSchedule!"

                    };
                }
                else
                {
                    var result = _mapper.Map<RegistraionSchedule>(UpdateRegistrationSchedule);
                    result.RegistraionScheduleCreateDate = DateTime.Now;   
                    await _registrationScheduleRepository.UpdateRegistrationSchedule(result);
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Update RegistrationSchedule success!",
                        Data = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<RegistrationScheduleAllResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<RegistrationScheduleAllResponseModel>> CancelRegistrationSchedule(CancelRegistrationScheduleModel model)
        {
            try
            {
                var registrationSchedule = await _registrationScheduleRepository.GetRegistrationScheduleById(model.RegistrationScheduleId);
                if (registrationSchedule == null)
                {
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found RegistrationSchedule!"

                    };
                }
                else
                {
                    registrationSchedule.RegistraionScheduleStatus = "Cancel";
                    if (model.RegistrationScheduleNote != null)
                    {
                        registrationSchedule.RegistraionScheduleNote = model.RegistrationScheduleNote;
                    }             
                    await _registrationScheduleRepository.UpdateRegistrationSchedule(registrationSchedule);
                    await SendMailCancel(registrationSchedule);
                    return new BaseResponse<RegistrationScheduleAllResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Cancel RegistrationSchedule success!",
                        Data = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<RegistrationScheduleAllResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse> TeacherCheckoutSchedule(CheckOutRegistrationScheduleModel model)
        {
            try
            {
                var registrationSchedule = await _registrationScheduleRepository.GetRegistrationScheduleById(model.RegistrationScheduleId);
                if (registrationSchedule == null)
                {
                    return new BaseResponse()
                    {
                        Code = 404,
                        Success = false,
                        Message = "khong tim thay lich hoc!"

                    };
                }

                if (model.RegistraionSchedule_Url_Img_Checkout == null)
                {
                    return new BaseResponse()
                    {
                        Code = 401,
                        Success = false,
                        Message = "phai co hinh anh de checkout!"

                    };
                }
                var room = await _roomRepository.GetRoomById(registrationSchedule.RoomId);
                room.RoomStatus = "Available";
                await _roomRepository.UpdateRoom(room);

                registrationSchedule.RegistraionSchedule_Url_Img_Checkout = model.RegistraionSchedule_Url_Img_Checkout;
                registrationSchedule.RegistraionScheduleStatus = "Completed";
                registrationSchedule.RegistraionScheduleCheckOut = DateTime.Now;
                await _registrationScheduleRepository.UpdateRegistrationSchedule(registrationSchedule);
                return new BaseResponse()
                {
                    Code = 200,
                    Success = true,
                    Message = "check out thanh cong"

                };


            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }
    }
}
