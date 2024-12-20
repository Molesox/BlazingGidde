using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazingGidde.Shared.Models.FlowCheck;

namespace BlazingGidde.Shared.Models.PersonMain
{
    /// <summary>
    /// The Person model class.
    /// </summary>
    [Table("Person", Schema = "Person")]
    public partial class Person : IModelBase
    {
        #region Properties
        
        /// <summary>
        /// Gets or sets the person id key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the person type ID.
        /// </summary>
        public int PersonTypeId { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Culture { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [StringLength(80)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [StringLength(80)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [StringLength(80)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the vat number.
        /// </summary>
        [StringLength(20)]
        public string? VatNumber { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        [StringLength(200)]
        public string? Remarks { get; set; }

        /// <summary>
        /// Gets or sets the annual revenue.
        /// </summary>
        public decimal? AnnualRevenue { get; set; }

        /// <summary>
        /// Gets the full name, 
        /// i.e, the concatenation of first name and last name.
        /// </summary>
        [NotMapped]
        public string FullName => $"{FirstName}, {LastName}";

        /// <summary>
        /// Gets the inverted full name,
        /// i.e, the concatenation of last name and first name.
        /// </summary>
        [NotMapped]
        public string FullNameInv => $"{LastName.ToUpper()}, {FirstName}";

        /// <summary>
        /// Gets or sets the skip application user property.
        /// </summary>
        [ForeignKey(nameof(FlowUser.Id))]
        public virtual FlowUser? ApplicationUser { get; set; }

        /// <summary>
        /// Gets or sets the skip person type property.
        /// </summary>
        public virtual PersonType PersonType { get; set; } = new();

        /// <summary>
        /// The collection of Addresses associated with this Person.
        /// </summary>
        [ForeignKey("PersonId")]
        public virtual ICollection<Address>? Addresses { get; set; } = new List<Address>();

        /// <summary>
        /// The collection of Emails associated with this Person.
        /// </summary>
        [ForeignKey("PersonId")]
        public virtual ICollection<Email>? Emails { get; set; } = new List<Email>();

        /// <summary>
        /// The collection of PersonProfiles associated with this Person.
        /// </summary>
        [ForeignKey("PersonId")]
        public virtual ICollection<PersonProfile>? PersonProfiles { get; set; } = new List<PersonProfile>();

        /// <summary>
        /// The collection of Phones associated with this Person.
        /// </summary>
        [ForeignKey("PersonId")]
        public virtual ICollection<Phone>? Phones { get; set; } = new List<Phone>();

        [NotMapped]
        public int Id { get => PersonId; set => PersonId = value; }

        #endregion
    }
}