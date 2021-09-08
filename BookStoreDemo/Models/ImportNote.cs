using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreDemo.Models
{
    public class ImportNote
    {
        public int ID { get; set; }
        public string MaPhieu { get; set; }
        public string SoLuongNhap { get; set; }
        public double GiaNhap { get; set; }
        public int ID_Storage { get; set; }
    }
}
