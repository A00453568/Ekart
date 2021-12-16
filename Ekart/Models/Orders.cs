using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ekart.Models
{
    [Table("Orders")]
    public class Orders
    {

        [Key]
        public uint OID { get; set; }


        public string CID { get; set; }


        public uint PID { get; set; }

        [Required]
        public uint Quantity { get; set; }

        [Required]
        public float Subtotal { get; set; }

    }

}
