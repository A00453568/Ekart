using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ekart.Models
{
    [Table("Transactions")]
    public class Transactions
    {
        [Key]
        public uint TID { get; set; }

        [Required]
        public uint OID { get; set; }

        [Required]
        public float Transaction_Value { get; set; }

    }
}
