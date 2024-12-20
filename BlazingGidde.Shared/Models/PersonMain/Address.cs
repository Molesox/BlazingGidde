using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlazingGidde.Shared.Models.PersonMain
{
    /// <summary>
    /// The Address model class.
    /// </summary>
    [Table("Address", Schema = "Person")]
    public partial class Address : ModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the person ID.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the address type ID.
        /// </summary>
        public int AddressTypeId { get; set; }

        /// <summary>
        /// Gets or sets the address line 1.
        /// </summary>
        [Required]
        [StringLength(60)]
        public string AddressLine1 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address line 2.
        /// </summary>
        [StringLength(60)]
        public string AddressLine2 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address line 3.
        /// </summary>
        [StringLength(60)]
        public string AddressLine3 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        [StringLength(30)]
        public string Region { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [StringLength(30)]

        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sort key.
        /// </summary>
        public int? SortKey { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        [StringLength(200)]
        public string Remarks { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the is default flag.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// The AddressType associated with this Address.
        /// </summary>
        public virtual AddressType AddressType { get; set; } = new();

        /// <summary>
        /// The Person associated with this Address.
        /// </summary>
        public virtual Person Person { get; set; } = new();

        #endregion
    }
}