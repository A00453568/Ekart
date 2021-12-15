using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ekart.Models
{
    public class AddNPay
    {
  
        [MaxLength(100)]
        [Required]
        public string CID { get; set; }


        [MaxLength(50)]
        public string FName { get; set; }

        [MaxLength(50)]
        public string LName { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(25)]
        public string Street { get; set; }

        [Required]
        [MaxLength(25)]
        public string City { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(25)]
        public string Province { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(25)]
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
        [RegularExpression(@"^((5[1-5] [0-9]{14})|(4[0-9]{15})| (34[0 - 9]{ 13})| (37[0 - 9]{ 13}))$", ErrorMessage = "Enter Correct Card Number")]
        public string Card_Holder_Name { get; set; }

        [MaxLength(3)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[0-9]{3}", ErrorMessage = "3 digit CVV required")]
        public string CVV { get; set; }

        [MaxLength(6)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^((0[1-9])|(1[0-2]))\/((20[1][6-9])|(20[2][0-9])|(20[3][0-1]))$", ErrorMessage = "MM/YYYY format of expiry required")]
        public string Expiry { get; set; }
    }
}
