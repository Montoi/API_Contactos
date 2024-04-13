
using API_Contactos.Models.DTOs.PhoneNumberss;
using System.ComponentModel.DataAnnotations;

namespace API_Contactos.Models.DTOs.Contact
{
	public class ContactUpdateDTO
	{
		public int Id { get; set; }
		[Required]

		public string Name { get; set; }
		[Required]

		public string Email { get; set; }
		[Required]

		public List<PhoneNumbersUpdateDTO> PhoneNumbers { get; set; }
	}
}
