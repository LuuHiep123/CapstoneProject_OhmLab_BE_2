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
    public class SemesterSubjectRepository : ISemesterSubjectRepository
    {
        private readonly DBContext.db_abadcb_ohmlabContext _DBContext;

        public SemesterSubjectRepository(DBContext.db_abadcb_ohmlabContext OhmLab_DBContext)
        {
            _DBContext = OhmLab_DBContext;
        }

        public async Task<List<SemesterSubject>> GetBySubjectIdAsync(int subjectId)
        {
            try
            {
                return await _DBContext.SemesterSubjects
                    .Include(ss => ss.Subject)
                    .Include(ss => ss.Semester)
                    .Where(ss => ss.SubjectId == subjectId && ss.SemesterSubject1.ToLower().Equals("valid"))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR][GetBySubjectIdAsync] {ex.Message} | Inner: {ex.InnerException?.Message}");
                throw;
            }
        }

        public async Task<List<SemesterSubject>> GetBySemesterIdAsync(int semesterId)
        {
            try
            {
                return await _DBContext.SemesterSubjects
                    .Include(ss => ss.Subject)
                    .Include(ss => ss.Semester)
                    .Where(ss => ss.SemesterId == semesterId && ss.SemesterSubject1.ToLower().Equals("valid"))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR][GetBySubjectIdAsync] {ex.Message} | Inner: {ex.InnerException?.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<SemesterSubject>> GetAllAsync()
        {
            try
            {
                return await _DBContext.SemesterSubjects
                    .Include(ss => ss.Subject)
                    .Include(ss => ss.Semester)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR][GetAllAsync] {ex.Message} | Inner: {ex.InnerException?.Message}");
                throw;
            }
        }

        public async Task<SemesterSubject> AddAsync(SemesterSubject semesterSubject)
        {
            try
            {
                _DBContext.SemesterSubjects.Add(semesterSubject);
                await _DBContext.SaveChangesAsync();
                return semesterSubject;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR][AddAsync] {ex.Message} | Inner: {ex.InnerException?.Message}");
                throw;
            }
        }

        public async Task<SemesterSubject> GetBySemesterIdAngSubjectIdAsync(int subjectId, int semeterid)
        {
            try
            {
                return await _DBContext.SemesterSubjects
                    .Include(ss => ss.Subject)
                    .Include(ss => ss.Semester)
                    .FirstOrDefaultAsync(ss => ss.SubjectId == subjectId && ss.SemesterId == semeterid);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR][AddAsync] {ex.Message} | Inner: {ex.InnerException?.Message}");
                throw;
            }
        }

        public async Task<SemesterSubject> GetByIdAsync(int sesmerterSubjectId)
        {
            try
            {
                return await _DBContext.SemesterSubjects
                    .Include(ss => ss.Subject)
                    .Include(ss => ss.Semester)
                    .FirstOrDefaultAsync(ss => ss.SemesterSubjectId == sesmerterSubjectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR][AddAsync] {ex.Message} | Inner: {ex.InnerException?.Message}");
                throw;
            }
        }


        public async Task<SemesterSubject> UpdateSemesterSubjectAsync(SemesterSubject semesterSubject)
        {
            try
            {
                _DBContext.SemesterSubjects
                    .Update(semesterSubject);
                await _DBContext.SaveChangesAsync();
                return semesterSubject;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR][AddAsync] {ex.Message} | Inner: {ex.InnerException?.Message}");
                throw;
            }
        }


    }
}
