using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Implement
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DBContext.db_abadcb_ohmlabContext _context;

        public RoomRepository(DBContext.db_abadcb_ohmlabContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRoom(Room room)
        {
            try
            {
                await _context.Rooms.AddAsync(room);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteRoom(Room room)
        {
            try
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Room>> GetAllRoom()
        {
            try
            {
                var listRoom = await _context.Rooms
                    .ToListAsync();
                return listRoom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Room> GetRoomById(int roomId)
        {
            try
            {
                var room = await _context.Rooms
                    .FirstOrDefaultAsync(r => r.RoomId == (roomId));
                return room;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Room> GetRoomByName(string name)
        {
            try
            {
                var room = await _context.Rooms
                    .FirstOrDefaultAsync(r => r.RoomName.Equals(name));
                return room;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateRoom(Room room)
        {
            try
            {
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
