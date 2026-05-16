using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IGradeDescriptionRepository
    {
        Task<bool> CreateGradeDescription(GradeDescription gradeDescription);
        Task<bool> UpdateGradeDescription(GradeDescription gradeDescription);
        Task<bool> DeleteGradeDescription(GradeDescription gradeDescription);
        Task<List<GradeDescription>> GetAllGradeDescription();
        Task<GradeDescription> GetGradeDescriptionById(int id);
    }
}
