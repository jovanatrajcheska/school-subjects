using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGradingCriteriaRepository : IRepository<GradingCriteria>
    {
        Task<IEnumerable<GradingCriteria>> GetGradingCriteriaBySubjectAsync(Guid subjectId);
        Task<GradingCriteria> GetGradingCriteriaByScoreAsync(Guid subjectId, int score);
    }
}
