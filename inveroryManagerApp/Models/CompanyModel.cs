namespace inveroryManagerApp.Models;

public class CompanyModel
{
    public int CompanyId { get; private set; }
    public string? CompanyName { get; set; }
    public string? CompanyDescription { get; set; }
    public DateTime CreatedDate { get; set; }
}