using System;
using Domain.Entities;

namespace Domain.Builders
{
    public class GradingCriteriaBuilder
    {
        private int _minScore;
        private int _maxScore;
        private string _gradeLabel = string.Empty;
        private Guid _subjectId;

        public GradingCriteriaBuilder WithMinScore(int minScore)
        {
            _minScore = minScore;
            return this;
        }

        public GradingCriteriaBuilder WithMaxScore(int maxScore)
        {
            _maxScore = maxScore;
            return this;
        }

        public GradingCriteriaBuilder WithGradeLabel(string gradeLabel)
        {
            _gradeLabel = gradeLabel;
            return this;
        }

        public GradingCriteriaBuilder WithSubjectId(Guid subjectId)
        {
            _subjectId = subjectId;
            return this;
        }

        public GradingCriteria Build()
        {
            if (_minScore < 0 || _minScore > 100)
                throw new ArgumentException("Minimum score must be between 0 and 100.");
            if (_maxScore < 0 || _maxScore > 100)
                throw new ArgumentException("Maximum score must be between 0 and 100.");
            if (_minScore > _maxScore)
                throw new ArgumentException("Minimum score must be less than or equal to MaxScore.");
            if (string.IsNullOrWhiteSpace(_gradeLabel))
                throw new ArgumentException("Grade label cannot be empty.");
            if (_gradeLabel.Length > 10)
                throw new ArgumentException("Grade label cannot be longer than 10 characters.");
            if (_subjectId == Guid.Empty)
                throw new ArgumentException("SubjectId is required.");

            return new GradingCriteria(_minScore, _maxScore, _gradeLabel, _subjectId);
        }
    }
}
