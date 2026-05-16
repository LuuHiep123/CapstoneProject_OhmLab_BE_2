using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Implement
{
    public class GradeDescriptionRepository : IGradeDescriptionRepository
    {
        private readonly DBContext.db_abadcb_ohmlabContext _context;
        public GradeDescriptionRepository(DBContext.db_abadcb_ohmlabContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateGradeDescription(GradeDescription gradeDescription)
        {
            try
            {
                await _context.GradeDescriptions.AddAsync(gradeDescription);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteGradeDescription(GradeDescription gradeDescription)
        {
            try
            {
                _context.GradeDescriptions.Remove(gradeDescription);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GradeDescription>> GetAllGradeDescription()
        {
            try
            {
                var listGradeDescription = await _context.GradeDescriptions
                    .Include(gd => gd.Class)
                    .Include(gd => gd.Lab)
                    .Include(gd => gd.Student)
                    .Include(gd => gd.Grade)
                        .ThenInclude(g => g.RegistraionSchedule)
                        .ThenInclude(g => g.Slot)
                    .ToListAsync();
                return listGradeDescription;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GradeDescription> GetGradeDescriptionById(int id)
        {
            try
            {
                var gradeDescription = await _context.GradeDescriptions
                    .Include(gd => gd.Class)
                    .Include(gd => gd.Lab)
                    .Include(gd => gd.Student)
                    .Include(gd => gd.Grade)
                        .ThenInclude(g => g.RegistraionSchedule)
                        .ThenInclude(g => g.Slot)

                    .FirstOrDefaultAsync(a => a.GradeDescriptionId.Equals(id));
                return gradeDescription;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateGradeDescription(GradeDescription gradeDescription)
        {
            try
            {
                _context.GradeDescriptions.Update(gradeDescription);
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
