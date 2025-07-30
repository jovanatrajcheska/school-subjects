using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;
using System;

namespace Data.Context
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Literature> Literature { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<GradingCriteria> GradingCriteria { get; set; }
        public DbSet<SubjectTeacher> SubjectTeachers { get; set; }
        public DbSet<SubjectLiterature> SubjectLiterature { get; set; }
        public DbSet<LiteratureAuthor> LiteratureAuthors { get; set; }
        public DbSet<SubjectPrerequisite> SubjectPrerequisites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureEntities(modelBuilder);
            ConfigureRelationships(modelBuilder);
            SeedData(modelBuilder);
        }

        private void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.WeeklyClasses).IsRequired();
                entity.Property(e => e.IsMandatory).IsRequired();
                entity.Property(e => e.Semester).IsRequired().HasConversion<string>();
                entity.Property(e => e.EvaluationMethod).IsRequired().HasConversion<string>();
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DepartmentName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.HeadTeacherId).IsRequired();
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Title).IsRequired().HasConversion<string>();
                entity.Property(e => e.Email).HasMaxLength(100);
            });

            modelBuilder.Entity<Literature>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Isbn).HasMaxLength(20);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<GradingCriteria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MinScore).IsRequired();
                entity.Property(e => e.MaxScore).IsRequired();
                entity.Property(e => e.GradeLabel).IsRequired().HasMaxLength(10);
            });
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // Between: Department - HeadTeacher && Type of relationship: 1:1 required
            modelBuilder.Entity<Department>()
                .HasOne(d => d.HeadTeacher)
                .WithOne(t => t.HeadOfDepartment)
                .HasForeignKey<Department>(d => d.HeadTeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Between: Department - Subjects && Type of relationship: 1:Many
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Subjects)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Between: Subject - GradingCriteria && Type of relationship: 1:Many
            modelBuilder.Entity<GradingCriteria>()
                .HasOne(gc => gc.Subject)
                .WithMany(s => s.GradingCriteria)
                .HasForeignKey(gc => gc.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Between: Subject - Teacher && Type of relationship: Many:Many
            modelBuilder.Entity<SubjectTeacher>(entity =>
            {
                entity.HasKey(st => new { st.SubjectId, st.TeacherId });

                entity.HasOne(st => st.Subject)
                      .WithMany(s => s.SubjectTeachers)
                      .HasForeignKey(st => st.SubjectId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(st => st.Teacher)
                      .WithMany(t => t.SubjectTeachers)
                      .HasForeignKey(st => st.TeacherId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Between: Subject - Literature && Type of relationship: Many:Many
            modelBuilder.Entity<SubjectLiterature>(entity =>
            {
                entity.HasKey(sl => new { sl.SubjectId, sl.LiteratureId });

                entity.HasOne(sl => sl.Subject)
                      .WithMany(s => s.SubjectLiterature)
                      .HasForeignKey(sl => sl.SubjectId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sl => sl.Literature)
                      .WithMany(l => l.SubjectLiterature)
                      .HasForeignKey(sl => sl.LiteratureId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Between: Literature - Author && Type of relationship: Many:Many
            modelBuilder.Entity<LiteratureAuthor>(entity =>
            {
                entity.HasKey(la => new { la.LiteratureId, la.AuthorId });

                entity.HasOne(la => la.Literature)
                      .WithMany(l => l.LiteratureAuthors)
                      .HasForeignKey(la => la.LiteratureId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(la => la.Author)
                      .WithMany(a => a.LiteratureAuthors)
                      .HasForeignKey(la => la.AuthorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Between: Subject Prerequisites && Type of relationship: Many:Many self-referencing
            modelBuilder.Entity<SubjectPrerequisite>(entity =>
            {
                entity.HasKey(sp => new { sp.SubjectId, sp.PrerequisiteSubjectId });

                entity.HasOne(sp => sp.Subject)
                      .WithMany(s => s.Prerequisites)
                      .HasForeignKey(sp => sp.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(sp => sp.PrerequisiteSubject)
                      .WithMany()
                      .HasForeignKey(sp => sp.PrerequisiteSubjectId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var teacherId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var teacherId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var teacherId3 = Guid.Parse("33333333-3333-3333-3333-333333333333");

            var departmentId1 = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var departmentId2 = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var departmentId3 = Guid.Parse("66666666-6666-6666-6666-666666666666");

            var authorId1 = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var authorId2 = Guid.Parse("88888888-8888-8888-8888-888888888888");

            var literatureId1 = Guid.Parse("99999999-9999-9999-9999-999999999999");
            var literatureId2 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            var subjectId1 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var subjectId2 = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var subjectId3 = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

            modelBuilder.Entity<Teacher>().HasData(
                new { Id = teacherId1, FirstName = "Emily", LastName = "Green", Title = AcademicTitle.BSc, Email = "emily.green@elementary.edu", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new { Id = teacherId2, FirstName = "Carlos", LastName = "Nguyen", Title = AcademicTitle.MSc, Email = "carlos.nguyen@elementary.edu", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new { Id = teacherId3, FirstName = "Priya", LastName = "Sharma", Title = AcademicTitle.PhD, Email = "priya.sharma@elementary.edu", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            modelBuilder.Entity<Department>().HasData(
                new { Id = departmentId1, DepartmentName = "Natural Sciences", HeadTeacherId = teacherId1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new { Id = departmentId2, DepartmentName = "Creative Arts", HeadTeacherId = teacherId2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new { Id = departmentId3, DepartmentName = "Mathematics", HeadTeacherId = teacherId3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            modelBuilder.Entity<Author>().HasData(
                new { Id = authorId1, FirstName = "Ava", LastName = "Baker", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new { Id = authorId2, FirstName = "Liam", LastName = "Rivera", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            modelBuilder.Entity<Literature>().HasData(
                new { Id = literatureId1, Title = "Discovering Plants and Animals", Isbn = "978-1234567890", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new { Id = literatureId2, Title = "Art Around Us", Isbn = "978-0987654321", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            modelBuilder.Entity<Subject>().HasData(
                new
                {
                    Id = subjectId1,
                    Name = "Elementary Science",
                    Description = "Exploring the world of plants, animals, and the environment.",
                    WeeklyClasses = 3,
                    IsMandatory = true,
                    Semester = Semester.Fall,
                    EvaluationMethod = EvaluationMethod.Project,
                    DepartmentId = departmentId1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new
                {
                    Id = subjectId2,
                    Name = "Creative Drawing",
                    Description = "Learning drawing, painting, and creative expression.",
                    WeeklyClasses = 2,
                    IsMandatory = false,
                    Semester = Semester.Spring,
                    EvaluationMethod = EvaluationMethod.Presentation,
                    DepartmentId = departmentId2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new
                {
                    Id = subjectId3,
                    Name = "Math Foundations",
                    Description = "Numbers, addition, subtraction, and problem-solving.",
                    WeeklyClasses = 4,
                    IsMandatory = true,
                    Semester = Semester.Fall,
                    EvaluationMethod = EvaluationMethod.Exam,
                    DepartmentId = departmentId3,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<SubjectTeacher>().HasData(
                new { SubjectId = subjectId1, TeacherId = teacherId1 },
                new { SubjectId = subjectId2, TeacherId = teacherId2 },
                new { SubjectId = subjectId3, TeacherId = teacherId3 }
            );

            modelBuilder.Entity<SubjectLiterature>().HasData(
                new { SubjectId = subjectId1, LiteratureId = literatureId1 },
                new { SubjectId = subjectId2, LiteratureId = literatureId2 }
            );

            modelBuilder.Entity<LiteratureAuthor>().HasData(
                new { LiteratureId = literatureId1, AuthorId = authorId1 },
                new { LiteratureId = literatureId2, AuthorId = authorId2 }
            );

            modelBuilder.Entity<GradingCriteria>().HasData(
                new { Id = Guid.Parse("11111111-2222-3333-4444-555555555555"), MinScore = 90, MaxScore = 100, GradeLabel = "A", SubjectId = subjectId1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new { Id = Guid.Parse("22222222-3333-4444-5555-666666666666"), MinScore = 80, MaxScore = 89, GradeLabel = "B", SubjectId = subjectId1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new { Id = Guid.Parse("33333333-4444-5555-6666-777777777777"), MinScore = 70, MaxScore = 79, GradeLabel = "C", SubjectId = subjectId1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new { Id = Guid.Parse("44444444-5555-6666-7777-888888888888"), MinScore = 60, MaxScore = 69, GradeLabel = "D", SubjectId = subjectId1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new { Id = Guid.Parse("55555555-6666-7777-8888-999999999999"), MinScore = 0, MaxScore = 59, GradeLabel = "F", SubjectId = subjectId1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );
        }

    }
}
