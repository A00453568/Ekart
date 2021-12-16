using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ekart.Models
{
    [Table("Basket")]
    public class Basket
    {
        [Required]
        [Column("PID")]
        public uint Id { get; set; }

        [MaxLength(100)]
        [Required]
        [Column("email")]
        public string email { get; set; }

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

        [MaxLength(150)]
        public string Image_url { get; set; }


        public Basket()
        {
            this.Id = 0;
            this.email = "";
            this.Product_Name = "";
            this.Product_Quantity = 0;
            this.Price = 0;
        }

        public Basket(string id, Product obj)
        {
            this.Id = obj.PID;
            this.email = id;
            this.Product_Name = obj.Product_Name;
            this.Product_Quantity = obj.Product_Quantity;
            this.Price = obj.Price;
            this.Brand = obj.Brand;
            this.Measure = obj.Measure;
            this.Image_url = obj.Image_url;

        }

        public Basket(string id, uint PID, string Product_Name, uint Product_Quantity, float Price, string Brand, string Measure, string img_url)
        {
            this.Id = PID;
            this.email = id;
            this.Product_Name = Product_Name;
            this.Product_Quantity = Product_Quantity;
            this.Price = Price;
            this.Brand = Brand;
            this.Measure = Measure;
            this.Image_url = img_url;

        }

        public static List<Basket> getBasket(AppDBContext db, string emailId, uint PID)
        {
            SqlParameter param1 = new SqlParameter("@p1", emailId);
            SqlParameter param2 = new SqlParameter("@p2", PID.ToString());
            var res = db.Basket.FromSqlRaw("select * from Basket where email=@p1 and PID=@p2", param1, param2).ToList();
            return res;
        }

    }
}