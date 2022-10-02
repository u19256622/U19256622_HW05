using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassActivity.Models
{
    public class BooksDetailsViewModel
    {
        public int id { get; set; }
       public string name { get; set; }

        public int numborrows { get; set; }

        public List<borrow> Borrows { get; set; }

        public string status { get; set; }
    }
}