using DataLayer.Entities;
using DataLayer.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IRoomRepository
    {
        public Task<bool> CreateRoom(Room room);
        public Task<bool> UpdateRoom(Room room);
        public Task<bool> DeleteRoom(Room room);
        public Task<List<Room>> GetAllRoom();
        public Task<Room> GetRoomById(int roomId);
        public Task<Room> GetRoomByName(string name);
    }
}
