using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreDemo.Models
{
    public class Receipt
    {
        public int ID { get; set; }
        public string MaHoaDon { get; set; }
        public DateTime NgayBan { get; set; }
        public Double ThanhTien { get; set; }
        public int ID_Customer { get; set; }
    }
}
