using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Ekart.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [MaxLength(100)]
        [RegularExpression(@"(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$)", ErrorMessage = "Not a valid e-mail address.")]
        [Required(ErrorMessage = "Email is required")]
        public string Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string FName { get; set; }

        [MaxLength(50)]
        public string LName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [StringLength(10)]
        [Required]
        public string PhoneNumber { get; set; }

        public bool Login_Check(AppDBContext db)
        {
            SqlParameter param1 = new SqlParameter("@p1", this.Id);
            SqlParameter param2 = new SqlParameter("@p2", this.Password);
            var res = db.Customer.FromSqlRaw("select * from Customer where Id=@p1 and Password=@p2", param1, param2).ToList();
            bool Signal = false;
            if(res.Count > 0)
            { Signal = true; }
            return Signal;
        }
        public void Signup(AppDBContext db)
        {
            db.Customer.Add(this); //will  add the obj to database
            db.SaveChanges();
        }

    }
}
