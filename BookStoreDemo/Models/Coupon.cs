using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreDemo.Models
{
    public class Coupon
    {
        public int ID { get; set; }
        public string MaCoupon { get; set; }
        public int GiaTri { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }
}
