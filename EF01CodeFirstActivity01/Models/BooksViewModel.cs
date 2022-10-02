using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassActivity.Models
{
    public class BooksViewModel
    {
       
       public List<book> books { get; set; }

        public List<author> authors { get; set; }

        public List<type> types { get; set; }

    }
}