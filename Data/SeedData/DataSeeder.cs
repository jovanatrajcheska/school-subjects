using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;
using Data.Context;

namespace Data.SeedData
{
    public class DataSeeder
    {
        private readonly SchoolDbContext _context;

        public DataSeeder(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException("Seed data file not found.");

            var json = await File.ReadAllTextAsync(jsonFilePath);
            var seedData = JsonSerializer.Deserialize<SeedData>(json);

            foreach (var dept in seedData.Departments)
            {
                if (!await _context.Departments.AnyAsync(d => d.Id == dept.Id))
                {
                    _context.Departments.Add(new Department(dept.Id, dept.Name, dept.HeadTeacherId));
                }
            }

            foreach (var teacher in seedData.Teachers)
            {
                if (!await _context.Teachers.AnyAsync(t => t.Id == teacher.Id))
                {
                    _context.Teachers.Add(new Teacher(teacher.FirstName, teacher.LastName, Enum.Parse<AcademicTitle>(teacher.Title), teacher.Title));
                }
            }

            foreach (var author in seedData.Authors)
            {
                if (!await _context.Authors.AnyAsync(a => a.Id == author.Id))
                {
                    _context.Authors.Add(new Author(author.FirstName, author.LastName));
                }
            }

            foreach (var lit in seedData.Literature)
            {
                if (!await _context.Literature.AnyAsync(l => l.Id == lit.Id))
                {
                    _context.Literature.Add(new Literature(lit.Title, lit.Isbn));
                }
            }

            foreach (var subject in seedData.Subjects)
            {
                if (!await _context.Subjects.AnyAsync(s => s.Id == subject.Id))
                {
                    var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentName == subject.DepartmentName);
                    if (department != null)
                    {
                        _context.Subjects.Add(new Subject(subject.Name, subject.Description, subject.WeeklyClasses, subject.IsMandatory, Enum.Parse<Semester>(subject.Semester), Enum.Parse<EvaluationMethod>(subject.EvaluationMethod), department.Id));
                    }
                }
            }

            if (seedData.SubjectTeachers != null)
            {
                foreach (var st in seedData.SubjectTeachers)
                {
                    if (!await _context.SubjectTeachers.AnyAsync(x => x.SubjectId == st.SubjectId && x.TeacherId == st.TeacherId))
                    {
                        _context.SubjectTeachers.Add(new SubjectTeacher(st.SubjectId, st.TeacherId));
                    }
                }
            }

            if (seedData.SubjectLiterature != null)
            {
                foreach (var sl in seedData.SubjectLiterature)
                {
                    if (!await _context.SubjectLiterature.AnyAsync(x => x.SubjectId == sl.SubjectId && x.LiteratureId == sl.LiteratureId))
                    {
                        _context.SubjectLiterature.Add(new SubjectLiterature(sl.SubjectId,sl.LiteratureId));
                    }
                }
            }

            if (seedData.LiteratureAuthors != null)
            {
                foreach (var la in seedData.LiteratureAuthors)
                {
                    if (!await _context.LiteratureAuthors.AnyAsync(x => x.LiteratureId == la.LiteratureId && x.AuthorId == la.AuthorId))
                    {
                        _context.LiteratureAuthors.Add(new LiteratureAuthor(la.LiteratureId, la.AuthorId));
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
