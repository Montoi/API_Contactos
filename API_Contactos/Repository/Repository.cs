
using API_Contactos.Data;
using API_Contactos.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API_Contactos.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AplicationDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(AplicationDbContext db)
        {
            _context = db;
            this.dbSet = _context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity); 
            await SaveAsync();
        }

        public async Task Remove(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

       

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

       

       public virtual async Task<T> GetAsync(Expression<Func<T, bool>>? filter, bool tracked)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
               query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);

            }

            return await query.ToListAsync();
        }
    }
}
