using System.ComponentModel.DataAnnotations;

namespace inveroryManagerApp.Models;
public class CustomerModel
{
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Customer Name is required. It cannot be empty.")]
    public string? CustomerName { get; set; }

    [Required(ErrorMessage = "Customer Name is required. It cannot be empty.")]
    public string? CustomerContactNumber { get; set; }
}