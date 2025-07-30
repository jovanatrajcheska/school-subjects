using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Teacher : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; private set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; private set; }

        [Required]
        public AcademicTitle Title { get; private set; }

        [MaxLength(100)]
        public string Email { get; private set; }

        private readonly List<SubjectTeacher> _subjectTeachers = new();
        public IReadOnlyCollection<SubjectTeacher> SubjectTeachers => _subjectTeachers.AsReadOnly();

        public Department HeadOfDepartment { get; private set; }

        private Teacher() { } 

        public Teacher(string firstName, string lastName, AcademicTitle title, string email = "")
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetTitle(title);
            SetEmail(email);
        }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.");
            FirstName = firstName;
            UpdateTimestamp();
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.");
            LastName = lastName;
            UpdateTimestamp();
        }

        public void SetTitle(AcademicTitle title)
        {
            Title = title;
            UpdateTimestamp();
        }

        public void SetEmail(string email)
        {
            Email = email ?? string.Empty;
            UpdateTimestamp();
        }

        public string GetFormattedName()
        {
            return Title == AcademicTitle.BSc
                ? $"{FirstName} {LastName}"
                : $"{Title}. {FirstName} {LastName}";
        }
    }
}