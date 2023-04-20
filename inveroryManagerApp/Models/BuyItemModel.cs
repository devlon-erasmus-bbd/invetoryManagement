using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace inveroryManagerApp.Models
{
    public class BuyItemModel
    {
        public int TransactionId { get; set; }

        public CustomerModel? Customer { get; set; }

        public StaffModel? Staff { get; set; } 

        public List<ItemModel>? Items { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy/mm/dd}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Customers")]
        public int listCustomer { get; set; }

        [Display(Name = "Staffs")]
        public int listStaff { get; set; }

        [Display(Name = "Items")]
        //public int listItem { get; set; }
        public List<SelectListItem> listItem { get; set; }

        public List<string> SelectedItems { get; set; }


    }
}
