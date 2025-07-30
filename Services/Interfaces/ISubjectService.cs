using Presentation.DTOs;
using Services.DTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync();
        Task<SubjectDto> GetSubjectByIdAsync(Guid id);
        Task<SubjectDto> CreateSubjectAsync(SubjectDto subjectDto);
        Task<SubjectDto> UpdateSubjectAsync(Guid id, SubjectDto subjectDto);
        Task<bool> DeleteSubjectAsync(Guid id);
        Task<IEnumerable<SubjectDto>> GetSubjectsByDepartmentAsync(Guid departmentId);
        Task<IEnumerable<SubjectDto>> GetMandatorySubjectsAsync();
        Task<IEnumerable<SubjectDto>> GetSubjectsBySemesterAsync(Semester semester);
        Task<IEnumerable<SubjectDto>> GetSubjectsByEvaluationMethodAsync(EvaluationMethod evaluationMethod);
    }
}
