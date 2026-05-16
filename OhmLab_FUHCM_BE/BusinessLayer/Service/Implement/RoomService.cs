using AutoMapper;
using BusinessLayer.RequestModel.Room;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.Equipment;
using BusinessLayer.ResponseModel.Room;
using DataLayer.Entities;
using DataLayer.Repository;
using DataLayer.Repository.Implement;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList.Extensions;

namespace BusinessLayer.Service.Implement
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public RoomService(IRoomRepository roomRepository, IConfiguration configuration, IMapper mapper, IMemoryCache memoryCache)
        {
            _roomRepository = roomRepository;
            _configuration = configuration;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }
        public async Task<BaseResponse<RoomResponseModel>> CreateRoom(CreateRoomRequestModel model)
        {
            try
            {
                var room = _mapper.Map<Room>(model);

                var checkRoomName = await _roomRepository.GetRoomByName(model.RoomName);


                room.RoomStatus = "Available";                
                if (checkRoomName != null)
                {
                    return new BaseResponse<RoomResponseModel>
                    {
                        Code = 401,
                        Success = true,
                        Message = "ten phong da co",
                        Data = null
                    };
                }

                var result = await _roomRepository.CreateRoom(room);


                return new BaseResponse<RoomResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Tao phong thanh cong",
                    Data = null
                };
            }
            catch (System.Exception ex)
            {
                return new BaseResponse<RoomResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<RoomResponseModel>> DeleteRoom(int id)
        {
            try
            {


                var room = await _roomRepository.GetRoomById(id);
                if(room == null)
                {
                    return new BaseResponse<RoomResponseModel>
                    {
                        Code = 404,
                        Success = true,
                        Message = "khong tim thay phong",
                        Data = null
                    };
                }
                room.RoomStatus = "Delete";
                await _roomRepository.UpdateRoom(room);
                return new BaseResponse<RoomResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "xoa phong thanh cong",
                    Data = null
                };
            }
            catch (System.Exception ex)
            {
                return new BaseResponse<RoomResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<RoomResponseModel>> GetAllRoom(GetAllRoomRequestModel model)
        {
            try
            {
                var listRoom= await _roomRepository.GetAllRoom();
                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<Room> listRoomByName = listRoom.Where(r => r.RoomName.Contains(model.keyWord)).ToList();

                    listRoom = listRoomByName
                               .GroupBy(r => r.RoomId)
                               .Select(g => g.First())
                               .ToList();
                }
                if (!string.IsNullOrEmpty(model.status))
                {
                    listRoom = listRoom.Where(eq => eq.RoomStatus.ToLower().Equals(model.status.ToLower())).ToList();
                }
                var result = _mapper.Map<List<RoomResponseModel>>(listRoom);

                // Nếu không có lỗi, thực hiện phân trang
                var pagedUsers = result// Giả sử result là danh sách người dùng
                    .OrderBy(r => r.RoomName) // Sắp xếp theo Name tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<RoomResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<RoomResponseModel>()
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
                return new DynamicResponse<RoomResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = null,
                    Data = null,
                };
            }
        }

        public async Task<BaseResponse<RoomResponseModel>> GetRoomById(int id)
        {
            try
            {
                var room = await _roomRepository.GetRoomById(id);
                if (room.RoomStatus.Equals("Delete"))
                {
                    return new BaseResponse<RoomResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "not found room",
                        Data = null,
                    };
                }
                var result = _mapper.Map<RoomResponseModel>(room);
                return new BaseResponse<RoomResponseModel>()
                {
                    Code = 200,
                    Success = false,
                    Message = "list Room type",
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<RoomResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<RoomResponseModel>> UpdateRoom(int id, UpdateRoomRequestModel model)
        {
            try
            {
                var room = await _roomRepository.GetRoomById(id);
                if (room != null && !room.RoomStatus.Equals("Delete"))
                {
                    var result = _mapper.Map(model, room);
                    await _roomRepository.UpdateRoom(result);
                    return new BaseResponse<RoomResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Update success!.",
                        Data = _mapper.Map<RoomResponseModel>(result)
                    };
                }
                else
                {
                    return new BaseResponse<RoomResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Room!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<RoomResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }
    }
}
