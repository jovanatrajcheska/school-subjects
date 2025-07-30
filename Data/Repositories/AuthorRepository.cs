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
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(SchoolDbContext context) : base(context) { }

        public async Task<IEnumerable<Author>> GetAuthorsByLiteratureAsync(Guid literatureId)
        {
            return await _dbSet
                .Where(a => a.LiteratureAuthors.Any(la => la.LiteratureId == literatureId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> SearchAuthorsByNameAsync(string searchTerm)
        {
            return await _dbSet
                .Where(a => a.FirstName.Contains(searchTerm) || a.LastName.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
