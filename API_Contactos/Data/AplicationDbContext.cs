using API_Contactos.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Contactos.Data
{
	public class AplicationDbContext : DbContext
	{
		public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) 
		{
		
		}


		public DbSet<Contact> Contacts { get; set; }
		public DbSet<PhoneNumbers> PhoneNumbers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


		}


	}
}
