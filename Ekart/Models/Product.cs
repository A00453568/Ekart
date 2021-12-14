using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ekart.Models
{
    [Table("Product")]
    public class Product
    {
        
        [Key]
        public uint PID { get; set; }

        [MaxLength(50)]
        [Required]
        public string Product_Name { get; set; }

        [Required]
        public uint Product_Quantity { get; set; }

        [MaxLength(50)]
        public string Brand { get; set; }

        [MaxLength(50)]
        public string Measure { get; set; }

        [Required]
        public float Price { get; set; }

        [MaxLength(150)]
        [Required]
        public string Image_url { get; set; }

        public Product()
        {
            this.Product_Quantity = 0;
            this.Price = 0;
        }

        public static List<Product> GetProducts(AppDBContext db)
        {
            return db.Product.FromSqlRaw("select * from Product ").ToList();
        }
    }
}
