using System;

namespace Domain.Entities
{
    public class SubjectPrerequisite
    {
        public Guid SubjectId { get; private set; }
        public Subject Subject { get; private set; }

        public Guid PrerequisiteSubjectId { get; private set; }
        public Subject PrerequisiteSubject { get; private set; }

        private SubjectPrerequisite() { } 

        public SubjectPrerequisite(Guid subjectId, Guid prerequisiteSubjectId)
        {
            SubjectId = subjectId;
            PrerequisiteSubjectId = prerequisiteSubjectId;
        }
    }
}
