using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileStore.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]

        public string Title { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public decimal Price { get; set; }
        [Required]

        public int AvailableProduct { get; set; }
        [Required]

        public int SoldProduct { get; set; }
        [Required]

        public string ImageUrl { get; set; }
        [Required]

        public int CategoryId { get; set; }
        [Required]

        public int BrandId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
    }
}