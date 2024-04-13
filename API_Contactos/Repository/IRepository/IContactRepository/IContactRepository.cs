using API_Contactos.Models;

using API_Contactos.Repository.IRepository;
using System.Linq.Expressions;

namespace API_Contactos.Repository.IRepository.IContactRepository
{
	public interface IContactRepository : IRepository<Contact>
	{

		Task<Contact> UpdateAsync(Contact entity);
		Task<List<Contact>> GetAllAsync(Expression<Func<Contact, bool>>? filter = null, bool tracked = true); // Añade GetAllAsync a la interfaz


	}
}
