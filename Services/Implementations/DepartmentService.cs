using Microsoft.Extensions.Logging;
using Domain.Interfaces;
using Services.DTOs;
using Services.Interfaces;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(IUnitOfWork unitOfWork, ILogger<DepartmentService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all departments");
                var departments = await _unitOfWork.Departments.GetAllAsync();
                return departments.Select(d => d.ToDepartmentDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all departments");
                throw;
            }
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Retrieving department with ID: {DepartmentId}", id);
                var department = await _unitOfWork.Departments.GetDepartmentWithSubjectsAsync(id);

                if (department == null)
                {
                    _logger.LogWarning("Department with ID {DepartmentId} not found", id);
                    return null;
                }

                return department.ToDepartmentDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving department with ID: {DepartmentId}", id);
                throw;
            }
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            try
            {
                _logger.LogInformation("Creating new department: {DepartmentName}", departmentDto.DepartmentName);

                if (departmentDto.HeadTeacher?.Id != null && departmentDto.HeadTeacher.Id != Guid.Empty)
                {
                    var headTeacher = await _unitOfWork.Teachers.GetByIdAsync(departmentDto.HeadTeacher.Id);
                    if (headTeacher == null)
                    {
                        throw new ArgumentException($"Head teacher with ID {departmentDto.HeadTeacher.Id} not found");
                    }
                }

                var department = departmentDto.ToEntity();  
                var createdDepartment = await _unitOfWork.Departments.AddAsync(department);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Department created successfully with ID: {DepartmentId}", createdDepartment.Id);
                return createdDepartment.ToDepartmentDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating department: {DepartmentName}", departmentDto.DepartmentName);
                throw;
            }
        }


        public async Task<DepartmentDto> UpdateDepartmentAsync(Guid id, DepartmentDto departmentDto)
        {
            try
            {
                _logger.LogInformation("Updating department with ID: {DepartmentId}", id);

                var department = await _unitOfWork.Departments.GetByIdAsync(id);
                if (department == null)
                {
                    _logger.LogWarning("Department with ID {DepartmentId} not found for update", id);
                    return null;
                }

                department.SetDepartmentName(departmentDto.DepartmentName);
                if (departmentDto.HeadTeacher != null)
                {
                    department.SetHeadTeacher(departmentDto.HeadTeacher.Id);
                }

                await _unitOfWork.Departments.UpdateAsync(department);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Department updated successfully: {DepartmentId}", id);
                return department.ToDepartmentDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating department with ID: {DepartmentId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteDepartmentAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting department with ID: {DepartmentId}", id);

                var department = await _unitOfWork.Departments.GetByIdAsync(id);
                if (department == null)
                {
                    _logger.LogWarning("Department with ID {DepartmentId} not found for deletion", id);
                    return false;
                }

                await _unitOfWork.Departments.DeleteAsync(department);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Department deleted successfully: {DepartmentId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting department with ID: {DepartmentId}", id);
                throw;
            }
        }
    }
}
