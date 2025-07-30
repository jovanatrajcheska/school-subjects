using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Author : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; private set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; private set; }

        private readonly List<LiteratureAuthor> _literatureAuthors = new();
        public IReadOnlyCollection<LiteratureAuthor> LiteratureAuthors => _literatureAuthors.AsReadOnly();

        private Author() { } 

        public Author(string firstName, string lastName)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
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

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
