using Domain.Entities;
using Presentation.DTOs;
using Services.DTOs;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Services.Mappers
{
    public static class SubjectMappingExtensions
    {
        public static SubjectDto ToSubjectDto(this Subject subject)
        {
            if (subject == null) return null;

            return new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description,
                WeeklyClasses = subject.WeeklyClasses,
                IsMandatory = subject.IsMandatory,
                Semester = subject.Semester,
                EvaluationMethod = subject.EvaluationMethod,
                Department = subject.Department?.ToDepartmentDto(),
                Teachers = subject.SubjectTeachers?.Select(st => st.Teacher.ToTeacherDto()).ToList() ?? new List<TeacherDto>(),
                Literature = subject.SubjectLiterature?.Select(sl => sl.Literature.ToLiteratureDto()).ToList() ?? new List<LiteratureDto>(),
                Prerequisites = subject.Prerequisites?.Select(p => p.PrerequisiteSubject.ToBasicSubjectDto()).ToList() ?? new List<SubjectDto>(),
                GradingCriteria = subject.GradingCriteria?.Select(gc => gc.ToGradingCriteriaDto()).ToList() ?? new List<GradingCriteriaDto>()
            };
        }

        public static SubjectDto ToBasicSubjectDto(this Subject subject)
        {
            if (subject == null) return null;

            return new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description,
                WeeklyClasses = subject.WeeklyClasses,
                IsMandatory = subject.IsMandatory,
                Semester = subject.Semester,
                EvaluationMethod = subject.EvaluationMethod,
                Department = subject.Department?.ToDepartmentDto()
            };
        }

        public static Subject ToEntity(this SubjectDto dto)
        {
            return new Subject(
                dto.Name,
                dto.Description,
                dto.WeeklyClasses,
                dto.IsMandatory,
                dto.Semester,
                dto.EvaluationMethod,
                dto.Department.Id
            );
        }

        public static List<SubjectDto> MapPredefinedSubjects(this IEnumerable<Subject> subjects)
        {
            return subjects.Select(s => s.ToSubjectDto()).ToList();
        }

        public static List<SubjectDto> MapDatabaseSubjects(this IEnumerable<Subject> subjects)
        {
            return subjects.Select(s => s.ToSubjectDto()).ToList();
        }
    }

    public static class DepartmentMappingExtensions
    {
        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            if (department == null) return null;

            return new DepartmentDto
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName,
                HeadTeacher = department.HeadTeacher?.ToTeacherDto()
            };
        }

        public static Department ToEntity(this DepartmentDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var headTeacherId = dto.HeadTeacher?.Id ?? Guid.Empty;
            return new Department(dto.Id, dto.DepartmentName, headTeacherId);
        }
    }


    public static class TeacherMappingExtensions
    {
        public static TeacherDto ToTeacherDto(this Teacher teacher)
        {
            if (teacher == null) return null;

            return new TeacherDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Title = teacher.Title,
                Email = teacher.Email
            };
        }

        public static Teacher ToEntity(this TeacherDto dto)
        {
            return new Teacher(dto.FirstName, dto.LastName, dto.Title, dto.Email);
        }
    }

    public static class LiteratureMappingExtensions
    {
        public static LiteratureDto ToLiteratureDto(this Literature literature)
        {
            if (literature == null)
            {
                return new LiteratureDto
                {
                    Id = Guid.Empty,
                    Title = "Unknown",
                    Isbn = string.Empty,
                    Authors = new List<AuthorDto>()
                };
            }

            return new LiteratureDto
            {
                Id = literature.Id,
                Title = literature.Title ?? string.Empty,
                Isbn = literature.Isbn ?? string.Empty,
                Authors = literature.LiteratureAuthors?.Select(la => la.Author.ToAuthorDto()).ToList() ?? new List<AuthorDto>()
            };
        }

        public static Literature ToEntity(this LiteratureDto dto)
        {
            return new Literature(dto.Title, dto.Isbn);
        }
    }

    public static class AuthorMappingExtensions
    {
        public static AuthorDto ToAuthorDto(this Author author)
        {
            if (author == null) return null;

            return new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }

        public static Author ToEntity(this AuthorDto dto)
        {
            return new Author(dto.FirstName, dto.LastName);
        }
    }

    public static class GradingCriteriaMappingExtensions
    {
        public static GradingCriteriaDto ToGradingCriteriaDto(this GradingCriteria criteria)
        {
            if (criteria == null) return null;

            return new GradingCriteriaDto
            {
                Id = criteria.Id,
                MinScore = criteria.MinScore,
                MaxScore = criteria.MaxScore,
                GradeLabel = criteria.GradeLabel
            };
        }

        public static GradingCriteria ToEntity(this GradingCriteriaDto dto, Guid subjectId)
        {
            return new GradingCriteria(dto.MinScore, dto.MaxScore, dto.GradeLabel, subjectId);
        }
    }
}
