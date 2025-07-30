using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class GradingCriteria : BaseEntity
    {
        [Range(0, 100)]
        public int MinScore { get; private set; }

        [Range(0, 100)]
        public int MaxScore { get; private set; }

        [Required]
        [MaxLength(10)]
        public string GradeLabel { get; private set; }

        public Guid SubjectId { get; private set; }
        public Subject Subject { get; private set; }

        private GradingCriteria() { } 

        public GradingCriteria(int minScore, int maxScore, string gradeLabel, Guid subjectId)
        {
            SetScoreRange(minScore, maxScore);
            SetGradeLabel(gradeLabel);
            SubjectId = subjectId;
        }

        public void SetScoreRange(int minScore, int maxScore)
        {
            if (minScore < 0 || maxScore > 100 || minScore > maxScore)
                throw new ArgumentException("Invalid score range.");
            MinScore = minScore;
            MaxScore = maxScore;
            UpdateTimestamp();
        }

        public void SetGradeLabel(string gradeLabel)
        {
            if (string.IsNullOrWhiteSpace(gradeLabel))
                throw new ArgumentException("Grade label cannot be empty.");
            GradeLabel = gradeLabel;
            UpdateTimestamp();
        }
    }
}
