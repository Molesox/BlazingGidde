﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BlazingGidde.Shared.Models.PersonMain
{
    /// <summary>
    /// The PersonType model class.
    /// </summary>
    [Table("PersonType", Schema = "Person")]
    
    public partial class PersonType 
    {
        #region Properties

        /// <summary>
        /// Gets or sets the person type ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonTypeID { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Required]
        [StringLength(2)]
       
        public string Code { get; set; }= string.Empty;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(30)]
       
        public string Name { get; set; }= string.Empty;

        /// <summary>
        /// Gets or sets the sort key.
        /// </summary>
       
        public int? SortKey { get; set; }

        /// <summary>
        /// The collection of Persons associated with this person type.
        /// </summary>
        public virtual ICollection<Person> Persons { get; set; } = new List<Person>();

		#endregion
	}
}
