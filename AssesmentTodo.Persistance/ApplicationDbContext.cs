using AssesmentTodo.Application;
using AssesmentTodo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService? _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Todo> Todos { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = string.IsNullOrEmpty(entry.Entity.CreatedBy) ? _currentUserService?.IdUser ?? Guid.Empty.ToString() : entry.Entity.CreatedBy;
                    entry.Entity.CreatedAt = DateTime.UtcNow.Date;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedBy = string.IsNullOrEmpty(entry.Entity.UpdatedBy) ? _currentUserService?.IdUser ?? Guid.Empty.ToString() : entry.Entity.UpdatedBy;
                    entry.Entity.UpdatedAt = DateTime.UtcNow.Date;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Todo).Assembly);
        }
    }
}
