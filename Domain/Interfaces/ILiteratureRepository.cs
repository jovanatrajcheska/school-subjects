using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILiteratureRepository : IRepository<Literature>
    {
        Task<IEnumerable<Literature>> GetLiteratureBySubjectAsync(Guid subjectId);
        Task<Literature> GetLiteratureByIsbnAsync(string isbn);
        Task<IEnumerable<Literature>> GetLiteratureByAuthorAsync(Guid authorId);
    }
}
