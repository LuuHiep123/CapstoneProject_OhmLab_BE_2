using DataLayer.DBContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Implement
{
    public class RegistrationScheduleRepository : IRegistrationScheduleRepository
    {
        private readonly DBContext.db_abadcb_ohmlabContext _context;

        public RegistrationScheduleRepository(DBContext.db_abadcb_ohmlabContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRegistrationSchedule(RegistraionSchedule registrationSchedule)
        {
            try
            {
                await _context.RegistraionSchedules.AddAsync(registrationSchedule);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteRegistrationSchedule(RegistraionSchedule registrationSchedule)
        {
            try
            {
                _context.RegistraionSchedules.Remove(registrationSchedule);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<RegistraionSchedule>> GetAllRegistrationSchedule()
        {
            try
            {
                var listRegistrationSchedule = await _context.RegistraionSchedules
                    .Include(rs => rs.Class)
                    .Include(rs => rs.Teaacher)
                    .Include(rs => rs.Room)
                    .Include(rs => rs.Lab)
                        .ThenInclude(l => l.Subject)
                    .Include(rs => rs.Room)
                    .Include(rs => rs.Slot)
                    .ToListAsync();
                return listRegistrationSchedule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<RegistraionSchedule>> GetAllRegistrationScheduleByTeacherId(Guid teacherId)
        {
            try
            {
                var listRegistrationSchedule = await _context.RegistraionSchedules
                    .Include(rs => rs.Class)
                   .Include(rs => rs.Room)
                    .Include(rs => rs.Teaacher)
                    .Include(rs => rs.Lab)
                       .ThenInclude(l => l.Subject)
                    .Include(rs => rs.Room)
                    .Include(rs => rs.Slot)
                    .Where(rs => rs.TeaacherId.Equals(teacherId))
                    .ToListAsync();
                return listRegistrationSchedule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RegistraionSchedule> GetRegistrationScheduleById(int id)
        {
            try
            {
                var listRegistrationSchedule = await _context.RegistraionSchedules
                    .Include(rs => rs.Class)
                   .Include(rs => rs.Room)
                    .Include(rs => rs.Teaacher)
                    .Include(rs => rs.Lab)
                       .ThenInclude(l => l.Subject)
                    .Include(rs => rs.Room)
                    .Include(rs => rs.Slot)
                    .FirstOrDefaultAsync(rs => rs.RegistraionScheduleId == id);
                return listRegistrationSchedule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<RegistraionSchedule>> GetByDateWithIncludesAsync(DateTime date)
        {
            try
            {
                var registrationSchedules = await _context.RegistraionSchedules
                    .Include(rs => rs.Class)
                   .Include(rs => rs.Room)
                    .Include(rs => rs.Teaacher)
                    .Include(rs => rs.Lab)
                       .ThenInclude(l => l.Subject)
                    .Include(rs => rs.Room)
                    .Include(rs => rs.Slot)
                    .Where(rs => rs.RegistraionScheduleCreateDate.Date == date.Date)
                    .ToListAsync();
                return registrationSchedules;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<RegistraionSchedule>> GetByTeacherIdAndDateWithIncludesAsync(Guid teacherId, DateTime date)
        {
            try
            {
                var registrationSchedules = await _context.RegistraionSchedules
                    .Include(rs => rs.Class)
                   .Include(rs => rs.Room)
                    .Include(rs => rs.Teaacher)
                    .Include(rs => rs.Lab)
                       .ThenInclude(l => l.Subject)
                    .Include(rs => rs.Room)
                    .Include(rs => rs.Slot)
                    .Where(rs => rs.TeaacherId == teacherId && rs.RegistraionScheduleCreateDate.Date == date.Date)
                    .ToListAsync();
                return registrationSchedules;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<RegistraionSchedule>> GetByClassIdAndDateWithIncludesAsync(int classId, DateTime date)
        {
            try
            {
                var registrationSchedules = await _context.RegistraionSchedules
                    .Include(rs => rs.Class)
                   .Include(rs => rs.Room)
                    .Include(rs => rs.Teaacher)
                    .Include(rs => rs.Lab)
                       .ThenInclude(l => l.Subject)
                    .Include(rs => rs.Room)
                    .Include(rs => rs.Slot)
                    .Where(rs => rs.ClassId == classId /*&& rs.RegistraionScheduleDate.date == date.Date*/)
                    .ToListAsync();
                return registrationSchedules;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> UpdateRegistrationSchedule(RegistraionSchedule registrationSchedule)
        {
            try
            {
                _context.RegistraionSchedules.Update(registrationSchedule);
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
