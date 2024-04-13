using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Contactos.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Email { get; set; }

		public List<PhoneNumbers> PhoneNumbers { get; set; }
		public DateTime CreatedDate { get; set; }    
        public DateTime UpdatedDate { get; set;}
    }
}
