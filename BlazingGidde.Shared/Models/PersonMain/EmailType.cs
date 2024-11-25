using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BlazingGidde.Shared.Models.PersonMain
{
    /// <summary>
    /// The EmailType model class.
    /// </summary>
    [Table("EmailType", Schema = "Person")]
    
    public partial class EmailType
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email type ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmailTypeID
        {
            get; set;
        }
        int _emailTypeID;

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Required]
        [StringLength(1)]
       
        public string Code
        {
            get; set;
        }
        string _code;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>

        [Required]
        [StringLength(30)]
       
        public string Name
        {
            get; set;
        }
        string _name;

        /// <summary>
        /// Gets or sets the sort key.
        /// </summary>
       
        public int? SortKey
        {
            get; set;
        }
        int? _sortKey;

        /// <summary>
        /// The collection of Emails associated with this Email type.
        /// </summary>
        [ForeignKey("EmailTypeID")]
       
        public virtual ICollection<Email> Emails
        { get; set; }

        #endregion
    }
}
