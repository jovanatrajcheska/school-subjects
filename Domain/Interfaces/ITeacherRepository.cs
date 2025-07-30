using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetTeachersBySubjectAsync(Guid subjectId);
        Task<IEnumerable<Teacher>> GetTeachersByTitleAsync(AcademicTitle title);
        Task<Teacher> GetDepartmentHeadTeacherAsync(Guid departmentId);
    }
}
