using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Organic_Food_01_EXM.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Title")]
        public string ProductTitle { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; }
        [Display(Name = "Product Ask Price")]
        public decimal ProductAskPrice { get; set; }
        [Display(Name = "Product Details")]
        public string ProductDetails { get; set; }
        public string Image { get; set; }
        [Display(Name ="Product Color")]
        public string ProductColor { get; set; }
        public DateTime Date { get; set; }
        [Required, Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        //nev
        [Required, Display(Name = "Tag")]
        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
        //nev
        [Required, Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
