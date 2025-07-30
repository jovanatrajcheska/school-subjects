using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISubjectRepository Subjects { get; }
        IDepartmentRepository Departments { get; }
        ITeacherRepository Teachers { get; }
        ILiteratureRepository Literature { get; }
        IAuthorRepository Authors { get; }
        IGradingCriteriaRepository GradingCriteria { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
