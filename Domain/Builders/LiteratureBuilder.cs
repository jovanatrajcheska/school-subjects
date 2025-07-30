using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Builders
{
    public class LiteratureBuilder
    {
        private string _title = string.Empty;
        private string _isbn = string.Empty;
        private readonly List<Author> _authors = new();

        public LiteratureBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public LiteratureBuilder WithIsbn(string isbn)
        {
            _isbn = isbn;
            return this;
        }

        public LiteratureBuilder AddAuthor(Author author)
        {
            _authors.Add(author);
            return this;
        }

        public LiteratureBuilder AddAuthor(Action<AuthorBuilder> configure)
        {
            var builder = new AuthorBuilder();
            configure(builder);
            _authors.Add(builder.Build());
            return this;
        }

        public Literature Build()
        {
            var literature = new Literature(_title, _isbn);
            foreach (var author in _authors)
                literature.AddAuthor(author);
            return literature;
        }
    }
}
