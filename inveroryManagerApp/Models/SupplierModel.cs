using System.ComponentModel.DataAnnotations;

namespace inveroryManagerApp.Models;
public class SupplierModel
{
    public int SupplierId { get; set; }

    [Required(ErrorMessage = "Supplier Name is required. It cannot be empty.")]
    public string? SupplierName { get; set; }

    [Required(ErrorMessage = "Supplier Contact Number is required. It cannot be empty.")]
    public string? SupplierContactNumber { get; set; }

    public DateTime DateAdded { get; set; }
}