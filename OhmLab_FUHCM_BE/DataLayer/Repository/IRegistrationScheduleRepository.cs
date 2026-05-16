using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IRegistrationScheduleRepository
    {
        Task<bool> CreateRegistrationSchedule(RegistraionSchedule registrationSchedule);
        Task<bool> UpdateRegistrationSchedule(RegistraionSchedule registrationSchedule);
        Task<bool> DeleteRegistrationSchedule(RegistraionSchedule registrationSchedule);
        Task<List<RegistraionSchedule>> GetAllRegistrationSchedule();
        Task<List<RegistraionSchedule>> GetAllRegistrationScheduleByTeacherId(Guid teacherId);
        Task<RegistraionSchedule> GetRegistrationScheduleById(int id);
        
        // Thêm các phương thức mới để hỗ trợ Report Service
        Task<List<RegistraionSchedule>> GetByDateWithIncludesAsync(DateTime date);
        Task<List<RegistraionSchedule>> GetByTeacherIdAndDateWithIncludesAsync(Guid teacherId, DateTime date);
        Task<List<RegistraionSchedule>> GetByClassIdAndDateWithIncludesAsync(int classId, DateTime date);
    }
}
