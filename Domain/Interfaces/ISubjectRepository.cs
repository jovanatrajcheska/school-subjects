using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<Subject> GetSubjectWithDetailsAsync(Guid id);
        Task<IEnumerable<Subject>> GetSubjectsByDepartmentAsync(Guid departmentId);
        Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(Guid teacherId);
        Task<IEnumerable<Subject>> GetMandatorySubjectsAsync();
        Task<IEnumerable<Subject>> GetSubjectsBySemesterAsync(Semester semester);
        Task<IEnumerable<Subject>> GetSubjectsByEvaluationMethodAsync(EvaluationMethod evaluationMethod);

    }
}
