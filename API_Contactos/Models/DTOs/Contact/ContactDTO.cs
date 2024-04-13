using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using API_Contactos.Models; // Asegúrate de agregar esta línea

namespace API_Contactos.Models.DTOs.Contact
{
	public class ContactDTO
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public List<PhoneNumbers> PhoneNumbers { get; set; }
	}
}