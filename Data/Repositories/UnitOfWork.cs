using Microsoft.EntityFrameworkCore.Storage;
using Domain.Interfaces;
using Data.Context;
using System;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext _context;
        private IDbContextTransaction _transaction;

        private ISubjectRepository _subjects;
        private IDepartmentRepository _departments;
        private ITeacherRepository _teachers;
        private ILiteratureRepository _literature;
        private IAuthorRepository _authors;
        private IGradingCriteriaRepository _gradingCriteria;

        public UnitOfWork(SchoolDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ISubjectRepository Subjects =>
            _subjects ??= new SubjectRepository(_context);

        public IDepartmentRepository Departments =>
            _departments ??= new DepartmentRepository(_context);

        public ITeacherRepository Teachers =>
            _teachers ??= new TeacherRepository(_context);

        public ILiteratureRepository Literature =>
            _literature ??= new LiteratureRepository(_context);

        public IAuthorRepository Authors =>
            _authors ??= new AuthorRepository(_context);

        public IGradingCriteriaRepository GradingCriteria =>
            _gradingCriteria ??= new GradingCriteriaRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}
