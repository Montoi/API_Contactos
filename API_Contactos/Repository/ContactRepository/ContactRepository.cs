using API_Contactos.Data;
using API_Contactos.Models;
using API_Contactos.Repository.IRepository.IContactRepository;
using API_Contactos.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace API_Contactos.Repository.ContactRepository
{
	public class ContactRepository : Repository<Contact>, IContactRepository
	{
		private readonly AplicationDbContext _context;

		public ContactRepository(AplicationDbContext db) : base(db)
		{
			_context = db;
		}

		public async Task<List<Contact>> GetAllAsync(Expression<Func<Contact, bool>>? filter = null, bool tracked = true)
		{
			IQueryable<Contact> query = _context.Contacts;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			query = query.Include(c => c.PhoneNumbers); // Incluye los datos relacionados de PhoneNumbers

			return await query.ToListAsync();
		}

		public override async Task<Contact> GetAsync(Expression<Func<Contact, bool>>? filter, bool tracked)
		{
			IQueryable<Contact> query = _context.Contacts;
			if (!tracked)
			{
				query = query.AsNoTracking();
			}
			if (filter != null)
			{
				query = query.Where(filter);
			}
			query = query.Include(c => c.PhoneNumbers);

			return await query.FirstOrDefaultAsync();
		}

		public async Task<Contact> UpdateAsync(Contact entity)
		{
			entity.UpdatedDate = DateTime.Now;
			_context.Contacts.Update(entity);

			await _context.SaveChangesAsync();
			return entity;
		}
	}
}
