using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ekart.Models
{
    [Table("AddNPay")]
    public class AddNPay
    {
  
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        [Key]
        public string CID { get; set; }


        [MaxLength(50)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([\+\-,\.~_=a-z A-Z&\(\)\[\]\{\}\|'""]+)$", ErrorMessage = "Valid First Name is required")]
        public string FName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([\+\-,\.~_=a-z A-Z&\(\)\[\]\{\}\|'""]+)$", ErrorMessage = "Valid Last Name is required")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(25)]
        [RegularExpression(@"^([\+\-,\.~_=a-z 0-9A-Z&\(\)\[\]\{\}\|'""]+)$", ErrorMessage = "Valid Address is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(25)]
        [RegularExpression(@"^([\+\-,\.~_=a-z A-Z&\(\)\[\]\{\}\|'""]+)$", ErrorMessage = "Valid City Name is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(25)]
        [RegularExpression(@"^([\+\-,\.~_=a-z A-Z&\(\)\[\]\{\}\|'""]+)$", ErrorMessage = "Valid Province Name is required")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(25)]
        [RegularExpression(@"^([\+\-,\.~_=a-z A-Z&\(\)\[\]\{\}\|'""]+)$" , ErrorMessage = "Valid Country Name is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(7)]
        public string PostalCode { get; set; }

        [MaxLength(100)]
        public string Id { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[1-9][0-9]{9}", ErrorMessage = "Valid phone number is required")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "{0} length must be between 15 and 16.", MinimumLength = 15)]
        public string Card_Number { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([\+\-,\.~_=a-z A-Z&\(\)\[\]\{\}\|'""]+)$", ErrorMessage = "Valid Card Holder Name is required")]
        public string Card_Holder_Name { get; set; }

        [MaxLength(3)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[0-9]{3}", ErrorMessage = "3 digit CVV required")]
        public string CVV { get; set; }

        [MaxLength(7)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^((0[1-9])|(1[0-2]))\/((20[1][6-9])|(20[2][0-9])|(20[3][0-1]))$", ErrorMessage = "MM/YYYY format of expiry required")]
        public string Expiry { get; set; }
    }
}
