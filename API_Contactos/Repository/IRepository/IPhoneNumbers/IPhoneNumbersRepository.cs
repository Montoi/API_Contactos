using API_Contactos.Models;

namespace API_Contactos.Repository.IRepository.IPhoneNumbers
{
	public interface IPhoneNumbersRepository : IRepository<PhoneNumbers>
	{
		Task<PhoneNumbers> UpdateAsync(PhoneNumbers entity);
	}
}
