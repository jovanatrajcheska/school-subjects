using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GradingCriteriaRepository : Repository<GradingCriteria>, IGradingCriteriaRepository
    {
        public GradingCriteriaRepository(SchoolDbContext context) : base(context) { }

        public async Task<IEnumerable<GradingCriteria>> GetGradingCriteriaBySubjectAsync(Guid subjectId)
        {
            return await _dbSet
                .Where(gc => gc.SubjectId == subjectId)
                .OrderBy(gc => gc.MinScore)
                .ToListAsync();
        }

        public async Task<GradingCriteria> GetGradingCriteriaByScoreAsync(Guid subjectId, int score)
        {
            return await _dbSet
                .FirstOrDefaultAsync(gc => gc.SubjectId == subjectId &&
                                          score >= gc.MinScore &&
                                          score <= gc.MaxScore);
        }
    }
}
