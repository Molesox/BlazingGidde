using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BlazingGidde.Shared.Models.PersonMain
{
	/// <summary>
	/// The Phone model class.
	/// </summary>
	[Table("Phone", Schema = "Person")]
	public partial class Phone
	{
		#region Properties

		/// <summary>
		/// Gets or sets the phone ID.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PhoneID { get; set; }

		private int _phoneID;

		/// <summary>
		/// Gets or sets the person ID.
		/// </summary>

		public int PersonID { get; set; }

		private int _personID;

		/// <summary>
		/// Gets or sets the phone type ID.
		/// </summary>

		public int PhoneTypeID { get; set; }

		private int _phoneTypeID;

		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		[Required]
		[StringLength(30)]
		public string PhoneNumber { get; set; }

		private string _phoneNumber;

		/// <summary>
		/// Gets or sets the sort key.
		/// </summary>

		public int? SortKey { get; set; }

		private int? _sortKey;

		/// <summary>
		/// Gets or sets the remarks.
		/// </summary>
		[StringLength(200)]
		public string Remarks { get; set; }

		private string _remarks;

		/// <summary>
		/// Gets or sets the is default flag.
		/// </summary>

		public bool IsDefault { get; set; }

		private bool _isDefault;


		/// <summary>
		/// The PhoneType associated with this Phone.
		/// </summary>
		public virtual PhoneType PhoneType { get; set; }

		/// <summary>
		/// The Person associated with this Phone.
		/// </summary>

		public virtual Person Person { get; set; }

		#endregion
	}
}