using System;

namespace Domain.Entities
{
    public class LiteratureAuthor
    {
        public Guid LiteratureId { get; private set; }
        public Literature Literature { get; private set; }

        public Guid AuthorId { get; private set; }
        public Author Author { get; private set; }

        private LiteratureAuthor() { }
        public void SetAuthor(Author author)
        {
            Author = author;
        }
        public LiteratureAuthor(Guid literatureId, Guid authorId)
        {
            LiteratureId = literatureId;
            AuthorId = authorId;
        }
    }
}
