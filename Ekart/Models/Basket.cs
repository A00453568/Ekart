using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ekart.Models
{
    [Table("Basket")]
    public class Basket
    {
        [Required]
        public uint PID { get; set; }

        [MaxLength(100)]
        [Required]
        public string Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Product_Name { get; set; }

        [Required]
        public uint Product_Quantity { get; set; }

        [Required]
        public float Price { get; set; }

        [MaxLength(50)]
        public string Brand { get; set; }

        [MaxLength(50)]
        public string Measure { get; set; }

        public Basket(string id, Product obj)
        {
            this.Id = id;
            this.PID = obj.PID;
            this.Product_Name = obj.Product_Name;
            this.Product_Quantity = obj.Product_Quantity;
            this.Price = obj.Price;
            this.Brand = obj.Brand;
            this.Measure = obj.Measure;

        }

        public Basket(string id, uint PID, string Product_Name, uint Product_Quantity, float Price, string Brand, string Measure)
        {
            this.Id = id;
            this.PID = PID;
            this.Product_Name = Product_Name;
            this.Product_Quantity = Product_Quantity;
            this.Price = Price;
            this.Brand = Brand;
            this.Measure = Measure;

        }

    }
}