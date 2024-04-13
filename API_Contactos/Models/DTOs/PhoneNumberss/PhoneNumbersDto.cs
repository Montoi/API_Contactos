using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Contactos.Models.DTOs.PhoneNumberss
{
	public class PhoneNumbersDTO
	{
		
			
			public int id { get; set; }
			[ForeignKey("Contact")]
			public int ContactId { get; set; }
			public long Number { get; set; }
		
	
	}
}
