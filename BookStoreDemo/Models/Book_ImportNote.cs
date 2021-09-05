using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreDemo.Models
{
    public class Book_ImportNote
    {
        public int ID { get; set; }
        public int ID_Book { get; set; }
        public int ID_ImportNote { get; set; }
        public int SoLuongNhap { get; set; }
    }
}
