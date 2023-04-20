using System.ComponentModel.DataAnnotations;

namespace inveroryManagerApp.Models;
public class TransactionModel
{
    public int TransactionId { get; set; }
    
    public StaffModel? Staff { get; set; }
    
    public CustomerModel? Customer { get; set; }

    public decimal TransactionTotal { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{yyyy/mm/dd}", ApplyFormatInEditMode = true)]
    public DateTime TransactionDate { get; set; }
}