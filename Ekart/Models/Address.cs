using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ekart.Models
{
    [Table("Address")]
    public class Address
    {
        [Key]
        [MaxLength(100)]
        [Required]
        public string CID { get; set; }


        [MaxLength(50)]
        public string FName { get; set; }

        [MaxLength(50)]
        public string LName { get; set; }

        [Required]
        [MaxLength(25)]
        public string Street { get; set; }

        [Required]
        [MaxLength(25)]
        public string City { get; set; }

        [Required]
        [MaxLength(25)]
        public string Province { get; set; }

        [Required]
        [MaxLength(25)]
        public string Country { get; set; }

        [Required]
        [MaxLength(7)]
        public string PostalCode { get; set; }

    }
}
