using BusinessLayer.RequestModel.Kit;
using BusinessLayer.RequestModel.Room;
using BusinessLayer.ResponseModel.BaseResponse;
using BusinessLayer.ResponseModel.Kit;
using BusinessLayer.ResponseModel.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IRoomService
    {
        Task<BaseResponse<RoomResponseModel>> CreateRoom(CreateRoomRequestModel model);
        Task<BaseResponse<RoomResponseModel>> DeleteRoom(int id);
        Task<BaseResponse<RoomResponseModel>> UpdateRoom(int id, UpdateRoomRequestModel model);
        Task<DynamicResponse<RoomResponseModel>> GetAllRoom(GetAllRoomRequestModel model);
        Task<BaseResponse<RoomResponseModel>> GetRoomById(int id);
    }
}
