using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreDemo.Models
{
    public class Receipt_Book
    {
        public int ID { get; set; }
        public int ID_Receipt { get; set; }
        public int ID_Book { get; set; }
        public int Soluongban { get; set; }
    }
}
