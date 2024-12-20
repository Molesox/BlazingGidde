using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.PersonMain
{
    /// <summary>
    /// The Email model class.
    /// </summary>
    [Table("Email", Schema = "Person")]   
    public partial class Email : ModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the person ID.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the email type ID.
        /// </summary>
        public int EmailTypeId { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [StringLength(255)]
        public string EmailAddress { get; set; }= string.Empty;

        /// <summary>
        /// Gets or sets the sort key.
        /// </summary>
        public int? SortKey { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        [StringLength(200)]
        public string Remarks { get; set; }= string.Empty;

        /// <summary>
        /// Gets or sets the is default flag.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// The EmailType associated with this Email.
        /// </summary>
        public virtual EmailType EmailType { get; set; } = new();

        /// <summary>
        /// The Person associated with this Email.
        /// </summary>
        public virtual Person Person { get; set; } = new();

        #endregion
    }
}
