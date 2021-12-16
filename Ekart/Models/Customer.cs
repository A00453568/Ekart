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

        [Required(ErrorMessage = "Required")]
        public string Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([\+\-,\.~_=a-z A-Z&\(\)\[\]\{\}\|'""]+)$", ErrorMessage = "Valid First Name is required")]
        public string FName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([\+\-,\.~_=a-z A-Z&\(\)\[\]\{\}\|'""]+)$", ErrorMessage = "Valid Last Name is required")]
        public string LName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[1-9][0-9]{9}", ErrorMessage = "Valid phone number is required")]
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
            db.Customer.Add(this); //will  add the customer object to database
            db.SaveChanges();
        }

    }
}
