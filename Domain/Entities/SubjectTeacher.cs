using System;

namespace Domain.Entities
{
    public class SubjectTeacher
    {
        public Guid SubjectId { get; private set; }
        public Subject Subject { get; private set; }

        public Guid TeacherId { get; private set; }
        public Teacher Teacher { get; private set; }

        private SubjectTeacher() { }
        public void SetTeacher(Teacher teacher)
        {
            Teacher = teacher;
        }
        public SubjectTeacher(Guid subjectId, Guid teacherId)
        {
            SubjectId = subjectId;
            TeacherId = teacherId;
        }
    }
}
