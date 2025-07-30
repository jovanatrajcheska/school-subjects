using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAuthorsByLiteratureAsync(Guid literatureId);
        Task<IEnumerable<Author>> SearchAuthorsByNameAsync(string searchTerm);
    }
}
