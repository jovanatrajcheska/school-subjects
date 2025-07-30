using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Entities
{
    public class Literature : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; private set; }

        [MaxLength(20)]
        public string Isbn { get; private set; }

        private readonly List<LiteratureAuthor> _literatureAuthors = new();
        public IReadOnlyCollection<LiteratureAuthor> LiteratureAuthors => _literatureAuthors.AsReadOnly();

        private readonly List<SubjectLiterature> _subjectLiterature = new();
        public IReadOnlyCollection<SubjectLiterature> SubjectLiterature => _subjectLiterature.AsReadOnly();

        private Literature() { } 

        public Literature(string title, string isbn = "")
        {
            SetTitle(title);
            SetIsbn(isbn);
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Literature title cannot be empty.");
            Title = title;
            UpdateTimestamp();
        }

        public void SetIsbn(string isbn)
        {
            Isbn = isbn ?? string.Empty;
            UpdateTimestamp();
        }

        public void AddAuthor(Author author)
        {
            if (_literatureAuthors.Any(la => la.AuthorId == author.Id))
                return;

            _literatureAuthors.Add(new LiteratureAuthor(Id, author.Id));
            UpdateTimestamp();
        }
    }
}
