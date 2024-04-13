using API_Contactos.Data;
using API_Contactos.Models;
using API_Contactos.Repository.IRepository.IContactRepository;
using API_Contactos.Repository.IRepository.IPhoneNumbers;
using API_Contactos.Repository;

namespace API_Contactos.Repository.PhoneNumbersRepository
{
	public class PhoneNumbersRepository : Repository<PhoneNumbers>, IPhoneNumbersRepository
	{
		private readonly AplicationDbContext _context;

		public PhoneNumbersRepository(AplicationDbContext db) : base(db)
		{
			_context = db;
		}



		public async Task<PhoneNumbers> UpdateAsync(PhoneNumbers entity)
		{
			entity.UpdatedDate = DateTime.Now;
			_context.PhoneNumbers.Update(entity);

			await _context.SaveChangesAsync();
			return entity;
		}
	}
}
