using Microsoft.Extensions.Logging;
using Presentation.DTOs;
using Services.Interfaces;
using Services.Mappers;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Implementations;
using Domain.Enums;

namespace Presentation
{
    public class ConsoleApplication
    {
        private readonly ILogger<ConsoleApplication> _logger;
        private readonly ISubjectService _subjectService;
        private readonly IDepartmentService _departmentService;
        private readonly ITeacherService _teacherService;
        public ConsoleApplication(
                ISubjectService subjectService,
                ILogger<ConsoleApplication> logger,
                IDepartmentService departmentService,
                ITeacherService teacherService)
        {
            _subjectService = subjectService;
            _logger = logger;
            _departmentService = departmentService;
            _teacherService = teacherService;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. View Predefined Subjects");
                Console.WriteLine("2. View Database Subjects");
                Console.WriteLine("3. View All Subjects");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");


                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await ShowSubjects(true);
                        break;
                    case "2":
                        await ShowSubjects(false);
                        break;
                    case "3":
                        await ShowAllSubjects();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private async Task ShowAllSubjects()
        {
            try
            {
                var predefined = PredefinedListOfSubjects.GetPredefinedSubjects()
                    .Select(s => s.ToSubjectDto());

                var dbSubjects = (await _subjectService.GetAllSubjectsAsync()).ToList();

                var allSubjects = predefined
                    .Concat(dbSubjects)
                    .GroupBy(s => s.Name)
                    .Select(g => g.First())
                    .ToList();

                if (!allSubjects.Any())
                {
                    Console.WriteLine("No subjects available. Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Available subjects (All):");
                    for (int i = 0; i < allSubjects.Count; i++)
                        Console.WriteLine($"{i + 1}. {allSubjects[i].Name}");

                    Console.WriteLine("\n0. Back to main menu");
                    Console.Write("Enter subject number to view details: ");

                    if (!int.TryParse(Console.ReadLine(), out int choice)) continue;
                    if (choice == 0) break;
                    if (choice < 1 || choice > allSubjects.Count) continue;

                    DisplaySubjectDetails(allSubjects[choice - 1]);
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error showing all subjects");
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        private async Task ShowSubjects(bool usePredefined)
        {
            try
            {
                List<SubjectDto> subjects;

                if (usePredefined)
                {
                    var predefined = PredefinedListOfSubjects.GetPredefinedSubjects();
                    subjects = predefined.Select(s => s.ToSubjectDto()).ToList();
                }
                else
                {
                    var dbSubjects = await _subjectService.GetAllSubjectsAsync();
                    subjects = dbSubjects.ToList();
                }

                if (!subjects.Any())
                {
                    Console.WriteLine("No subjects available. Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"Available subjects ({(usePredefined ? "Predefined" : "Database")}):");

                    for (int i = 0; i < subjects.Count; i++)
                        Console.WriteLine($"{i + 1}. {subjects[i].Name}");

                    Console.WriteLine("\n0. Back to main menu");
                    Console.Write("Enter subject number to view details: ");

                    if (!int.TryParse(Console.ReadLine(), out int choice)) continue;

                    if (choice == 0) break;
                    if (choice < 1 || choice > subjects.Count) continue;

                    DisplaySubjectDetails(subjects[choice - 1]);

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error showing subjects");
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        private void DisplaySubjectDetails(SubjectDto subject)
        {
            Console.Clear();
            Console.WriteLine($"=== {subject.Name.ToUpper()} ===");
            Console.WriteLine($"ID: {subject.Id}");
            Console.WriteLine($"Description: {subject.Description}");
            Console.WriteLine($"Weekly Classes: {subject.WeeklyClasses}");
            Console.WriteLine($"Mandatory: {(subject.IsMandatory ? "Yes" : "No")}");
            Console.WriteLine($"Semester: {subject.Semester}");
            Console.WriteLine($"Evaluation Method: {subject.EvaluationMethod}");
            Console.WriteLine($"Department: {subject.Department?.DepartmentName ?? "N/A"}");

            if (subject.Department?.HeadTeacher != null)
                Console.WriteLine($"Department Head: {subject.Department.HeadTeacher.FormattedName}");

            Console.WriteLine("\nTeachers:");
            if (subject.Teachers?.Any() == true)
                foreach (var teacher in subject.Teachers.Where(t => t != null))
                    Console.WriteLine($"  - {teacher.FormattedName}{(string.IsNullOrEmpty(teacher.Email) ? "" : $"\n    Email: {teacher.Email}")}");
            else
                Console.WriteLine("  No teachers assigned");

            Console.WriteLine("\nPrerequisites:");
            if (subject.Prerequisites?.Any() == true)
                foreach (var prereq in subject.Prerequisites)
                    Console.WriteLine($"  - {prereq.Name} (Semester {prereq.Semester})");
            else
                Console.WriteLine("  No prerequisites");

            Console.WriteLine("\nGrading Criteria:");
            if (subject.GradingCriteria?.Any() == true)
                foreach (var criteria in subject.GradingCriteria)
                    Console.WriteLine($"  - {criteria.GradeLabel}: {criteria.MinScore}-{criteria.MaxScore} points");
            else
                Console.WriteLine("  No grading criteria defined");

            Console.WriteLine("\nLiterature:");
            if (subject.Literature?.Any() == true)
                foreach (var lit in subject.Literature.Where(l => l != null))
                {
                    Console.WriteLine($"  - {lit.Title}");
                    if (!string.IsNullOrEmpty(lit.Isbn))
                        Console.WriteLine($"    ISBN: {lit.Isbn}");
                    if (lit.Authors?.Any() == true)
                        Console.WriteLine($"    Authors: {string.Join(", ", lit.Authors.Select(a => $"{a.FirstName} {a.LastName}"))}");
                }
            else
                Console.WriteLine("  No literature assigned");
        }
    }
}
