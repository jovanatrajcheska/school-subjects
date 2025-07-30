using Microsoft.Extensions.Logging;
using Domain.Interfaces;
using Domain.Enums;
using Presentation.DTOs;
using Services.Interfaces;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TeacherService> _logger;

        public TeacherService(IUnitOfWork unitOfWork, ILogger<TeacherService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all teachers");
                var teachers = await _unitOfWork.Teachers.GetAllAsync();
                return teachers.Select(t => t.ToTeacherDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all teachers");
                throw;
            }
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Retrieving teacher with ID: {TeacherId}", id);
                var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);

                if (teacher == null)
                {
                    _logger.LogWarning("Teacher with ID {TeacherId} not found", id);
                    return null;
                }

                return teacher.ToTeacherDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teacher with ID: {TeacherId}", id);
                throw;
            }
        }

        public async Task<TeacherDto> CreateTeacherAsync(TeacherDto teacherDto)
        {
            try
            {
                _logger.LogInformation("Creating new teacher: {TeacherName}", $"{teacherDto.FirstName} {teacherDto.LastName}");

                var teacher = teacherDto.ToEntity();
                var createdTeacher = await _unitOfWork.Teachers.AddAsync(teacher);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Teacher created successfully with ID: {TeacherId}", createdTeacher.Id);
                return createdTeacher.ToTeacherDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating teacher: {TeacherName}", $"{teacherDto.FirstName} {teacherDto.LastName}");
                throw;
            }
        }

        public async Task<TeacherDto> UpdateTeacherAsync(Guid id, TeacherDto teacherDto)
        {
            try
            {
                _logger.LogInformation("Updating teacher with ID: {TeacherId}", id);

                var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
                if (teacher == null)
                {
                    _logger.LogWarning("Teacher with ID {TeacherId} not found for update", id);
                    return null;
                }

                teacher.SetFirstName(teacherDto.FirstName);
                teacher.SetLastName(teacherDto.LastName);
                teacher.SetTitle(teacherDto.Title);
                teacher.SetEmail(teacherDto.Email);

                await _unitOfWork.Teachers.UpdateAsync(teacher);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Teacher updated successfully: {TeacherId}", id);
                return teacher.ToTeacherDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating teacher with ID: {TeacherId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteTeacherAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting teacher with ID: {TeacherId}", id);

                var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
                if (teacher == null)
                {
                    _logger.LogWarning("Teacher with ID {TeacherId} not found for deletion", id);
                    return false;
                }

                await _unitOfWork.Teachers.DeleteAsync(teacher);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Teacher deleted successfully: {TeacherId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting teacher with ID: {TeacherId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<TeacherDto>> GetTeachersByTitleAsync(AcademicTitle title)
        {
            try
            {
                _logger.LogInformation("Retrieving teachers with title: {Title}", title);
                var teachers = await _unitOfWork.Teachers.GetTeachersByTitleAsync(title);
                return teachers.Select(t => t.ToTeacherDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teachers with title: {Title}", title);
                throw;
            }
        }
    }
}
