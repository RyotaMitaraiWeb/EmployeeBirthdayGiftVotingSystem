using EmployeeBirthdayGiftVotingSystem.Contracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeBirthdayGiftVotingSystem.Data.Repositories
{
    public class Repository(ApplicationDbContext context) : IRepository
    {
        protected DbContext Context { get; set; } = context;
        protected DbSet<T> DbSet<T>() where T : class
        {
            return this.Context.Set<T>();
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await DbSet<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await DbSet<T>().AddRangeAsync(entities);
        }
        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>().AsQueryable();
        }

        public IQueryable<T> All<T>(Expression<Func<T, bool>> search) where T : class
        {
            return this.DbSet<T>().Where(search);
        }

        public IQueryable<T> AllReadonly<T>() where T : class
        {
            return this.DbSet<T>()
                .AsNoTracking();
        }
        public IQueryable<T> AllReadonly<T>(Expression<Func<T, bool>> search) where T : class
        {
            return this.DbSet<T>()
                .Where(search)
                .AsNoTracking();
        }

        public void Detach<T>(T entity) where T : class
        {
            EntityEntry entry = this.Context.Entry(entity);

            entry.State = EntityState.Detached;
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        /// <summary>
        /// Gets a specific record from the database by primary key
        /// </summary>
        /// <param name="id">A record identificator</param>
        /// <returns>A single record</returns>
        public async Task<T?> GetByIdAsync<T>(object id) where T : class
        {
            return await DbSet<T>().FindAsync(id);
        }

        /// <summary>
        /// Gets a specific record from the database by primary keys
        /// </summary>
        /// <param name="id">An array of record identificators</param>
        /// 
        /// <returns>A single record</returns>
        public async Task<T?> GetByIdsAsync<T>(object[] id) where T : class
        {
            return await DbSet<T>().FindAsync(id);
        }

        /// <summary>
        /// Saves all made changes in trasaction
        /// </summary>
        /// 
        /// <returns>Error code</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a record in database
        /// </summary>
        /// 
        /// <param name="entity">The entity for record to be updated</param>
        public void Update<T>(T entity) where T : class
        {
            this.DbSet<T>().Update(entity);
        }

        /// <summary>
        /// Updates a set of records in the database
        /// </summary>
        /// 
        /// <param name="entities">An enumerable collection of the entities to be updated</param>
        public void UpdateRange<T>(IEnumerable<T> entities) where T : class
        {
            this.DbSet<T>().UpdateRange(entities);
        }
    }
}
