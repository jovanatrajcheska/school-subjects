using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Builders;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;


namespace Domain.Helpers
{
    public static class PredefinedListOfSubjects
    {
        public static List<Author> GetPredefinedAuthors()
        {
            return new List<Author>
            {
                new AuthorBuilder().WithFirstName("Elly").WithLastName("Schottman").Build(),
                new AuthorBuilder().WithFirstName("Caroline").WithLastName("Linse").Build(),
                new AuthorBuilder().WithFirstName("Cherri").WithLastName("Moseley").Build(),
                new AuthorBuilder().WithFirstName("Janet").WithLastName("Rees").Build(),
                new AuthorBuilder().WithFirstName("Pamela").WithLastName("Sachant").Build(),
                new AuthorBuilder().WithFirstName("Richard").WithLastName("Fosbery").Build(),
                new AuthorBuilder().WithFirstName("Dennis").WithLastName("Taylor").Build(),
            };
        }

        public static List<Literature> GetPredefinedLiterature(List<Author> authors)
        {
            return new List<Literature>
            {
                new LiteratureBuilder()
                    .WithTitle("Cambridge Global English Learner's Book")
                    .WithIsbn("9781108963619")
                    .AddAuthor(authors[0])
                    .AddAuthor(authors[1])
                    .Build(),
                new LiteratureBuilder()
                    .WithTitle("Cambridge Primary Mathematics Learner's Book 3")
                    .WithIsbn("9781108746489")
                    .AddAuthor(authors[2])
                    .AddAuthor (authors[3])
                    .Build(),
                new LiteratureBuilder()
                    .WithTitle("Introduction to Art: Design, Context, and Meaning")
                    .WithIsbn("9780714847054")
                    .AddAuthor(authors[4])
                    .Build(),
                new LiteratureBuilder()
                    .WithTitle("Biology Coursebook")
                    .WithIsbn("9781108859028")
                    .AddAuthor(authors[5])
                    .AddAuthor(authors[6])
                    .Build()
            };
        }

        public static List<Department> GetPredefinedDepartments(Guid headTeacherId)
        {
            return new List<Department>
            {
                new DepartmentBuilder()
                    .WithId(Guid.NewGuid())
                    .WithName("Science")
                    .WithHeadTeacherId(headTeacherId)
                    .Build(),
                new DepartmentBuilder()
                    .WithName("Art")
                    .WithHeadTeacherId(headTeacherId)
                    .Build(),
                new DepartmentBuilder()
                    .WithId(Guid.NewGuid())
                    .WithName("Math")
                    .WithHeadTeacherId(headTeacherId)
                    .Build(),
                new DepartmentBuilder()
                    .WithId(Guid.NewGuid())
                    .WithName("Literature")
                    .WithHeadTeacherId(headTeacherId)
                    .Build()
            };
        }
        public static List<GradingCriteria> GetGradingCriteria(Guid subjectId, int maxPoints)
        {
            var builder = new GradingCriteriaBuilder();

            if (maxPoints == 100)
            {
                return new List<GradingCriteria>
                {
                    builder.WithMinScore(90).WithMaxScore(100).WithGradeLabel("A").WithSubjectId(subjectId).Build(),
                    builder.WithMinScore(80).WithMaxScore(89).WithGradeLabel("B").WithSubjectId(subjectId).Build(),
                    builder.WithMinScore(70).WithMaxScore(79).WithGradeLabel("C").WithSubjectId(subjectId).Build(),
                    builder.WithMinScore(60).WithMaxScore(69).WithGradeLabel("D").WithSubjectId(subjectId).Build(),
                    builder.WithMinScore(0).WithMaxScore(59).WithGradeLabel("F").WithSubjectId(subjectId).Build()
                };
            }
            else if (maxPoints == 50)
            {
                return new List<GradingCriteria>
                {
                    builder.WithMinScore(45).WithMaxScore(50).WithGradeLabel("A").WithSubjectId(subjectId).Build(),
                    builder.WithMinScore(40).WithMaxScore(44).WithGradeLabel("B").WithSubjectId(subjectId).Build(),
                    builder.WithMinScore(35).WithMaxScore(39).WithGradeLabel("C").WithSubjectId(subjectId).Build(),
                    builder.WithMinScore(30).WithMaxScore(34).WithGradeLabel("D").WithSubjectId(subjectId).Build(),
                    builder.WithMinScore(0).WithMaxScore(29).WithGradeLabel("F").WithSubjectId(subjectId).Build()
                };
            }
            else
            {
                throw new ArgumentException("Unsupported max points value.");
            }
        }

        public static List<Teacher> GetPredefinedTeachers()
        {
            return new List<Teacher>
            {
                new TeacherBuilder()
                    .WithFirstName("John")
                    .WithLastName("Doe")
                    .WithTitle(AcademicTitle.PhD)
                    .WithEmail("joedoe@test.edu")
                    .Build(),
                new TeacherBuilder()
                    .WithFirstName("Mateo")
                    .WithLastName("Carlson")
                    .WithTitle(AcademicTitle.BSc)
                    .WithEmail("mateocarlson@test.edu")
                    .Build(),
                new TeacherBuilder()
                    .WithFirstName("Michael")
                    .WithLastName("Schultz")
                    .WithTitle(AcademicTitle.MSc)
                    .WithEmail("michaelschultz@test.edu")
                    .Build()
            };
        }

        public static List<Subject> GetPredefinedSubjects()
        {
            var authors = GetPredefinedAuthors();
            var teachers = GetPredefinedTeachers();
            var departments = GetPredefinedDepartments(teachers[0].Id);
            var literature = GetPredefinedLiterature(authors);

            var subjectMaxPoints = new Dictionary<string, int>
            {
                { "Math", 100 },
                { "English", 100 },
                { "Art", 50 },
                { "Biology", 50 }
            };

            var subjectToDepartmentMap = new Dictionary<string, string>
            {
                { "Math", "Math" },
                { "English", "Literature" },
                { "Art", "Art" },
                { "Biology", "Science" }
            };

            var departmentMap = departments.ToDictionary(d => d.DepartmentName, d => d);
            var subjectNames = new[] { "Math", "English", "Art", "Biology" };
            var subjectDescriptions = new[] { "Grade 9", "Grade 6", "Grade 5", "Grade 7" };
            var subjectWeeklyClasses = new[] { 5, 4, 3, 3 };
            var subjectIsMandatory = new[] { true, true, false, true };
            var subjectSemester = new[] { Semester.Fall, Semester.Spring, Semester.Spring, Semester.Fall };
            var subjectEvaluationMethod = new[] { EvaluationMethod.Exam, EvaluationMethod.Project, EvaluationMethod.Presentation, EvaluationMethod.Presentation };
            var subjectTeachers = new[] { teachers[0], teachers[1], teachers[2], teachers[1] };
            var subjectLiterature = new[] { literature[0], literature[1], literature[2], literature[3] };

            var subjects = new List<Subject>();

            for (int i = 0; i < subjectNames.Length; i++)
            {
                var subjectName = subjectNames[i];
                var departmentName = subjectToDepartmentMap[subjectName];
                var department = departments.First(d => d.DepartmentName == departmentName);

                var subjectId = Guid.NewGuid();
                var maxPoints = subjectMaxPoints[subjectName];
                var gradingCriteria = GetGradingCriteria(subjectId, maxPoints);

                var subjectBuilder = new SubjectBuilder()
                    .WithName(subjectName)
                    .WithDescription(subjectDescriptions[i])
                    .WithWeeklyClasses(subjectWeeklyClasses[i])
                    .WithIsMandatory(subjectIsMandatory[i])
                    .WithSemester(subjectSemester[i])
                    .WithEvaluationMethod(subjectEvaluationMethod[i])
                    .WithDepartmentId(department.Id)
                    .AddLiterature(subjectLiterature[i])
                    .AddTeacher(subjectTeachers[i]);

                foreach (var criteria in gradingCriteria)
                {
                    subjectBuilder.AddGradingCriteria(criteria);
                }

                subjects.Add(subjectBuilder.Build());
            }

            foreach (var subject in subjects)
            {
                var department = departmentMap[subjectToDepartmentMap[subject.Name]];
                if (subject.GetType().GetProperty("Department") != null)
                {
                    subject.GetType().GetProperty("Department")?.SetValue(subject, department);
                }
                foreach (var sl in subject.SubjectLiterature)
                {
                    var lit = literature.FirstOrDefault(l => l.Id == sl.LiteratureId);
                    if (lit != null)
                    {
                        sl.SetLiterature(lit);

                        foreach (var la in lit.LiteratureAuthors)
                        {
                            var author = authors.FirstOrDefault(a => a.Id == la.AuthorId);
                            if (author != null)
                            {
                                la.SetAuthor(author); 
                            }
                        }
                    }
                }

                foreach (var st in subject.SubjectTeachers)
                {
                    var teacher = teachers.FirstOrDefault(t => t.Id == st.TeacherId);
                    if (teacher != null)
                    {
                        st.SetTeacher(teacher); 
                    }
                }
            }

            return subjects;
        }
    }
}
