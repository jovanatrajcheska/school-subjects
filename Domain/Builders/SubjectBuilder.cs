using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Builders
{
    public class SubjectBuilder
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private int _weeklyClasses;
        private bool _isMandatory;
        private Semester _semester;
        private EvaluationMethod _evaluationMethod;
        private Guid _departmentId;
        private string _departmentName;
        private readonly List<Teacher> _teachers = new();
        private readonly List<Literature> _literature = new();
        private readonly List<GradingCriteria> _gradingCriteria = new();
        private readonly List<Subject> _prerequisites = new();

        public SubjectBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SubjectBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public SubjectBuilder WithWeeklyClasses(int weeklyClasses)
        {
            _weeklyClasses = weeklyClasses;
            return this;
        }

        public SubjectBuilder WithIsMandatory(bool isMandatory)
        {
            _isMandatory = isMandatory;
            return this;
        }

        public SubjectBuilder WithSemester(Semester semester)
        {
            _semester = semester;
            return this;
        }

        public SubjectBuilder WithEvaluationMethod(EvaluationMethod evaluationMethod)
        {
            _evaluationMethod = evaluationMethod;
            return this;
        }

        public SubjectBuilder WithDepartmentId(Guid departmentId)
        {
            _departmentId = departmentId;
            return this;
        }

        public SubjectBuilder WithDepartmentName(String departmentName)
        {
            _departmentName = departmentName;
            return this;
        }

        public SubjectBuilder AddTeacher(Teacher teacher)
        {
            _teachers.Add(teacher);
            return this;
        }

        public SubjectBuilder AddTeacher(Action<TeacherBuilder> configure)
        {
            var builder = new TeacherBuilder();
            configure(builder);
            _teachers.Add(builder.Build());
            return this;
        }

        public SubjectBuilder AddLiterature(Literature literature)
        {
            _literature.Add(literature);
            return this;
        }

        public SubjectBuilder AddLiterature(Action<LiteratureBuilder> configure)
        {
            var builder = new LiteratureBuilder();
            configure(builder);
            _literature.Add(builder.Build());
            return this;
        }

        public SubjectBuilder AddGradingCriteria(GradingCriteria criteria)
        {
            _gradingCriteria.Add(criteria);
            return this;
        }

        public SubjectBuilder AddGradingCriteria(Action<GradingCriteriaBuilder> configure)
        {
            var builder = new GradingCriteriaBuilder();
            configure(builder);
            _gradingCriteria.Add(builder.Build());
            return this;
        }

        public SubjectBuilder AddPrerequisite(Subject prerequisite)
        {
            _prerequisites.Add(prerequisite);
            return this;
        }

        public SubjectBuilder AddPrerequisite(Action<SubjectBuilder> configure)
        {
            var builder = new SubjectBuilder();
            configure(builder);
            _prerequisites.Add(builder.Build());
            return this;
        }

        public Subject Build()
        {
            var subject = new Subject(
                _name,
                _description,
                _weeklyClasses,
                _isMandatory,
                _semester,
                _evaluationMethod,
                _departmentId
            );

            foreach (var teacher in _teachers)
                subject.AddTeacher(teacher);

            foreach (var lit in _literature)
                subject.AddLiterature(lit);

            foreach (var criteria in _gradingCriteria)
                subject.AddGradingCriteria(criteria);

            foreach (var prereq in _prerequisites)
                subject.AddPrerequisite(prereq);

            return subject;
        }
    }
}
