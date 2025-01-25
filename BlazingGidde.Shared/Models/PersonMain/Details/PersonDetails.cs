using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingGidde.Shared.Models.PersonMain.Details;

public class PersonDetails
{
    /// <summary>
    ///     Gets or sets the person ID.
    /// </summary>
    [Key]
    [Display(Name = "ID", Description = "Unique Identifier")]
    public int PersonId { get; set; }

    /// <summary>
    ///     Gets or sets the culture.
    /// </summary>
    [Required]
    [StringLength(20, ErrorMessage = "Culture must be up to 20 characters.")]
    [Display(Name = "Culture", Description = "Culture of the person")]
    public string Culture { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the title.
    /// </summary>
    [StringLength(80, ErrorMessage = "Title must be up to 80 characters.")]
    [Display(Name = "Title", Description = "Title of the person")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the last name.
    /// </summary>
    [StringLength(80, ErrorMessage = "Last name must be up to 80 characters.")]
    [Required(ErrorMessage = "Last name is required.")]
    [Display(Name = "Last Name", Description = "Last name of the person")]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the first name.
    /// </summary>
    [StringLength(80, ErrorMessage = "First name must be up to 80 characters.")]
    [Required(ErrorMessage = "First name is required.")]
    [Display(Name = "First Name", Description = "First name of the person")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the VAT number.
    /// </summary>
    [StringLength(20, ErrorMessage = "VAT number must be up to 20 characters.")]
    [Display(Name = "VAT Number", Description = "Tax identification number")]
    public string? VatNumber { get; set; }

    /// <summary>
    ///     Gets or sets the remarks.
    /// </summary>
    [StringLength(200, ErrorMessage = "Remarks must be up to 200 characters.")]
    [Display(Name = "Remarks", Description = "Additional comments")]
    public string? Remarks { get; set; }

    /// <summary>
    ///     Gets or sets the annual revenue.
    /// </summary>
    [Display(Name = "Annual Revenue", Description = "Yearly income in USD")]
    [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true, NullDisplayText = "Not Provided")]
    public decimal? AnnualRevenue { get; set; }

    /// <summary>
    ///     Gets the full name, i.e., the concatenation of first name and last name.
    /// </summary>
    [NotMapped]
    [Display(Name = "Full Name", Description = "Full name (First + Last)")]
    public string FullName => $"{FirstName}, {LastName}";

    /// <summary>
    ///     Gets the inverted full name, i.e., the concatenation of last name and first name.
    /// </summary>
    [NotMapped]
    [Display(Name = "Inverted Full Name", Description = "Full name (Last + First)")]
    public string FullNameInv => $"{LastName.ToUpper()}, {FirstName}";
}