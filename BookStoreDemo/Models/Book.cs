using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreDemo.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public Double DonGia { get; set; }
        public int Soluong { get; set; }
        public int ID_Publisher { get; set; }
        public int ID_Author { get; set; }

    }
}
