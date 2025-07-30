using System;

namespace Domain.Entities
{
    public class SubjectLiterature
    {
        public Guid SubjectId { get; private set; }
        public Subject Subject { get; private set; }

        public Guid LiteratureId { get; private set; }
        public Literature Literature { get; private set; }

        private SubjectLiterature() { }
        public void SetLiterature(Literature literature)
        {
            Literature = literature;
        }
        public SubjectLiterature(Guid subjectId, Guid literatureId)
        {
            SubjectId = subjectId;
            LiteratureId = literatureId;
        }
    }
}
