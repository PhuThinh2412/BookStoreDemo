using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreDemo.Models
{
    public class Storage
    {
        public int ID { get; set; }
        public string MaKho { get; set; }
        public Double GiaNhap { get; set; }
        public DateTime NgayNhap { get; set; }
        public int SoLuongTon { get; set; }
    }
}
