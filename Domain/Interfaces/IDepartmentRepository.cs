using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department> GetDepartmentWithSubjectsAsync(Guid id);
        Task<Department> GetDepartmentByHeadTeacherAsync(Guid headTeacherId);
    }
}
