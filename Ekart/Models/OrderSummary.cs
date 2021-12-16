using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekart.Models
{
    public class OrderSummary
    {
        public uint PID { get; set; }

        public string Email { get; set; }

        public string Product_Name { get; set; }

        public string Brand { get; set; }

        public string Measure { get; set; }

        public uint Product_Quantity { get; set; }

        public float Price { get; set; }

        public float Subtotal { get; set;}

        public string Image_url { get; set; }
    }

}
