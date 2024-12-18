﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BlazingGidde.Shared.Models.PersonMain
{
    /// <summary>
    /// The AddressType model class.
    /// </summary>
    [Table("AddressType", Schema = "Person")]
    public partial class AddressType
    {
        #region Properties

        /// <summary>
        /// Gets or sets the AddressType ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressTypeID { get; set; }

        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        [Required]
        [StringLength(1)]
       
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>

        [Required]
        [StringLength(30)]
        public string Name { get; set; }= string.Empty;

        /// <summary>
        /// Gets or sets the SortKey.
        /// </summary>
       
        public int? SortKey { get; set; }

        /// <summary>
        /// The collection of Addresses associated with this Address type.
        /// </summary>
        [ForeignKey("AddressTypeID")]
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

        #endregion
    }
}
