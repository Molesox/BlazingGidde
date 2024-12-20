using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BlazingGidde.Shared.Models;

public abstract class ModelBase : ISupportTimeStamp, IModelBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime? UpdateDate { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    [MaxLength(256)]
    public string CreateUser { get; set; } = string.Empty;

    [MaxLength(256)]
    public string? UpdateUser { get; set; }

}
