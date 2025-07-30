using System;
using System.Collections.Generic;

namespace Data.SeedData
{
    public class SeedData
    {
        public List<SubjectSeed> Subjects { get; set; } = new();
        public List<DepartmentSeed> Departments { get; set; } = new();
        public List<TeacherSeed> Teachers { get; set; } = new();
        public List<LiteratureSeed> Literature { get; set; } = new();
        public List<AuthorSeed> Authors { get; set; } = new();
        public List<SubjectTeacherSeed> SubjectTeachers { get; set; } = new();
        public List<SubjectLiteratureSeed> SubjectLiterature { get; set; } = new();
        public List<LiteratureAuthorSeed> LiteratureAuthors { get; set; } = new();
    }

    public class SubjectSeed
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int WeeklyClasses { get; set; }
        public bool IsMandatory { get; set; }
        public string Semester { get; set; }
        public string EvaluationMethod { get; set; }
        public string DepartmentName { get; set; }
    }

    public class DepartmentSeed
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid HeadTeacherId { get; set; }
    }

    public class TeacherSeed
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
    }

    public class LiteratureSeed
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
    }

    public class AuthorSeed
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class SubjectTeacherSeed
    {
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
    }

    public class SubjectLiteratureSeed
    {
        public Guid SubjectId { get; set; }
        public Guid LiteratureId { get; set; }
    }

    public class LiteratureAuthorSeed
    {
        public Guid LiteratureId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
