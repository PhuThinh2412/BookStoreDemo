using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreDemo.Models
{
    public class Receipt_Coupon
    {
        public int ID { get; set; }
        public int ID_Receipt { get; set; }
        public int ID_Coupon { get; set; }
        public DateTime NgayPhatHanh { get; set; }
    }
}
