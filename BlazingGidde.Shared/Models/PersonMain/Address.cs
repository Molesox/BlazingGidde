﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Runtime.Serialization;

namespace BlazingGidde.Shared.Models.PersonMain
{
    /// <summary>
    /// The Address model class.
    /// </summary>
    [Table("Address", Schema = "Person")]
    
    public partial class Address
    {
        #region Properties

        /// <summary>
        /// Gets or sets the address ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       
        public int AddressID
        {
            get; set;
        }
        int _addressID;

        /// <summary>
        /// Gets or sets the person ID.
        /// </summary>
       
        public int PersonID
        {
            get; set;
        }
        int _personID;

        /// <summary>
        /// Gets or sets the address type ID.
        /// </summary>
       
        public int AddressTypeID
        {
            get; set;
        }
        int _addressTypeID;

        /// <summary>
        /// Gets or sets the address line 1.
        /// </summary>
        [Required]
        [StringLength(60)]
       
        public string AddressLine1
        {
            get; set;
        }
        string _addressLine1;

        /// <summary>
        /// Gets or sets the address line 2.
        /// </summary>
        [StringLength(60)]
       
        public string AddressLine2
        {
            get; set;
        }
        string _addressLine2;

        /// <summary>
        /// Gets or sets the address line 3.
        /// </summary>
        [StringLength(60)]
       
        public string AddressLine3
        {
            get; set;
        }
        string _addressLine3;

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [Required]
        [StringLength(20)]
       
        public string PostalCode
        {
            get; set;
        }
        string _PostalCode;

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [Required]
        [StringLength(50)]
       
        public string City
        {
            get; set;
        }
        string _city;

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        [StringLength(30)]
       
        public string Region
        {
            get; set;
        }
        string _region;

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [StringLength(30)]
       
        public string Country
        {
            get; set;
        }
        string _country;


        /// <summary>
        /// Gets or sets the sort key.
        /// </summary>
       
        public int? SortKey
        {
            get; set;
        }
        int? _sortKey;

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        [StringLength(200)]
       
        public string Remarks
        {
            get; set;
        }
        string _remarks;

        /// <summary>
        /// Gets or sets the is default flag.
        /// </summary>
       
        public bool IsDefault
        {
            get; set;
        }
        bool _isDefault;

        /// <summary>
        /// The AddressType associated with this Address.
        /// </summary>
       
        public virtual AddressType AddressType { get; set; }

        /// <summary>
        /// The Person associated with this Address.
        /// </summary>
       
        public virtual Person Person { get; set; }

        #endregion
    }
}