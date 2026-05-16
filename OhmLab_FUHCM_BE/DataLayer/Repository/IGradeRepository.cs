using DataLayer.Entities;

namespace DataLayer.Repository
{
    public interface IGradeRepository
    {
        Task<List<Grade>> GetAllAsync();
        Task<Grade> GetByIdAsync(int id);
        Task<List<Grade>> GetByUserIdAsync(Guid userId);
        //Task<IEnumerable<Grade>> GetByLabIdAsync(int labId);
        Task<List<Grade>> GetByTeamIdAsync(int teamId);
        Task<List<Grade>> GetByStatusAsync(string status);
        Task<Grade> CreateAsync(Grade grade);
        Task<Grade> UpdateAsync(Grade grade);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        //Task<decimal> GetAverageScoreByLabAsync(int labId);
        //Task<decimal> GetAverageScoreByUserAsync(Guid userId);
        //Task<List<Grade>> GetGradesByStudentId(Guid studentId);
        //Task<List<Grade>> GetGradesByLabAndTeam(int labId, int teamId);
    }
}