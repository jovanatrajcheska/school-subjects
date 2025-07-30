using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Entities
{
    public class Subject : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; private set; }

        [MaxLength(500)]
        public string Description { get; private set; }

        [Range(1, 40)]
        public int WeeklyClasses { get; private set; }

        public bool IsMandatory { get; private set; }

        public Semester Semester { get; private set; }

        public EvaluationMethod EvaluationMethod { get; private set; }

        public Guid DepartmentId { get; private set; }
        public Department Department { get; private set; }

        private readonly List<SubjectTeacher> _subjectTeachers = new();
        public IReadOnlyCollection<SubjectTeacher> SubjectTeachers => _subjectTeachers.AsReadOnly();

        private readonly List<SubjectLiterature> _subjectLiterature = new List<SubjectLiterature>();
        public IReadOnlyCollection<SubjectLiterature> SubjectLiterature => _subjectLiterature.AsReadOnly();

        private readonly List<SubjectPrerequisite> _prerequisites = new();
        public IReadOnlyCollection<SubjectPrerequisite> Prerequisites => _prerequisites.AsReadOnly();

        private readonly List<GradingCriteria> _gradingCriteria = new();
        public IReadOnlyCollection<GradingCriteria> GradingCriteria => _gradingCriteria.AsReadOnly();

        private Subject() { } 

        public Subject(string name, string description, int weeklyClasses, bool isMandatory,
                      Semester semester, EvaluationMethod evaluationMethod, Guid departmentId)
        {
            SetName(name);
            SetDescription(description);
            SetWeeklyClasses(weeklyClasses);
            SetIsMandatory(isMandatory);
            SetSemester(semester);
            SetEvaluationMethod(evaluationMethod);
            DepartmentId = departmentId;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Subject name cannot be empty.");
            Name = name;
            UpdateTimestamp();
        }

        public void SetDescription(string description)
        {
            Description = description ?? string.Empty;
            UpdateTimestamp();
        }

        public void SetWeeklyClasses(int weeklyClasses)
        {
            if (weeklyClasses < 1 || weeklyClasses > 40)
                throw new ArgumentException("Weekly classes must be between 1 and 40.");
            WeeklyClasses = weeklyClasses;
            UpdateTimestamp();
        }

        public void SetIsMandatory(bool isMandatory)
        {
            IsMandatory = isMandatory;
            UpdateTimestamp();
        }

        public void SetSemester(Semester semester)
        {
            Semester = semester;
            UpdateTimestamp();
        }

        public void SetEvaluationMethod(EvaluationMethod evaluationMethod)
        {
            EvaluationMethod = evaluationMethod;
            UpdateTimestamp();
        }

        public void AddTeacher(Teacher teacher)
        {
            if (_subjectTeachers.Any(st => st.TeacherId == teacher.Id))
                return;

            _subjectTeachers.Add(new SubjectTeacher(Id, teacher.Id));
            UpdateTimestamp();
        }

        public void AddLiterature(Literature literature)
        {
            if (_subjectLiterature.Any(sl => sl.LiteratureId == literature.Id))
                return;

            _subjectLiterature.Add(new SubjectLiterature(Id, literature.Id));
            UpdateTimestamp();
        }

        public void AddPrerequisite(Subject prerequisite)
        {
            if (_prerequisites.Any(p => p.PrerequisiteSubjectId == prerequisite.Id))
                return;

            _prerequisites.Add(new SubjectPrerequisite(Id, prerequisite.Id));
            UpdateTimestamp();
        }

        public void AddGradingCriteria(GradingCriteria criteria)
        {
            _gradingCriteria.Add(criteria);
            UpdateTimestamp();
        }

        public void SetDepartment(Department department)
        {
            Department = department;
        }
    }
}
