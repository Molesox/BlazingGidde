using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.Patois
{
    [Table("DictionaryEntry", Schema = "Patois")]
	public class DictionaryEntry
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public char Parent { get; set; }

		[StringLength(100)]
		public string? FrenchWord { get; set; }

		[StringLength(100)]
		public string? DialectWord { get; set; }

		[StringLength(500)]
		public string? FrenchExample { get; set; }

		[StringLength(500)]
		public string? DialectExample { get; set; }

		public string? AudioId { get; set; }

		#region Auditables

		public DateTime Created { get; set; }

		[StringLength(100)]
		public string? CreatedVisa { get; set; }

		public DateTime? Updated { get; set; }

		[StringLength(100)]
		public string? UpdatedVisa { get; set; }

		#endregion
	}
}
