using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inveroryManagerApp.Models;

[Table("Company")]
public class CompanyModel
{
    [Key]
    public int CompanyId { get; set; }

    [Display(Name = "Company Name")]
    [Required(ErrorMessage = "Company Name is required. It cannot be empty.")]
    public string? CompanyName { get; set; }

    [Display(Name = "Company Description")]
    [Required(ErrorMessage = "Company Description is required. It cannot be empty.")]
    public string? CompanyDescription { get; set; }

    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; }
}