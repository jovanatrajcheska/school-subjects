using Presentation.DTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto> GetTeacherByIdAsync(Guid id);
        Task<TeacherDto> CreateTeacherAsync(TeacherDto teacherDto);
        Task<TeacherDto> UpdateTeacherAsync(Guid id, TeacherDto teacherDto);
        Task<bool> DeleteTeacherAsync(Guid id);
        Task<IEnumerable<TeacherDto>> GetTeachersByTitleAsync(AcademicTitle title);
    }
}
