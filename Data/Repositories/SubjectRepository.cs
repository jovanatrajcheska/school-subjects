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
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public SubjectRepository(SchoolDbContext context) : base(context) { }

        public async Task<Subject> GetSubjectWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(s => s.Department)
                    .ThenInclude(d => d.HeadTeacher)
                .Include(s => s.SubjectTeachers)
                    .ThenInclude(st => st.Teacher)
                .Include(s => s.SubjectLiterature)
                    .ThenInclude(sl => sl.Literature)
                        .ThenInclude(l => l.LiteratureAuthors)
                            .ThenInclude(la => la.Author)
                .Include(s => s.Prerequisites)
                    .ThenInclude(p => p.PrerequisiteSubject)
                .Include(s => s.GradingCriteria)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByDepartmentAsync(Guid departmentId)
        {
            return await _dbSet
                .Include(s => s.Department)
                .Where(s => s.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(Guid teacherId)
        {
            return await _dbSet
                .Include(s => s.Department)
                .Where(s => s.SubjectTeachers.Any(st => st.TeacherId == teacherId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetMandatorySubjectsAsync()
        {
            return await _dbSet
                .Include(s => s.Department)
                .Where(s => s.IsMandatory)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsBySemesterAsync(Semester semester)
        {
            return await _dbSet
                .Include(s => s.Department)
                .Where(s => s.Semester == semester)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByEvaluationMethodAsync(EvaluationMethod evaluationMethod)
        {
            return await _dbSet
                .Include(s => s.Department)
                .Where(s => s.EvaluationMethod == evaluationMethod)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _dbSet
                .Include(s => s.Department)
                    .ThenInclude(d => d.HeadTeacher)
                .Include(s => s.SubjectTeachers)
                    .ThenInclude(st => st.Teacher)
                .Include(s => s.SubjectLiterature)
                    .ThenInclude(sl => sl.Literature)
                        .ThenInclude(l => l.LiteratureAuthors)
                            .ThenInclude(la => la.Author)
                .Include(s => s.Prerequisites)
                    .ThenInclude(p => p.PrerequisiteSubject)
                .Include(s => s.GradingCriteria)
                .ToListAsync();
        }
    }
}
