using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BlazingGidde.Shared.Models.PersonMain
{
    /// <summary>
    /// The PersonProfile model class.
    /// </summary>
    [Table("PersonProfile", Schema = "Person")]
    
    public partial class PersonProfile
    {
        #region Properties

        /// <summary>
        /// Gets or sets the PersonProfile ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonProfileID { get; set; }

        /// <summary>
        /// Gets or sets the person ID.
        /// </summary>
       
        public int PersonID { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
       
        public byte[]? Photo { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        [Column(TypeName = "date")]
       
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
       
        public int Gender { get; set; }

        /// <summary>
        /// The Person associated with this PersonProfile.
        /// </summary>

        public virtual Person Person { get; set; } = new();

        #endregion
    }
}