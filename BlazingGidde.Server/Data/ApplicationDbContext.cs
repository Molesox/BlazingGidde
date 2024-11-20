
using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazingGidde.Server.Data
{
	public partial class ApplicationDbContext : IdentityDbContext
	{
		public virtual DbSet<Person> Persons { get; set; }

		public virtual DbSet<Address> Addresses{ get; set; }
		public virtual DbSet<AddressType> AddressTypes { get; set; }
		public virtual DbSet<Email> Emails { get; set; }	
		public virtual DbSet<EmailType> EmailTypes { get; set; }
		public virtual DbSet<PersonProfile> PersonProfiles{ get; set; }
		public virtual DbSet<PersonType> PersonTypes{ get; set; }
		public virtual DbSet<Phone> Phones{ get; set; }
		public virtual DbSet<PhoneType> PhoneTypes{ get; set; }

		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}