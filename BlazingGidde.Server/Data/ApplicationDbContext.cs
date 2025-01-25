using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;
using BlazingGidde.Shared.Models.Patois;
using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazingGidde.Server.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    #region Patois

    public virtual DbSet<DictionaryEntry> DictionaryEntries { get; set; }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder
        // .LogTo(Console.WriteLine)
        // .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Template>().UseTpcMappingStrategy();

        modelBuilder.Entity<FlowUser>()
            .HasMany(u => u.FlowRoles)
            .WithMany(r => r.FlowUsers)
            .UsingEntity<IdentityUserRole<string>>(
                joinEntity => joinEntity
                    .HasOne<FlowRole>()
                    .WithMany()
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Restrict),
                joinEntity => joinEntity
                    .HasOne<FlowUser>()
                    .WithMany()
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Restrict),
                joinEntity =>
                {
                    joinEntity.ToTable("AspNetUserRoles"); // Explicitly use the AspNetUserRoles table
                });

        modelBuilder.Entity<Person>()
            .HasOne(p => p.PersonType)
            .WithMany(t => t.Persons)
            .HasForeignKey(p => p.PersonTypeId)
            .IsRequired();

        modelBuilder.Entity<FlowUser>()
            .HasOne(fu => fu.Person)
            .WithOne(p => p.ApplicationUser)
            .HasForeignKey<Person>(p => p.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    #region Identity

    public virtual DbSet<FlowUser> FlowUsers { get; set; }
    public virtual DbSet<FlowRole> FlowRoles { get; set; }

    #endregion

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
    public virtual DbSet<Incidency> Incidencies { get; set; }
    public virtual DbSet<Template> Templates { get; set; }
    public virtual DbSet<TemplateItem> TemplateItems { get; set; }
    public virtual DbSet<BreakeableItem> BreakeableItems { get; set; }
    public virtual DbSet<GazItem> GazItems { get; set; }
    public virtual DbSet<TemplateKind> TemplateKinds { get; set; }
    public virtual DbSet<TemplateType> TemplateTypes { get; set; }

    #endregion
}