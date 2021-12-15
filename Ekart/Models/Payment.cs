using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ekart.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        [MaxLength(100)]
        public string Id { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "{0} length must be between 15 and 16.", MinimumLength = 15)]
        public string Card_Number { get; set; }

        [MaxLength(50)]
        [Required]
        public string Card_Holder_Name { get; set; }
    }
}
