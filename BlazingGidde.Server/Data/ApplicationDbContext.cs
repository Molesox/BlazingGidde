
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;
using BlazingGidde.Shared.Models.Patois;
using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BlazingGidde.Shared.Models.Identity;

namespace BlazingGidde.Server.Data
{
	public partial class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		#region Person Main

		public virtual DbSet<Person> Persons { get; set; }
		public virtual DbSet<Address> Addresses { get; set; }
		public virtual DbSet<AddressType> AddressTypes { get; set; }
		public virtual DbSet<Email> Emails { get; set; }
		public virtual DbSet<EmailType> EmailTypes { get; set; }
		public virtual DbSet<PersonProfile> PersonProfiles { get; set; }
		public virtual DbSet<PersonType> PersonTypes { get; set; }
		public virtual DbSet<Phone> Phones { get; set; }
		public virtual DbSet<PhoneType> PhoneTypes { get; set; }

		#endregion

		#region FlowCheck

		public virtual DbSet<CustomTemplateItem> CustomTemplateItems { get; set; }
		public virtual DbSet<FlowUser> FlowUsers { get; set; }
		public virtual DbSet<FlowRole> FlowRoq { get; set; }
		public virtual DbSet<Incidency> Incidencies { get; set; }
		public virtual DbSet<Template> Templates { get; set; }
		public virtual DbSet<TemplateItem> TemplateItems { get; set; }
		public virtual DbSet<BreakeableItem> BreakeableItems { get; set; }
		public virtual DbSet<GazItem> GazItems { get; set; }
		public virtual DbSet<TemplateKind> TemplateKinds { get; set; }
		public virtual DbSet<TemplateType> TemplateTypes { get; set; }

		#endregion

		#region Patois

		public virtual DbSet<DictionaryEntry> DictionaryEntries { get; set; }

		#endregion

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
			.LogTo(Console.WriteLine)
			.EnableSensitiveDataLogging();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Person>()
				.HasOne(p => p.PersonType)
				.WithMany(t => t.Persons)
				.HasForeignKey(p => p.PersonTypeId)
				.IsRequired();

			modelBuilder.Entity<Template>().UseTpcMappingStrategy();

		}
	}
}