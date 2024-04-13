using API_Contactos.Models.DTOs.PhoneNumberss;
using API_Contactos.Models.DTOs.PhoneNumberss;
using System.ComponentModel.DataAnnotations;

namespace API_Contactos.Models.DTOs.Contact
{
	public class ContactCreateDTO
	{
		
		[Required]

		public string Name { get; set; }
		[Required]

		public string Email { get; set; }
		[Required]

		public List<PhoneNumbersCreateDTO> PhoneNumbers { get; set; }
	}
}
