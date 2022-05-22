using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Organic_Food_01_EXM.Models
{
    public class OrderProduct
    {
        public OrderProduct()
        {
            OrderDetails = new List<OrderDetails>();
        }
        public int Id { get; set; }
        [Display(Name = "Order No")]
        public string OrderNo { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
        [Required, Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required, Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Product Quantity")]
        public int ProductQty { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
    public class OrderDetails
    {
        public int Id { get; set; }
        [Display(Name = "Order")]
        public int OrderId { get; set; }
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        //nev
        [ForeignKey("OrderId")]
        public OrderProduct Order { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}
