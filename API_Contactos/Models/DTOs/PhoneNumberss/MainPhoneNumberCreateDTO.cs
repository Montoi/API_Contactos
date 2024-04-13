using System.ComponentModel.DataAnnotations.Schema;

namespace API_Contactos.Models.DTOs.PhoneNumberss
{
	public class MainPhoneNumberCreateDTO
	{

		[ForeignKey("Contact")]
		public int ContactId { get; set; }
		public long Number { get; set; }
	}
}
