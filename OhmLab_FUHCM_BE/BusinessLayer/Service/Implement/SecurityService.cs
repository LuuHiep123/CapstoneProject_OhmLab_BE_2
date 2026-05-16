using AutoMapper;
using BusinessLayer.RequestModel.Security;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.Semester;
using DataLayer.Repository;
using DataLayer.Repository.Implement;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Implement
{
    public class SecurityService : IsecurityService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRegistrationScheduleRepository _registrationScheduleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ScheduleTypeService> _logger;

        public SecurityService(IRegistrationScheduleRepository registrationScheduleRepository, IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
            _registrationScheduleRepository = registrationScheduleRepository;
        }

        public async Task<BaseResponse> CheckIn(CheckInSecurityRequestModel model)
        {
            try
            {
                var registrationSchedule = await _registrationScheduleRepository.GetRegistrationScheduleById(model.RegistrationScheduleId);
                if (registrationSchedule == null)
                {
                    return new BaseResponse
                    {
                        Code = 404,
                        Success = false,
                        Message = "Khong tim thay lich hoc!",
                    };
                }
                var room = await _roomRepository.GetRoomById(registrationSchedule.RoomId);
                if (room == null)
                {
                    return new BaseResponse
                    {
                        Code = 404,
                        Success = false,
                        Message = "Khong tim thay phong hoc!",
                    };
                }

                registrationSchedule.RegistraionScheduleStatus = "InProgress";
                room.RoomStatus = "inUse";
                registrationSchedule.RegistraionScheduleCheckIn = DateTime.Now;
                await _registrationScheduleRepository.UpdateRegistrationSchedule(registrationSchedule);
                await _roomRepository.UpdateRoom(room);
                return new BaseResponse
                {
                    Code = 200,
                    Success = true,
                    Message = "mo phong hoc thanh cong!",
                };

            }
            catch (System.Exception ex)
            {
                return new BaseResponse
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                };
            }
        }
    }
}
