﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BlazingGidde.Shared.Models.PersonMain
{
    /// <summary>
    /// The PhoneType model class.
    /// </summary>
    [Table("PhoneType", Schema = "Person")]
    public partial class PhoneType
    {
        #region Properties

        /// <summary>
        /// Gets or sets the phone type ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneTypeID
        {
            get; set;
        }
        int _phoneTypeID;

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Required]
        [StringLength(2)]
       
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
        /// The collection of Phones associated with this PhoneType.
        /// </summary>
        [ForeignKey("PhoneTypeID")]
       
        public virtual ICollection<Phone> Phones
        { get; set; }

        #endregion

    }
}
