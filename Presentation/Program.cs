using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Data.Context;
using Data.Repositories;
using Domain.Interfaces;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Threading.Tasks;
using Data.Context;
using Data.SeedData;


namespace Presentation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
                await context.Database.EnsureDeletedAsync();

                await context.Database.EnsureCreatedAsync();
                var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                var jsonPath = Path.Combine("SeedData", "seed-data.json");
                await seeder.SeedAsync(jsonPath);

                Console.WriteLine("Database created and seeded successfully!");
            }

            var consoleApp = host.Services.GetRequiredService<ConsoleApplication>();
            await consoleApp.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<SchoolDbContext>(options =>
                        options.UseSqlite("Data Source=school_subjects.db"));

                    services.AddScoped<IUnitOfWork, UnitOfWork>();

                    services.AddScoped<ISubjectRepository, SubjectRepository>();
                    services.AddScoped<IDepartmentRepository, DepartmentRepository>();
                    services.AddScoped<ITeacherRepository, TeacherRepository>();
                    services.AddScoped<ILiteratureRepository, LiteratureRepository>();
                    services.AddScoped<IAuthorRepository, AuthorRepository>();
                    services.AddScoped<IGradingCriteriaRepository, GradingCriteriaRepository>();

                    services.AddScoped<ISubjectService, SubjectService>();
                    services.AddScoped<IDepartmentService, DepartmentService>();
                    services.AddScoped<ITeacherService, TeacherService>();

                    services.AddTransient<ConsoleApplication>();
                    services.AddTransient<Data.SeedData.DataSeeder>();

                    services.AddLogging(builder =>
                    {
                        builder.AddConsole();
                        builder.SetMinimumLevel(LogLevel.Information);
                    });
                });
    }
}
