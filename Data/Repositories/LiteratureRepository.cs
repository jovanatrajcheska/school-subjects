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
    public class LiteratureRepository : Repository<Literature>, ILiteratureRepository
    {
        public LiteratureRepository(SchoolDbContext context) : base(context) { }

        public async Task<IEnumerable<Literature>> GetLiteratureBySubjectAsync(Guid subjectId)
        {
            return await _dbSet
                .Include(l => l.LiteratureAuthors)
                    .ThenInclude(la => la.Author)
                .Where(l => l.SubjectLiterature.Any(sl => sl.SubjectId == subjectId))
                .ToListAsync();
        }

        public async Task<Literature> GetLiteratureByIsbnAsync(string isbn)
        {
            return await _dbSet
                .Include(l => l.LiteratureAuthors)
                    .ThenInclude(la => la.Author)
                .FirstOrDefaultAsync(l => l.Isbn == isbn);
        }

        public async Task<IEnumerable<Literature>> GetLiteratureByAuthorAsync(Guid authorId)
        {
            return await _dbSet
                .Include(l => l.LiteratureAuthors)
                    .ThenInclude(la => la.Author)
                .Where(l => l.LiteratureAuthors.Any(la => la.AuthorId == authorId))
                .ToListAsync();
        }

        public override async Task<IEnumerable<Literature>> GetAllAsync()
        {
            return await _dbSet
                .Include(l => l.LiteratureAuthors)
                    .ThenInclude(la => la.Author)
                .ToListAsync();
        }
    }
}
