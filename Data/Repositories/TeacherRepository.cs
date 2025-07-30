using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(SchoolDbContext context) : base(context) { }

        public async Task<IEnumerable<Teacher>> GetTeachersBySubjectAsync(Guid subjectId)
        {
            return await _dbSet
                .Where(t => t.SubjectTeachers.Any(st => st.SubjectId == subjectId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetTeachersByTitleAsync(AcademicTitle title)
        {
            return await _dbSet
                .Where(t => t.Title == title)
                .ToListAsync();
        }

        public async Task<Teacher> GetDepartmentHeadTeacherAsync(Guid departmentId)
        {
            return await _dbSet
                .Include(t => t.HeadOfDepartment)
                .FirstOrDefaultAsync(t => t.HeadOfDepartment.Id == departmentId);
        }
    }
}
