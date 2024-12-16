
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Models.Patois;
using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazingGidde.Server.Data
{
	public partial class ApplicationDbContext : IdentityDbContext
	{
		#region Person Main

		public virtual DbSet<Person> Persons { get; set; }
		public virtual DbSet<Address> Addresses{ get; set; }
		public virtual DbSet<AddressType> AddressTypes { get; set; }
		public virtual DbSet<Email> Emails { get; set; }	
		public virtual DbSet<EmailType> EmailTypes { get; set; }
		public virtual DbSet<PersonProfile> PersonProfiles{ get; set; }
		public virtual DbSet<PersonType> PersonTypes{ get; set; }
		public virtual DbSet<Phone> Phones{ get; set; }
		public virtual DbSet<PhoneType> PhoneTypes{ get; set; }

		#endregion

		#region FlowCheck

		public virtual DbSet<CustomTemplateItem> CustomTemplateItems { get; set; }
		public virtual DbSet<FlowUser> FlowUsers { get; set; }
		public virtual DbSet<Incidency> Incidencies { get; set; }
		public virtual DbSet<Template> Templates { get; set; }
		public virtual DbSet<TemplateItem> TemplateItems { get; set; }
		public virtual DbSet<TemplateKind> TemplateKinds { get; set; }
		public virtual DbSet<TemplateType> TemplateTypes { get; set; }

		#endregion

		public virtual DbSet<DictionaryEntry> DictionaryEntries{ get; set; }
	
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}