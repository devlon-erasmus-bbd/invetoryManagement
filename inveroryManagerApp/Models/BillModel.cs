namespace inveroryManagerApp.Models;
public class BillModel
{
    public int BillId { get; set; }
    public int StaffId { get; set; }
    public int CustomerId { get; set; }
    public datetime OrderDate { get; set; }
}