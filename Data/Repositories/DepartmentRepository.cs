using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(SchoolDbContext context) : base(context) { }

        public async Task<Department> GetDepartmentWithSubjectsAsync(Guid id)
        {
            return await _dbSet
                .Include(d => d.HeadTeacher)
                .Include(d => d.Subjects)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Department> GetDepartmentByHeadTeacherAsync(Guid headTeacherId)
        {
            return await _dbSet
                .Include(d => d.HeadTeacher)
                .Include(d => d.Subjects)
                .FirstOrDefaultAsync(d => d.HeadTeacherId == headTeacherId);
        }

        public override async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _dbSet
                .Include(d => d.HeadTeacher)
                .ToListAsync();
        }
    }
}
