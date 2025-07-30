using Microsoft.Extensions.Logging;
using Domain.Interfaces;
using Domain.Entities;
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
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SubjectService> _logger;

        public SubjectService(IUnitOfWork unitOfWork, ILogger<SubjectService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all subjects");
                var subjects = await _unitOfWork.Subjects.GetAllAsync();
                return subjects.Select(s => s.ToSubjectDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all subjects");
                throw;
            }
        }

        public async Task<SubjectDto> GetSubjectByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Retrieving subject with ID: {SubjectId}", id);
                var subject = await _unitOfWork.Subjects.GetSubjectWithDetailsAsync(id);

                if (subject == null)
                {
                    _logger.LogWarning("Subject with ID {SubjectId} not found", id);
                    return null;
                }

                return subject.ToSubjectDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subject with ID: {SubjectId}", id);
                throw;
            }
        }

        public async Task<SubjectDto> CreateSubjectAsync(SubjectDto subjectDto)
        {
            try
            {
                _logger.LogInformation("Creating new subject: {SubjectName}", subjectDto.Name);

                var department = await _unitOfWork.Departments.GetByIdAsync(subjectDto.Department.Id);
                if (department == null)
                {
                    throw new ArgumentException($"Department with ID {subjectDto.Department.Id} not found");
                }

                var subject = subjectDto.ToEntity();
                var createdSubject = await _unitOfWork.Subjects.AddAsync(subject);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Subject created successfully with ID: {SubjectId}", createdSubject.Id);
                return createdSubject.ToSubjectDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subject: {SubjectName}", subjectDto.Name);
                throw;
            }
        }

        public async Task<SubjectDto> UpdateSubjectAsync(Guid id, SubjectDto subjectDto)
        {
            try
            {
                _logger.LogInformation("Updating subject with ID: {SubjectId}", id);

                var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
                if (subject == null)
                {
                    _logger.LogWarning("Subject with ID {SubjectId} not found for update", id);
                    return null;
                }

                subject.SetName(subjectDto.Name);
                subject.SetDescription(subjectDto.Description);
                subject.SetWeeklyClasses(subjectDto.WeeklyClasses);
                subject.SetIsMandatory(subjectDto.IsMandatory);
                subject.SetSemester(subjectDto.Semester);
                subject.SetEvaluationMethod(subjectDto.EvaluationMethod);

                await _unitOfWork.Subjects.UpdateAsync(subject);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Subject updated successfully: {SubjectId}", id);
                return subject.ToSubjectDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating subject with ID: {SubjectId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteSubjectAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting subject with ID: {SubjectId}", id);

                var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
                if (subject == null)
                {
                    _logger.LogWarning("Subject with ID {SubjectId} not found for deletion", id);
                    return false;
                }

                await _unitOfWork.Subjects.DeleteAsync(subject);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Subject deleted successfully: {SubjectId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting subject with ID: {SubjectId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<SubjectDto>> GetSubjectsByDepartmentAsync(Guid departmentId)
        {
            try
            {
                _logger.LogInformation("Retrieving subjects for department: {DepartmentId}", departmentId);
                var subjects = await _unitOfWork.Subjects.GetSubjectsByDepartmentAsync(departmentId);
                return subjects.Select(s => s.ToSubjectDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subjects for department: {DepartmentId}", departmentId);
                throw;
            }
        }

        public async Task<IEnumerable<SubjectDto>> GetMandatorySubjectsAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving mandatory subjects");
                var subjects = await _unitOfWork.Subjects.GetMandatorySubjectsAsync();
                return subjects.Select(s => s.ToSubjectDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving mandatory subjects");
                throw;
            }
        }

        public async Task<IEnumerable<SubjectDto>> GetSubjectsBySemesterAsync(Semester semester)
        {
            try
            {
                _logger.LogInformation("Retrieving subjects for semester: {Semester}", semester);
                var subjects = await _unitOfWork.Subjects.GetSubjectsBySemesterAsync(semester);
                return subjects.Select(s => s.ToSubjectDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subjects for semester: {Semester}", semester);
                throw;
            }
        }

        public async Task<IEnumerable<SubjectDto>> GetSubjectsByEvaluationMethodAsync(EvaluationMethod evaluationMethod)
        {
            try
            {
                _logger.LogInformation("Retrieving subjects for evaluation method: {EvaluationMethod}", evaluationMethod);
                var subjects = await _unitOfWork.Subjects.GetSubjectsByEvaluationMethodAsync(evaluationMethod);
                return subjects.Select(s => s.ToSubjectDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subjects for evaluation method: {EvaluationMethod}", evaluationMethod);
                throw;
            }
        }
    }
}
