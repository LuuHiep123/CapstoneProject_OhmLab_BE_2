using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface ISemesterSubjectRepository
    {
        Task<List<SemesterSubject>> GetBySubjectIdAsync(int subjectId);
        Task<SemesterSubject> UpdateSemesterSubjectAsync(SemesterSubject semesterSubject);
        Task<List<SemesterSubject>> GetBySemesterIdAsync(int semesterId);
        Task<SemesterSubject> GetBySemesterIdAngSubjectIdAsync(int subjectId, int semeterid);
        Task<SemesterSubject> GetByIdAsync(int sesmerterSubjectId);
        Task<IEnumerable<SemesterSubject>> GetAllAsync();
        Task<SemesterSubject> AddAsync(SemesterSubject semesterSubject);
    }
}
