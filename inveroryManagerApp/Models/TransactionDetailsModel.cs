namespace inveroryManagerApp.Models;
public class TransactionDetailsModel
{
    public int TransactionDetailsId { get; set; }

    public ItemModel? Item { get; set; }

    public TransactionModel? Transaction { get; set; }
    
    public int Quantity { get; set; }
}